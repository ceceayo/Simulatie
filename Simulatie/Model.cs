using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public string DbPath { get; }

        public SimulationDatabaseContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "simulatie.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
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
        public UnitArgument[] Arguments { set; get; } = Array.Empty<UnitArgument>();
    }

    public class UnitArgument
    {
        /*
         * Some units have more data attached to them. This data is stored in UnitArguments.
         */
        public int Id { set; get; }
        public int Type { set; get; }
        public string Value { set; get; } = string.Empty;
    }
}