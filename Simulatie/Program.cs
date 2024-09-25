using System;
using System.Linq;

using var db = new SimulationDatabaseContext();

// Note: This sample requires the database to be created before running.
Console.WriteLine($"Database path: {db.DbPath}.");

