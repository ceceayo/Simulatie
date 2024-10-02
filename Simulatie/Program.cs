using System;
using System.Linq;
using Simulatie;

using var db = new SimulationDatabaseContext();

// Note: This sample requires the database to be created before running.
Console.WriteLine($"Database path: {db.DbPath}.");

var UP = new UnitProvider();

var x = new SimulatedUnit { Type = 1 };
var o = db.SimulatedUnits.Add(x);
db.SaveChanges();

Console.WriteLine(UP.GetInstance(x.Id, db));
Console.WriteLine(UP.GetInstance(x.Id + 1, db)); // this will fail!