namespace Simulatie
{
    public class UnitTickResponse
    {
        public required IUnitType NewUnit;
        public required int ResourcesUsed;
        public List<IUnitType>? UnitsToAdd;
    }
}
