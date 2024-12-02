using Microsoft.EntityFrameworkCore;

namespace Simulatie
{
    public class SimulationDatabaseContext : DbContext
    {
        public DbSet<SimulatedUnit> SimulatedUnits { get; set; }
        public DbSet<UnitArgument> UnitArguments { get; set; }
        public DbSet<Simulation> Simulations { get; set; }
        public DbSet<Stats> Statistics { get; set; }

        public string DbPath { get; }

        public SimulationDatabaseContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "simulatie.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (Environment.GetEnvironmentVariable("USE_DATABASE") == "POSTGRES")
            {
                throw new NotImplementedException();
            }
            else
            {
                options.UseSqlite($"Data Source={DbPath}");
            }
        }
    }

    public class SimulatedUnit
    {
        public int Id { set; get; }
        public int Type { set; get; }
        public SimulatedUnit? Owner { set; get; }
        public int ResourcesUsedLastRound { set; get; }
        public int ResourcesUsedLastRoundRecursive { set; get; }
    }

    public class UnitArgument
    {
        public int Id { set; get; }
        public SimulatedUnit Owner { set; get; }
        public int Type { set; get; }
        public string Value { set; get; } = string.Empty;
    }

    public class Simulation
    {
        public int Id { set; get; }
        public SimulatedUnit Unit { set; get; }
        public int TotalResourcesUsed { set; get; }
        public Int16 Hour { set; get; }
        public Int32 Day { set; get; }
        public int Year { set; get; }
    }

    public class Stats
    {
        public int Id { set; get; }
        public int Type { set; get; }
        public int Role { set; get; }
        public string Value { set; get; }
        public Simulation Owner { set; get; }
    }
}
