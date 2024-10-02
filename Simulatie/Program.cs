using System;
using System.Linq;
using Simulatie;
using Serilog;

using var log = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.Debug()
    .CreateLogger();
Log.Logger = log;

using var db = new SimulationDatabaseContext();

// Note: This sample requires the database to be created before running.
Log.Information("Database path is {DbPath}.", db.DbPath);

var UP = new UnitProvider();

var x = new SimulatedUnit { Type = 1 };
Log.Debug("Created a SU {@x}.", x);
var o = db.SimulatedUnits.Add(x);
db.SaveChanges();
Log.Debug("SU changed to {@x}.", x);

IUnitType? instance = UP.GetInstance(x.Id, db);
IUnitType? instance_failing = UP.GetInstance(x.Id + 1, db);
Log.Information("Instance data {@instance}", instance);
Log.Information("Data of an instance which does not exist {@instance_failing}", instance_failing);