using System;
using System.Linq;
using Simulatie;
using Serilog;

using var log = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
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

Console.WriteLine(UP.GetInstance(x.Id, db));
Console.WriteLine(UP.GetInstance(x.Id + 1, db)); // this will fail!