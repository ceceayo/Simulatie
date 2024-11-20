using Microsoft.EntityFrameworkCore;
using Serilog;
using Simulatie.UnitTypes;

namespace Simulatie
{
    public class UnitProvider
    {
        public Dictionary<int, Type> Types = new Dictionary<int, Type>
        {
            { 1, typeof(City) },
            { 2, typeof(House) },
            { 3, typeof(Lamp) },
            { 4, typeof(Freezer) },
            { 5, typeof(Refrigerator) },
            { 6, typeof(School) },
            { 7, typeof(Schoollamp) },
            { 8, typeof(Schoolcafetaria) },
            { 9, typeof(Classroom) },
            { 10, typeof(Phonecharger) },
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
                IUnitType? parent = null;
                SimulatedUnit? owner = db.SimulatedUnits.Include(b => b.Owner).Single(b => b.Id == id);
                if (owner.Owner == null) { }
                else
                {
                    parent = GetInstance(owner.Owner.Id, db);
                }
                var i = Activator.CreateInstance(t, [id, GetArgsByUnit(o, db), parent]) as IUnitType;
                Log.Debug("GetInstance will return {@i}", i);
                return i;
            }
            return null;
        }
        public int MakeInstance(IUnitType unit, SimulationDatabaseContext db, IUnitType? owner)
        {
            Log.Debug("Making instance in db of {@unit}, owned by {@owner}", unit, owner);
            SimulatedUnit x;
            if (owner == null)
            {
                Log.Debug("Owner is null");
                x = new SimulatedUnit { Type = unit.TypeNum, Owner = null };
            }
            else
            {
                Log.Debug("Owner is not null");
                x = new SimulatedUnit { Type = unit.TypeNum, Owner = db.SimulatedUnits.Find(owner.Id) };
            }
            db.SimulatedUnits.Add(x);
            Log.Debug("Added unit to db");
            db.SaveChanges();
            Log.Debug("Saved changes");
            Log.Debug("Id of new unit is {id}", x.Id);
            return x.Id;
        }
        public List<IUnitType> GetAllOwnedBy(IUnitType owner, SimulationDatabaseContext db)
        {
            Log.Debug("Owner is {@owner}", owner);
            var x = db.SimulatedUnits.Include(b => b.Owner).Where(b => b.Owner.Equals(db.SimulatedUnits.Find(owner.Id))).ToList();
            //var x = db.SimulatedUnits.Include(b => b.Owner).Where(b => b.Owner == db.SimulatedUnits.Find(owner.Id)).ToList();
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

        public Dictionary<int, string> GetArgsByUnit(SimulatedUnit unit, SimulationDatabaseContext db)
        {
            var x = db.UnitArguments.Where(b => b.Owner == db.SimulatedUnits.Find(unit.Id)).ToList();
            Log.Debug("GetArgsByUnit for unit {@unit} has result from db {@x}.", unit, x);
            var list = new Dictionary<int, string>();
            foreach (UnitArgument z in x)
            {
                Log.Debug("Working on making args in GetArgsByUnit. Doing arg {@arg}", z);
                list.Add(z.Id, z.Value);
            }
            Log.Debug("GetArgsByUnit will send {@list}", list);
            return list;
        }

        public void SaveUnit(IUnitType unit, SimulationDatabaseContext db)
        {
            SimulatedUnit? loaded_unit = db.SimulatedUnits.First(b => b.Id == unit.Id);
            Log.Debug("Loaded a unit from the db. Value is {@loaded}", loaded_unit);
            if (loaded_unit != null) {
                Log.Debug("Unit is not null.");
                loaded_unit.Id = unit.Id;
                loaded_unit.Type = unit.TypeNum;
                Log.Debug("New loaded_unit is {@loaded}", loaded_unit);
                db.SaveChanges();
                Log.Debug("Saving changes");
                db.UnitArguments.Where(b => b.Owner == db.SimulatedUnits.Find(unit.Id)).ExecuteDelete();
                Log.Debug("Deleted old arguments. Now adding new arguments.");
                foreach (var I in unit.Arguments)
                {
                    var x = new UnitArgument { Owner = db.SimulatedUnits.Find(unit.Id), Type = I.Key, Value = I.Value };
                    Log.Debug("Iterating over unit's arguments. {@I}, {@x}", I, x);
                    db.UnitArguments.Add(x);
                    db.SaveChanges();
                    Log.Debug("Saving changes.");
                }
                Log.Debug("Finished saving unit {@unit}.", unit);
            }
        }
    }
}
