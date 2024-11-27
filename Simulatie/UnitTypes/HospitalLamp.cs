using Serilog;
using Simulatie.StatTypes;

namespace Simulatie.UnitTypes
{
    public class HospitalLamp : IUnitType
    {
        public int TypeNum { get; } = 14;
        public int Id { get; set; }
        public Dictionary<int, string> Arguments { get; set; } = new Dictionary<int, string>();
        public IUnitType? Owner { get; set; }
        public HospitalLamp(int id, Dictionary<int, string> args, IUnitType? owner)
        {
            this.Id = id;
            this.Arguments = args;
            Owner = owner;
            Log.Debug("Create IUnitType instance of type HospitalLamp with value {val}.", this);
        }

        public UnitTickResponse? OnTick(SimulationDatabaseContext db, StatProvider sp, UnitProvider up, Simulation sim)
        {
            SimpleNumber? statInstance = sp.FindInstance(db, 7*10000+1, 1, sim, "Electricity used by lamps in hospital") as SimpleNumber;
            if (sim.Day % 7 < 6 && sim.Hour > 8 && sim.Hour < 17)
            {
                return new UnitTickResponse
                {
                    NewUnit = new HospitalLamp(args: this.Arguments, id: this.Id, owner: Owner),
                    ResourcesUsed = statInstance != null ? int.Parse(statInstance.Value) : 0 // Assuming IStatType has an Id property
                };
            }
            else
            {
                return new UnitTickResponse
                {
                    NewUnit = new HospitalLamp(args: this.Arguments, id: this.Id, owner: Owner),
                    ResourcesUsed = 0
                };
            }
        }
        public List<IUnitType> OnCreate(SimulationDatabaseContext db, StatProvider sp, UnitProvider up, Simulation sim)
        {
            return new List<IUnitType> { };
        }
    }
}
