using Serilog;
using Simulatie.StatTypes;

namespace Simulatie.UnitTypes
{
    public class Classroom : IUnitType
    {
        public int TypeNum { get; } = 9;
        public int Id { get; set; }
        public Dictionary<int, string> Arguments { get; set; } = new Dictionary<int, string>();
        public IUnitType? Owner { get; set; }

        public Classroom(int id, Dictionary<int, string> args, IUnitType? owner)
        {
            this.Id = id;
            this.Arguments = args;
            Owner = owner;
            Log.Debug("Create IUnitType instance of type Classroom with value {val}.", this);
        }

        public UnitTickResponse? OnTick(SimulationDatabaseContext db, StatProvider sp, UnitProvider up, Simulation sim)
        {
            return new UnitTickResponse
            {
                NewUnit = new Classroom(args: this.Arguments, id: this.Id, owner: Owner),
                ResourcesUsed = 0
            };
        }
        public List<IUnitType> OnCreate(SimulationDatabaseContext db, StatProvider sp, UnitProvider up, Simulation sim)
        {
            SimpleNumber? schoollamps_to_make = sp.FindInstance(db, 9*10000+1, 1, sim, "Total schoollamps in classroom") as SimpleNumber;
            if (schoollamps_to_make == null)
            {
                Log.Fatal("Total schoollamps to make per classroom was not found.");
                throw new InvalidOperationException("Total schoollamps to make per classroom was not found.");
            }
            List<IUnitType> child_creations = new List<IUnitType>();
            for (int i = 0; i < schoollamps_to_make.GetNumber(); i++)
            {
                HospitalLamp schoollamp = new HospitalLamp(args: new Dictionary<int, string>(), id: 0, owner: null);
                child_creations.Add(schoollamp);
            }
            return child_creations;
        }
    }
}
