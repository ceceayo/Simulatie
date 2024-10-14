using Simulatie;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

var folder = Environment.SpecialFolder.LocalApplicationData;
var path = Environment.GetFolderPath(folder);
var logPath = Path.Join(path, "simulatie.log");

using var log = new LoggerConfiguration()
    .MinimumLevel.Debug()
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

int CreateSimulation()
{
    Log.Information("I will now be creating a new simulation, meaning a city will be created and populated");
    var x = new SimulatedUnit { Type = 1, Owner = null };
    Log.Debug("Created a new simulated unit {@x}", x);
    db.SimulatedUnits.Add(x);
    db.SaveChanges();
    var id = x.Id;
    Log.Information("Added unit to db with Id {id}", id);
    Log.Information("Make a new House");
    var y = new SimulatedUnit { Type = 2, Owner = db.SimulatedUnits.Find(id) };
    Log.Information("Created a new simulated unit {@y}", y);
    db.SimulatedUnits.Add(y);
    db.SaveChanges();
    id = y.Id;
    Log.Information("Added unit to db, with id {id}", id);
    return x.Id;
}

RunSimulationRecursiveResult RunSimulationRecursive(IUnitType unit)
{
    Log.Information("Calling RunSimulationRecursive on unit {@unit}", unit);
    int TotalPowerUsed = 0;
    List<IUnitType> new_units = new List<IUnitType>();
    var result = unit.OnTick(db);
    TotalPowerUsed += result.ResourcesUsed;
    new_units.Add(result.NewUnit);
    Log.Information("Running first step of simulation used {power} watts", result.ResourcesUsed);
    var children = up.GetAllOwnedBy(unit, db);
    Log.Information("Children of {Id} are {@children}", unit.Id, children);
    foreach (var child in children)
    {
        // TODO: rename
        RunSimulationRecursiveResult john = RunSimulationRecursive(child);
        TotalPowerUsed += john.ResourcesUsed;
        foreach (var NewUnit in john.NewUnits)
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

int RunSimulationAt(IUnitType start)
{
    Log.Information("Running simulation at {@start}", start);
    RunSimulationRecursiveResult result = RunSimulationRecursive(start);
    Log.Information("Recursive simulation has ended with {power} watt.", result.ResourcesUsed);
    return 0;
}

Log.Information("Waiting for OPTION to be selected. [c]reate sim., [r]un sim.");
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
        var instance = up.GetInstance(startId, db);
        if (instance != null)
        {
            Log.Information("running simulation on {@instance}", instance);
            var res = RunSimulationAt(instance);
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
else
{
    Log.Error("Invalid input. No such option {input}", input);
}

Log.Information("Finishing up. Good-bye!");

db.SaveChanges();

Log.CloseAndFlush();