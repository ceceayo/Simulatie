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
    }
}