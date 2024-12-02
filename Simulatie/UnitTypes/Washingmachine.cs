﻿using Serilog;
using Simulatie.StatTypes;

namespace Simulatie.UnitTypes
{
    public class Washingmachine : IUnitType
    {
        public int TypeNum { get; } = 16;
        public int Id { get; set; }
        public Dictionary<int, string> Arguments { get; set; } = new Dictionary<int, string>();
        public IUnitType? Owner { get; set; }
        public Washingmachine(int id, Dictionary<int, string> args, IUnitType? owner)
        {
            this.Id = id;
            this.Arguments = args;
            Owner = owner;
            Log.Debug("Create IUnitType instance of type Washingmachine with value {val}.", this);
            
        }

        public UnitTickResponse? OnTick(SimulationDatabaseContext db, StatProvider sp, UnitProvider up, Simulation sim)
        {
            int power_usage;
            if (Arguments.Keys.Contains(1))
            {
                power_usage = int.Parse(Arguments[1]);
            }
            else
            {
                Distribution? statInstance =
                    sp.FindInstance(db, 16 * 10000 + 1, 2, sim, "Electricity used by washingmachines") as Distribution;
                Arguments[1] = statInstance?.GetNumber().ToString() ?? "0";
                power_usage = int.Parse(Arguments[1]);
            }

            return new UnitTickResponse
            {
                NewUnit = new Washingmachine(args: this.Arguments, id: this.Id, owner: Owner),
                ResourcesUsed = power_usage // Assuming IStatType has an Id property
            };
        }
        public List<IUnitType> OnCreate(SimulationDatabaseContext db, StatProvider sp, UnitProvider up, Simulation sim)
        {
            return new List<IUnitType> { };
        }
    }
}