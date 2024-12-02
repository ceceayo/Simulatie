using Serilog;
using Simulatie.StatTypes;

namespace Simulatie.UnitTypes
{
    public class Dryer : IUnitType
    {
        public int TypeNum { get; } = 17;
        public int Id { get; set; }
        public Dictionary<int, string> Arguments { get; set; } = new Dictionary<int, string>();
        public IUnitType Owner { get; set; }
        public Dryer(int id, Dictionary<int, string> args, IUnitType owner)
        {
            this.Id = id;
            this.Arguments = args;
            Owner = owner;
            Log.Debug("Create IUnitType instance of type Refrigerator with value {val}.", this);
        }

        public UnitTickResponse? OnTick(SimulationDatabaseContext db, StatProvider sp, UnitProvider up, Simulation sim)
        {
            SimpleNumber statInstance = sp.FindInstance(db, 17*10000+1, 1, sim, "Electricity used by dryer(s)") as SimpleNumber;
            return new UnitTickResponse
            {
                NewUnit = new Refrigerator(args: this.Arguments, id: this.Id, owner: Owner),
                ResourcesUsed = statInstance != null ? statInstance.GetNumber() : 700 // Assuming IStatType has an Id property
            };
        }
        public List<IUnitType> OnCreate(SimulationDatabaseContext db, StatProvider sp, UnitProvider up, Simulation sim)
        {
            return new List<IUnitType> { };
        }
    }
}
