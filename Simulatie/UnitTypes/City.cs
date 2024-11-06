using Serilog;
using Simulatie.StatTypes;

namespace Simulatie.UnitTypes
{
    public class City : IUnitType
    {
        public int TypeNum { get; } = 1;
        public int Id { get; set; }
        public Dictionary<int, string> Arguments { get; set; }

        public City(int id, Dictionary<int, string> args)
        {
            this.Id = id;
            this.Arguments = args;
        }

        public UnitTickResponse? OnTick(SimulationDatabaseContext db, StatProvider sp, UnitProvider up, Simulation sim)
        {
            return new UnitTickResponse
            {
                NewUnit = new City(args: this.Arguments, id: this.Id),
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
                House house = new House(args: new Dictionary<int, string>(), id: 0);
                
                List<IUnitType> children = house.OnCreate(db, sp, up, sim);
                child_creations.Add(house);
                child_creations.AddRange(children);

            }
            return child_creations;
        }
    }
}