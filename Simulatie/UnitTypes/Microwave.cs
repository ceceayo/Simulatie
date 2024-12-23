﻿using Serilog;
using Simulatie.StatTypes;

namespace Simulatie.UnitTypes
{
    public class Microwave : IUnitType
    {
        public int TypeNum { get; } = 11;
        public int Id { get; set; }
        public Dictionary<int, string> Arguments { get; set; } = new Dictionary<int, string>();
        public IUnitType? Owner { get; set; }
        public Microwave(int id, Dictionary<int, string> args, IUnitType? owner)
        {
            this.Id = id;
            this.Arguments = args;
            Owner = owner;
            Log.Debug("Create IUnitType instance of type Microwave with value {val}.", this);
        }

        public UnitTickResponse? OnTick(SimulationDatabaseContext db, StatProvider sp, UnitProvider up, Simulation sim)
        {
            SimpleNumber? statInstance = sp.FindInstance(db, 11 * 10000 + 1, 1, sim, "Electricity used by microwave(s)") as SimpleNumber;
            return new UnitTickResponse
            {
                NewUnit = new Microwave(args: this.Arguments, id: this.Id, owner: Owner),
                ResourcesUsed = statInstance != null ? statInstance.GetNumber() : 0 // Assuming IStatType has an Id property
            };
        }
        public List<IUnitType> OnCreate(SimulationDatabaseContext db, StatProvider sp, UnitProvider up, Simulation sim)
        {
            return new List<IUnitType> { };
        }
    }
}
