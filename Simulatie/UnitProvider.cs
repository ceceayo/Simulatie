using Serilog;
using Simulatie.UnitTypes;

namespace Simulatie
{
    public class UnitProvider
    {
        public Dictionary<int, Type> Types = new Dictionary<int, Type>
        {
            { 1, typeof(City) },
            { 2, typeof(House) }
        };

        public UnitProvider()
        {
            Log.Debug("Created a unitprovider");
        }

        public Type? GetType(int type)
        {
            if (Types.ContainsKey(type))
            {
                return Types[type];
            }
            return null;
        }

        public IUnitType? GetInstance(int id, SimulationDatabaseContext db)
        {
            var o = db.SimulatedUnits.Find(id);
            if (o == null) { return null; }
            if (Types.ContainsKey(o.Type))
            {
                var t = Types[o.Type];
                var i = Activator.CreateInstance(t, [id, new Dictionary<int, string>()]) as IUnitType;
                return i;
            }
            return null;
        }
        public int MakeInstance(IUnitType unit, SimulationDatabaseContext db, IUnitType? owner)
        {
            Log.Information("Making instance in db of @{unit}, owned by @{owner}", unit, owner);
            SimulatedUnit x;
            if (owner == null)
            {
                Log.Information("Owner is null");
                x = new SimulatedUnit { Type = unit.TypeNum, Owner = null };
            }
            else
            {
                Log.Information("Owner is not null");
                x = new SimulatedUnit { Type = unit.TypeNum, Owner = db.SimulatedUnits.Find(owner.Id) };
            }
            db.SimulatedUnits.Add(x);
            Log.Information("Added unit to db");
            db.SaveChanges();
            Log.Information("Saved changes");
            Log.Information("Id of new unit is {x.Id}", x);
            return x.Id;
        }
        public List<IUnitType> GetAllOwnedBy(IUnitType owner, SimulationDatabaseContext db)
        {
            var x = db.SimulatedUnits.Where(b => b.Owner == db.SimulatedUnits.Find(owner.Id)).ToList();
            List<IUnitType> y = new List<IUnitType>();
            Log.Debug("x is {@x}", x);
            foreach (var z in x)
            {
                if (z is SimulatedUnit)
                {
                    y.Add(this.GetInstance(z.Id, db) ?? throw new InvalidOperationException());
                } else { Log.Error("z is not a SimulatedUnit"); }
            }
            return y;
        }
    }
}
