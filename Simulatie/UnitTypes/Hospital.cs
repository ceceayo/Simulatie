using Serilog;
using Simulatie.StatTypes;

namespace Simulatie.UnitTypes
{
    public class Hospital : IUnitType
    {
        public int TypeNum { get; } = 12;
        public int Id { get; set; }
        public Dictionary<int, string> Arguments { get; set; } = new Dictionary<int, string>();
        public IUnitType? Owner { get; set; }
        public Hospital(int id, Dictionary<int, string> args, IUnitType? owner)
        {
            this.Id = id;
            this.Arguments = args;
            Owner = owner;
            Log.Debug("Create IUnitType instance of type Hospital with value {val}.", this);
        }

        public UnitTickResponse? OnTick(SimulationDatabaseContext db, StatProvider sp, UnitProvider up, Simulation sim)
        {
            SimpleNumber? statInstance = sp.FindInstance(db, 2, 1, sim, "Electricity used by Hospital") as SimpleNumber;
            return new UnitTickResponse
            {
                NewUnit = new Lamp(args: this.Arguments, id: this.Id, owner: Owner),
                ResourcesUsed = statInstance != null ? int.Parse(statInstance.Value) : 0 // Assuming IStatType has an Id property
            };
        }
        public List<IUnitType> OnCreate(SimulationDatabaseContext db, StatProvider sp, UnitProvider up, Simulation sim)
        {
            SimpleNumber? total_hospitallamps_to_make = sp.FindInstance(db, 12 * 10000 + 1, 1, sim, "Total houses to make") as SimpleNumber;
            if (total_hospitallamps_to_make == null)
            {
                Log.Fatal("Total hospitallamps to make was not found.");
                throw new InvalidOperationException("Total hospitallamps to make not found.");
            }
            List<IUnitType> child_creations = new List<IUnitType>();
            for (int i = 0; i < total_hospitallamps_to_make.GetNumber(); i++)
            {
                HospitalLamp hospitallamp = new HospitalLamp(args: new Dictionary<int, string>(), id: 0, owner: this);
                child_creations.Add(hospitallamp);
            }
            return child_creations;
        }
    }
}
