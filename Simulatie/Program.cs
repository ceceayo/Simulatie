using Simulatie;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

var folder = Environment.SpecialFolder.LocalApplicationData;
var path = Environment.GetFolderPath(folder);
var logPath = Path.Join(path, "simulatie.log");

using var log = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .WriteTo.Debug()
    .WriteTo.File(logPath)
    .CreateLogger();
Log.Logger = log;

using var db = new SimulationDatabaseContext();
Log.Information("Created a database {db}", db);

// Note: This sample requires the database to be created before running.
Log.Information("Database path is {DbPath}.", db.DbPath);

var up = new UnitProvider();
var sp = /*new SocialistischePartij();*/ new StatProvider();

int CreateSimulation()
{
    Log.Information("I will now be creating a new simulation, meaning a city will be created and populated");
    SimulatedUnit rootCity = new SimulatedUnit { Type = 1, Owner = null };
    db.SimulatedUnits.Add(rootCity);
    db.SaveChanges();
    Simulation sim = new Simulation { TotalResourcesUsed = 0, Unit = rootCity, Year = 2000, Day = 0, Hour = 11 };
    db.Simulations.Add(sim);
    db.SaveChanges();
    Log.Information("I have created a city ({@city}) and a simulation ({@sim}).", rootCity, sim);
    List<IUnitType> children = up.GetInstance(rootCity.Id, db)!.OnCreate(db, sp, up, sim);
    Queue<CreateSimulationItem> items = new Queue<CreateSimulationItem>();
    foreach (var child in children)
    {
        Log.Debug("Saving child {@child}", child);
        int NewUnitId = up.MakeInstance(child, db, child.Owner);
        Log.Debug("Child got Id {Id}", NewUnitId);
        IUnitType newUnit = up.GetInstance(NewUnitId, db)!;
        List<IUnitType> newChildren = newUnit.OnCreate(db, sp, up, sim);
        foreach (var newChild in newChildren)
        {
            items.Enqueue(new CreateSimulationItem ( Unit: newChild, OwnerId: newUnit.Id ));
        }
    }
    while (items.Count > 0)
    {
        CreateSimulationItem item = items.Dequeue();
        Log.Debug("Saving item {@item}", item);
        int NewUnitId = up.MakeInstance(item.Unit, db, up.GetInstance(item.OwnerId, db));
        Log.Debug("Item got Id {Id}", NewUnitId);
        IUnitType newUnit = up.GetInstance(NewUnitId, db)!;
        List<IUnitType> newChildren = newUnit.OnCreate(db, sp, up, sim);
        foreach (var newChild in newChildren)
        {
            items.Enqueue(new CreateSimulationItem(Unit: newChild, OwnerId: newUnit.Id));
        }
    }
    return sim.Id;
}

RunSimulationRecursiveResult RunSimulationRecursive(IUnitType unit, Simulation sim)
{
    Log.Information("Calling RunSimulationRecursive on unit {@unit}", unit);
    int TotalPowerUsed = 0;
    List<IUnitType> new_units = new List<IUnitType>();
    var q = db.SimulatedUnits.Where(b => b.Id == unit.Id).First();
    Log.Debug("Database has object {@obj} stored.", q);
    var result = unit.OnTick(db,sp,up,sim);
    TotalPowerUsed += result.ResourcesUsed;
    new_units.Add(result.NewUnit);
    Log.Information("Running first step of simulation used {power} watts", result.ResourcesUsed);
    var children = up.GetAllOwnedBy(unit, db);
    Log.Information("Children of {Id} are {@children}", unit.Id, children);
    foreach (var child in children)
    {
        RunSimulationRecursiveResult ResultOfChild = RunSimulationRecursive(child, sim);
        TotalPowerUsed += ResultOfChild.ResourcesUsed;
        foreach (var NewUnit in ResultOfChild.NewUnits)
        {
            new_units.Add(NewUnit);
        }
    }
    return new RunSimulationRecursiveResult
    {
        NewUnits = new_units,
        ResourcesUsed = TotalPowerUsed
    };
}

int RunSimulationAt(IUnitType start, Simulation sim)
{
    Log.Information("Running simulation at {@start}", start);
    RunSimulationRecursiveResult result = RunSimulationRecursive(start, sim);
    Log.Information("Recursive simulation has ended with {power} watt.", result.ResourcesUsed);
    if (sim.Hour == 23)
    {
        sim.Hour = 0;
        if (sim.Day == 364)
        {
            sim.Day = 0;
            sim.Year = sim.Year + 1;
        }
        else
        {
            sim.Day += 1;
        }
    }
    else
    {
        sim.Hour += 1;
    }
    db.SaveChanges();
    Log.Debug("Saving new units from database.");
    foreach (var unit in result.NewUnits)
    {
        Log.Debug("Saving unit {@unit}", unit);
        up.SaveUnit(unit, db);
    }
    var x = db.Simulations.Find(sim.Id);
    if (x == null)
    {
        Log.Fatal("Could not find simulation with id {id}", sim.Id);
        throw new Exception("Could not find simulation with id");
    }
    else
    {
        x.TotalResourcesUsed += result.ResourcesUsed;
        db.SaveChanges();
    }
    return result.ResourcesUsed;
}

Log.Information("Waiting for OPTION to be selected. [c]reate sim., [r]un sim., [t]est.");
char input = Console.ReadKey().KeyChar;
Console.WriteLine();

if (input == 'c')
{
    var res = CreateSimulation();
    Log.Information("Finished creating simulation with id {res}", res);
}
else if (input == 'r')
{
    Log.Information("Please type the number of the id to start simulating.");
    var inputLine = Console.ReadLine();
    if (inputLine != null)
    {
        var startId = int.Parse(inputLine);
        var sim = db.Simulations.Include(b => b.Unit).Single(b => b.Id == startId);
        Log.Debug("THIS {@sim}", sim);
        var instance = up.GetInstance(sim.Unit.Id, db);
        if (instance != null)
        {
            Log.Information("running simulation on {@instance}", instance);
            var res = RunSimulationAt(instance, sim);
            log.Information("RunSimulationAt returned {res}", res);
        }
        else
        {
            Log.Error("No instance for your start_id {inputLine}", inputLine);
        }
    }
    else
    {
        Log.Error("No input received for simulation start id {inputLine}.", inputLine);
    }
}
else if (input == 't')
{
    up.GetArgsByUnit(db.SimulatedUnits.Where(b => b.Id == 5).First(), db);
}
else
{
    Log.Error("Invalid input. No such option {input}", input);
}

Log.Information("Finishing up. Good-bye!");

db.SaveChanges();

Log.CloseAndFlush();