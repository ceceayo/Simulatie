using Simulatie;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Microsoft.EntityFrameworkCore;

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

Log.Information("Waiting for OPTION to be selected. [c]reate sim., [r]un simulation. [m]igrate.");
char input = Console.ReadKey().KeyChar;
Console.WriteLine();

Simulator simulator = new Simulator();

Log.Information("Database path is {DbPath}.", simulator.db.DbPath);

if (input == 'm')
{
    simulator.db.Database.Migrate();
} 
else if (input == 'c')
{
    var res = simulator.CreateSimulation();
    Log.Information("Finished creating simulation with id {res}", res);
}
else if (input == 'r')
{
    Log.Information("Please type the number of the id to start simulating.");
    var inputLine = Console.ReadLine();
    if (inputLine != null)
    {
        var startId = int.Parse(inputLine);
        var sim = simulator.db.Simulations.Include(b => b.Unit).Single(b => b.Id == startId);
        Log.Debug("THIS {@sim}", sim);
        var instance = simulator.up.GetInstance(sim.Unit.Id, simulator.db);
        if (instance != null)
        {
            //Log.Information("running simulation on {@instance}", instance);
            var res = simulator.RunSimulationAt(instance, sim);
            //log.Information("RunSimulationAt returned {res}", res);
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

simulator.CleanUp();

Log.CloseAndFlush();
