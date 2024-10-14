namespace Simulatie
{
    public interface IUnitType
    {
        public int TypeNum { get; }
        public int Id { get; }
        public Dictionary<int, string> Arguments { get; }
        public UnitTickResponse? OnTick(SimulationDatabaseContext db);
    }
}
