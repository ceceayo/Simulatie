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
            SimpleNumber? cafeterias_to_make = sp.FindInstance(db, 6 * 10000 + 2, 1, sim, "Total cafeterias in school") as SimpleNumber;
            SimpleNumber? classrooms_to_make = sp.FindInstance(db, 6 * 10000 + 3, 1, sim, "Total classrooms in school") as SimpleNumber;
            if (lamps_to_make == null)
            {
                Log.Fatal("Total lamps to make per school was not found.");
                throw new InvalidOperationException("Total lamps to make per school was not found.");
            }
            if (cafeterias_to_make == null)
            {
                Log.Fatal("Total cafetarias to make per school was not found.");
                throw new InvalidOperationException("Total cafetarias to make per school was not found.");
            }
            if (classrooms_to_make == null)
            {
                Log.Fatal("Total classrooms to make per school was not found.");
                throw new InvalidOperationException("Total classrooms to make per school was not found.");
            }
            List<IUnitType> child_creations = new List<IUnitType>();
            for (int i = 0; i < lamps_to_make.GetNumber(); i++)
            {
                HospitalLamp lamp = new HospitalLamp(args: new Dictionary<int, string>(), id: 0, owner: null);
                child_creations.Add(lamp);

            }
            for (int i = 0; i < cafeterias_to_make.GetNumber(); i++)
            {
                SchoolCafetaria cafeteria = new SchoolCafetaria(args: new Dictionary<int, string>(), id: 0, owner: null);
                child_creations.Add(cafeteria);

            }
            for (int i = 0; i < classrooms_to_make.GetNumber(); i++)
            {
                Classroom classroom = new Classroom(args: new Dictionary<int, string>(), id: 0, owner: null);
                child_creations.Add(classroom);

            }
            return child_creations;
        }
    }
}
