namespace Simulatie.UnitTypes
{
    public class Freezer : IUnitType
    {
        public int TypeNum { get; } = 4;
        public int Id { get; set; }
        public Dictionary<int, string> Arguments { get; set; } = new Dictionary<int, string>();

        public Freezer(int id, Dictionary<int, string> args)
        {
            this.Id = id;
        }

        public UnitTickResponse? OnTick(SimulationDatabaseContext db, StatProvider sp, UnitProvider up, Simulation sim)
        {
            var statInstance = sp.FindInstance(db, 2, 1, sim, "Electricity used by freezer(s)");
            return new UnitTickResponse
            {
                NewUnit = new Freezer(args: this.Arguments, id: this.Id),
                ResourcesUsed = statInstance != null ? statInstance.Id : 0 // Assuming IStatType has an Id property
            };
        }
    }
}
