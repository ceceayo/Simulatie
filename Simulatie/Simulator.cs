using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace Simulatie
{
    public class Simulator
    {
        public SimulationDatabaseContext db;
        public UnitProvider up;
        public StatProvider sp;
        public Simulator()
        {
            Log.Information("Created a Simulator Object.");
            db = new SimulationDatabaseContext();
            up = new UnitProvider();
            sp = new StatProvider();

            
        }

        internal void save_db(SimulationDatabaseContext db)
        {
            while (true)
            {
                try
                {
                    db.SaveChanges();
                    return;
                }
                catch
                {
                    Log.Error("Could not save database. Trying again in 1 seconds.");
                    System.Threading.Thread.Sleep(1000);
                    continue;
                }
            }
        }

        public int CreateSimulation()
        {
            Log.Information("I will now be creating a new simulation, meaning a city will be created and populated");
            SimulatedUnit rootCity = new SimulatedUnit { Type = 1, Owner = null };
            db.SimulatedUnits.Add(rootCity);
            save_db(db);
            Simulation sim = new Simulation { TotalResourcesUsed = 0, Unit = rootCity, Year = 2000, Day = 0, Hour = 11 };
            db.Simulations.Add(sim);
            save_db(db);
            Log.Information("I have created a city ({@city}) and a simulation ({@sim}).", rootCity, sim);
            List<IUnitType> children = up.GetInstance(rootCity.Id, db)!.OnCreate(db, sp, up, sim);
            Queue<CreateSimulationItem> items = new Queue<CreateSimulationItem>();
            foreach (var child in children)
            {
                Log.Debug("Saving child {@child}", child);
                int NewUnitId = up.MakeInstance(child, db, child.Owner);
                Log.Debug("Child got Id {Id}", NewUnitId);
                IUnitType newUnit = up.GetInstance(NewUnitId, db)!;
                List<IUnitType> newChildren = newUnit.OnCreate(db, sp, up, sim);
                foreach (var newChild in newChildren)
                {
                    items.Enqueue(new CreateSimulationItem(Unit: newChild, OwnerId: newUnit.Id));
                }
            }
            while (items.Count > 0)
            {
                CreateSimulationItem item = items.Dequeue();
                Log.Debug("Saving item {@item}", item);
                int NewUnitId = up.MakeInstance(item.Unit, db, up.GetInstance(item.OwnerId, db));
                Log.Debug("Item got Id {Id}", NewUnitId);
                IUnitType newUnit = up.GetInstance(NewUnitId, db)!;
                List<IUnitType> newChildren = newUnit.OnCreate(db, sp, up, sim);
                foreach (var newChild in newChildren)
                {
                    items.Enqueue(new CreateSimulationItem(Unit: newChild, OwnerId: newUnit.Id));
                }
            }
            return sim.Id;
        }


        RunSimulationRecursiveResult RunSimulationRecursive(IUnitType unit, Simulation sim)
        {
            Log.Information("Calling RunSimulationRecursive on unit {@unit}", unit);
            int TotalPowerUsed = 0;
            List<IUnitType> new_units = new List<IUnitType>();
            var q = db.SimulatedUnits.Where(b => b.Id == unit.Id).First();
            Log.Debug("Database has object {@obj} stored.", q);
            var result = unit.OnTick(db, sp, up, sim);
            SimulatedUnit unitToSetUsedResourcesOf = db.SimulatedUnits.Find(unit.Id);
            unitToSetUsedResourcesOf.ResourcesUsedLastRound = result.ResourcesUsed;
            Log.Information("Setting resources used of unit {Id} to {power} watts", unit.Id, result.ResourcesUsed);
            save_db(db);
            TotalPowerUsed += result.ResourcesUsed;
            new_units.Add(result.NewUnit);
            Log.Information("Running first step of simulation used {power} watts", result.ResourcesUsed);
            var children = up.GetAllOwnedBy(unit, db);
            Log.Information("Children of {Id} are {@children}", unit.Id, children);
            foreach (var child in children)
            {
                RunSimulationRecursiveResult ResultOfChild = RunSimulationRecursive(child, sim);
                TotalPowerUsed += ResultOfChild.ResourcesUsed;
                foreach (var NewUnit in ResultOfChild.NewUnits)
                {
                    new_units.Add(NewUnit);
                }
            }
            SimulatedUnit unitToSetRecursiveUsedResourcesOf = db.SimulatedUnits.Find(unit.Id);
            unitToSetRecursiveUsedResourcesOf.ResourcesUsedLastRoundRecursive = TotalPowerUsed;
            save_db(db);
            return new RunSimulationRecursiveResult
            {
                NewUnits = new_units,
                ResourcesUsed = TotalPowerUsed
            };
        }

        public int RunSimulationAt(IUnitType start, Simulation sim)
        {
            Log.Information("Running simulation at {@start}", start);
            RunSimulationRecursiveResult result = RunSimulationRecursive(start, sim);
            Log.Information("Recursive simulation has ended with {power} watt.", result.ResourcesUsed);
            if (sim.Hour == 23)
            {
                sim.Hour = 0;
                if (sim.Day == 364)
                {
                    sim.Day = 0;
                    sim.Year = sim.Year + 1;
                }
                else
                {
                    sim.Day += 1;
                }
            }
            else
            {
                sim.Hour += 1;
            }
            save_db(db);
            Log.Debug("Saving new units from database.");
            foreach (var unit in result.NewUnits)
            {
                Log.Debug("Saving unit {@unit}", unit);
                up.SaveUnit(unit, db);
            }
            var x = db.Simulations.Find(sim.Id);
            if (x == null)
            {
                Log.Fatal("Could not find simulation with id {id}", sim.Id);
                throw new Exception("Could not find simulation with id");
            }
            else
            {
                x.TotalResourcesUsed += result.ResourcesUsed;
                save_db(db);
            }
            return result.ResourcesUsed;
        }

        public void CleanUp()
        {
            save_db(db);
            db.Dispose();
            Log.Information("So long and thanks for all the simulations. And fish.");
        }
    }
}
