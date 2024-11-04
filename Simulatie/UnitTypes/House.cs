namespace Simulatie.UnitTypes
{
    public class House : IUnitType
    {
        public int TypeNum { get; } = 2;
        public int Id { get; set; }
        public Dictionary<int, string> Arguments { get; set; } = new Dictionary<int, string>();

        public House(int id, Dictionary<int, string> args)
        {
            this.Id = id;
        }

        public UnitTickResponse? OnTick(SimulationDatabaseContext db, StatProvider sp, UnitProvider up)
        {
            return new UnitTickResponse
            {
                NewUnit = new House(args: this.Arguments, id: this.Id),
                ResourcesUsed = 4
            };
        }
    }
}
