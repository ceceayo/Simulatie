﻿namespace Simulatie.UnitTypes
{
    public class Lamp : IUnitType
    {
        public int TypeNum { get; } = 3;
        public int Id { get; set; }
        public Dictionary<int, string> Arguments { get; set; } = new Dictionary<int, string>();

        public Lamp(int id, Dictionary<int, string> args)
        {
            this.Id = id;
        }

        public UnitTickResponse? OnTick(SimulationDatabaseContext db, StatProvider sp, UnitProvider up, Simulation sim)
        {
            var statInstance = sp.FindInstance(db, 2, 1, sim, "Electricity used by lamps");
            return new UnitTickResponse
            {
                NewUnit = new Lamp(args: this.Arguments, id: this.Id),
                ResourcesUsed = statInstance != null ? statInstance.Id : 0 // Assuming IStatType has an Id property
            };
        }
    }
}
