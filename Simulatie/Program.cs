using System;
using System.Linq;
using Simulatie;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Simulatie.UnitTypes;

var folder = Environment.SpecialFolder.LocalApplicationData;
var path = Environment.GetFolderPath(folder);
var LogPath = System.IO.Path.Join(path, "simulatie.log");

using var log = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .WriteTo.Debug()
    .WriteTo.File(LogPath)
    .CreateLogger();
Log.Logger = log;

using var db = new SimulationDatabaseContext();
Log.Information("Created a database {db}", db);

// Note: This sample requires the database to be created before running.
Log.Information("Database path is {DbPath}.", db.DbPath);

var UP = new UnitProvider();

int create_simulation()
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

int run_simulation_at(IUnitType start)
{
    Log.Information("Running simulation at {@start}", start);
    var children = UP.GetAllOwnedBy(start, db);
    Log.Information("Children of {Id} are {@children}", start.Id, children);
    return 0;
}

Log.Information("Waiting for OPTION to be selected. [c]reate sim., [r]un sim.");
char input = Console.ReadKey().KeyChar;
Console.WriteLine();

if (input == 'c')
{
    var res = create_simulation();
    Log.Information("Finished creating simulation with id {res}", res);
}
else if (input == 'r')
{
    Log.Information("Please type the number of the id to start simulating.");
    var inputLine = Console.ReadLine();
    if (inputLine != null)
    {
        var start_id = int.Parse(inputLine);
        var instance = UP.GetInstance(start_id, db);
        if (instance != null)
        {
            Log.Information("running simulation on {@instance}", instance);
            var res = run_simulation_at(instance);
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
Log.CloseAndFlush();