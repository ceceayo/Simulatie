using Serilog;
using Simulatie.StatTypes;

namespace Simulatie.UnitTypes
{
    public class School : IUnitType
    {
        public int TypeNum { get; } = 6;
        public int Id { get; set; }
        public Dictionary<int, string> Arguments { get; set; } = new Dictionary<int, string>();
        public IUnitType? Owner { get; set; }

        public School(int id, Dictionary<int, string> args, IUnitType? owner)
        {
            this.Id = id;
            this.Arguments = args;
            Owner = owner;
            Log.Debug("Create IUnitType instance of type School with value {val}.", this);
        }

        public UnitTickResponse? OnTick(SimulationDatabaseContext db, StatProvider sp, UnitProvider up, Simulation sim)
        {
            return new UnitTickResponse
            {
                NewUnit = new School(args: this.Arguments, id: this.Id, owner: Owner),
                ResourcesUsed = 0
            };
        }
        public List<IUnitType> OnCreate(SimulationDatabaseContext db, StatProvider sp, UnitProvider up, Simulation sim)
        {
            SimpleNumber? lamps_to_make = sp.FindInstance(db, 6*10000+1, 1, sim, "Total lamps in school") as SimpleNumber;
            if (lamps_to_make == null)
            {
                Log.Fatal("Total lamps to make per school was not found.");
                throw new InvalidOperationException("Total lamps to make per school was not found.");
            }
            List<IUnitType> child_creations = new List<IUnitType>();
            for (int i = 0; i < lamps_to_make.GetNumber(); i++)
            {
                Schoollamp lamp = new Schoollamp(args: new Dictionary<int, string>(), id: 0, owner: null);
                child_creations.Add(lamp);

            }
            return child_creations;
        }
    }
}
