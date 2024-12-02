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
            Id = id;
            Arguments = args;
            Owner = owner;
            Log.Debug("Create IUnitType instance of type City with value {val}.", this);
        }

        public UnitTickResponse? OnTick(SimulationDatabaseContext db, StatProvider sp, UnitProvider up, Simulation sim)
        {
            return new UnitTickResponse
            {
                NewUnit = new City(args: Arguments, id: Id, owner: Owner),
                ResourcesUsed = 0
            };
        }

        public List<IUnitType> OnCreate(SimulationDatabaseContext db, StatProvider sp, UnitProvider up, Simulation sim)
        {
            SimpleNumber? totalHousesToMake = sp.FindInstance(db, 1 * 10000 + 1, 1, sim, "Total houses to make") as SimpleNumber;
            if (totalHousesToMake == null)
            {
                Log.Fatal("Total houses to make was not found.");
                throw new InvalidOperationException("Total houses to make not found.");
            }

            List<IUnitType> childCreations = new List<IUnitType>();
            for (int i = 0; i < totalHousesToMake.GetNumber(); i++)
            {
                House house = new House(args: new Dictionary<int, string>(), id: 0, owner: this);
                childCreations.Add(house);
            }

            SimpleNumber? totalSchoolsToMake = sp.FindInstance(db, 1 * 10000 + 2, 1, sim, "Total schools to make") as SimpleNumber;
            if (totalSchoolsToMake == null)
            {
                Log.Fatal("Total schools to make was not found.");
                throw new InvalidOperationException("Total schools to make not found.");
            }

            for (int i = 0; i < totalSchoolsToMake.GetNumber(); i++)
            {
                School school = new School(args: new Dictionary<int, string>(), id: 0, owner: this);
                childCreations.Add(school);
            }

            SimpleNumber? totalHospitalsToMake = sp.FindInstance(db, 1 * 10000 + 3, 1, sim, "Total hospitals to make") as SimpleNumber;
            if (totalHospitalsToMake == null)
            {
                Log.Fatal("Total hospitals to make was not found.");
                throw new InvalidOperationException("Total hospitals to make not found.");
            }

            for (int i = 0; i < totalHospitalsToMake.GetNumber(); i++)
            {
                Hospital hospital = new Hospital(args: new Dictionary<int, string>(), id: 0, owner: this);
                childCreations.Add(hospital);
            }

            return childCreations;
        }
    }
}
