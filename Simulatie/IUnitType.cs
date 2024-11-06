namespace Simulatie
{
    public interface IUnitType
    {
        public int TypeNum { get; }
        public int Id { get; }
        public Dictionary<int, string> Arguments { get; }
        public UnitTickResponse? OnTick(SimulationDatabaseContext db, StatProvider sp, UnitProvider up, Simulation sim);
        public List<IUnitType> OnCreate(SimulationDatabaseContext db, StatProvider sp, UnitProvider up, Simulation sim);
    }
}
