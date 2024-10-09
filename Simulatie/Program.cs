using System;
using System.Linq;
using Simulatie;
using Serilog;
using Simulatie.UnitTypes;

var folder = Environment.SpecialFolder.LocalApplicationData;
var path = Environment.GetFolderPath(folder);
var LogPath = System.IO.Path.Join(path, "simulatie.log");

using var log = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
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
    Log.Debug("Created a new simulated unit @{x}", x);
    db.SimulatedUnits.Add(x);
    db.SaveChanges();
    var id = x.Id;
    Log.Information("Added unit to db with Id {id}", id);
    return 0;
}

Log.Information("Waiting for OPTION to be selected. [c]reate simulation");
char input = Console.ReadKey().KeyChar;
Console.WriteLine();

if (input == 'c')
{
    create_simulation();
}
else
{
    Log.Error("Invalid input. No such option {input}", input);
}