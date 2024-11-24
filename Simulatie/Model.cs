using Microsoft.EntityFrameworkCore;

namespace Simulatie
{
    public class SimulationDatabaseContext : DbContext
    {
        /*
         * The SimulationDatabaseContext class represents the database context.
         * This means that all database models are stored in this file, and that
         * the class SimulationDatabaseContext is used to interact with the database.
         * This class takes care of getting tables and connecting the database.
         */
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

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
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
            //=> options.UseSqlite($"Data Source={DbPath}");
    }

    public class SimulatedUnit
    {
        /*
         * A SimulatedUnit is the basis of everything. It represents a unit in the simulation.
         * A unit can be a city, person, house, flat, etc.
         * Cities do not have an owner, but everything else does.
         * If something does not have an owner, the unit is either destroyed or the highest part of
         * the hierarchy.
         */
        public int Id { set; get; }
        public int Type { set; get; }

        public SimulatedUnit? Owner { set; get; }
    }

    public class UnitArgument
    {
        /*
         * Some units have more data attached to them. This data is stored in UnitArguments.
         */
        public int Id { set; get; }
        public SimulatedUnit Owner { set; get; }
        public int Type { set; get; }
        // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
        public string Value { set; get; } = string.Empty;
    }

    public class Simulation
    {
        /*
         * Some units have more data attached to them. This data is stored in UnitArguments.
         */
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