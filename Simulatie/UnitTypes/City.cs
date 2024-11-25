using Serilog;
using Simulatie.StatTypes;

namespace Simulatie.UnitTypes
{
    public class City : IUnitType
    {
        public int TypeNum { get; } = 1;
        public int Id { get; set; }
        public Dictionary<int, string> Arguments { get; set; }
        public IUnitType? Owner { get; set; }
        public City(int id, Dictionary<int, string> args, IUnitType? owner)
        {
            this.Id = id;
            this.Arguments = args;
            Owner = owner;
            Log.Debug("Create IUnitType instance of type City with value {val}.", this);
        }

        public UnitTickResponse? OnTick(SimulationDatabaseContext db, StatProvider sp, UnitProvider up, Simulation sim)
        {
            return new UnitTickResponse
            {
                NewUnit = new City(args: this.Arguments, id: this.Id, owner: Owner),
                ResourcesUsed = 0
            };
        }
        public List<IUnitType> OnCreate(SimulationDatabaseContext db, StatProvider sp, UnitProvider up, Simulation sim)
        {
            SimpleNumber? total_houses_to_make = sp.FindInstance(db, 3, 1, sim, "Total houses to make") as SimpleNumber;
            if (total_houses_to_make == null)
            {
                Log.Fatal("Total houses to make was not found.");
                throw new InvalidOperationException("Total houses to make not found.");
            }
            List<IUnitType> child_creations = new List<IUnitType>();
            for (int i = 0; i < total_houses_to_make.GetNumber(); i++)
            {
                House house = new House(args: new Dictionary<int, string>(), id: 0, owner: this);
                child_creations.Add(house);

            }
            SimpleNumber? total_schools_to_make = sp.FindInstance(db, 5, 1, sim, "Total schools to make") as SimpleNumber;
            if (total_schools_to_make == null)
            {
                Log.Fatal("Total schools to make was not found.");
                throw new InvalidOperationException("Total schools to make not found.");
            }
            for (int i = 0; i < total_schools_to_make.GetNumber(); i++)
            {
                School school = new School(args: new Dictionary<int, string>(), id: 0, owner: this);
                child_creations.Add(school);

            }
            SimpleNumber? total_hospitals_to_make = sp.FindInstance(db, 5, 1, sim, "Total schools to make") as SimpleNumber;
            if (total_hospitals_to_make == null)
            {
                Log.Fatal("Total hospitals to make was not found.");
                throw new InvalidOperationException("Total hospitals to make not found.");
            }
            for (int i = 0; i < total_hospitals_to_make.GetNumber(); i++)
            {
                Hospital hospital = new Hospital(args: new Dictionary<int, string>(), id: 0, owner: this);
                child_creations.Add(hospital);

            }
            return child_creations;
        }
    }
}