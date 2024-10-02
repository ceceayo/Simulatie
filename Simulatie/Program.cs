using System;
using System.Linq;
using Simulatie;
using Serilog;

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
    return 0;
}

var x = new SimulatedUnit { Type = 1 };
Log.Debug("Created a SU {@x}.", x);
var o = db.SimulatedUnits.Add(x);
db.SaveChanges();
Log.Debug("SU changed to {@x}.", x);

IUnitType? instance = UP.GetInstance(x.Id, db);
IUnitType? instance_failing = UP.GetInstance(x.Id + 1, db);
Log.Information("Instance data {@instance}", instance);
Log.Information("Data of an instance which does not exist {@instance_failing}", instance_failing);