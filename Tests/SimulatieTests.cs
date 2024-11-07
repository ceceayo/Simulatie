using Simulatie;
using Simulatie.UnitTypes;

namespace Tests;

[TestClass]
public class SimulatieTests
{
    [TestMethod]
    public void TestTrueIsTrue()
    {
        Assert.IsTrue(true);
    }

    [TestMethod]
    public void TestConnectDatabase()
    {
        using var db = new SimulationDatabaseContext();
    }
    [TestMethod]
    public void TestListAllUnits()
    {
        using var db = new SimulationDatabaseContext();
        var units = db.SimulatedUnits.ToList();
        foreach (var unit in units)
        {
            Console.WriteLine(unit);
        }
    }

    [TestMethod]
    public void TestListAllUnitArguments()
    {
        using var db = new SimulationDatabaseContext();
        var units = db.UnitArguments.ToList();
        foreach (var unit in units)
        {
            Console.WriteLine(unit);
        }
    }

    [TestMethod]
    public void TestCreateUnitProvider()
    {
        var up = new UnitProvider();
        Assert.IsNotNull(up);
    }

    [TestMethod]
    public void TestUnitProviderTypesDict()
    {
        var up = new UnitProvider();
        Assert.IsTrue(up.Types.Count == 2);
        Assert.AreEqual(up.Types[1], typeof(City));
        Assert.AreEqual(up.Types[2], typeof(House));
    }
    /* TEST TEMPORARILY DISABLED
    [TestMethod]
    public void TestSaveUnitInDatabase()
    {
        using var db = new SimulationDatabaseContext();
        var up = new UnitProvider();
        int idOfGeneratedUnit = up.MakeInstance(
            new House(-1, new Dictionary<int, string>()), db, null);
        var unitFromDb = db.SimulatedUnits.Find(idOfGeneratedUnit);
        var unitFromUp = up.GetInstance(idOfGeneratedUnit, db);
        Assert.IsNotNull(unitFromDb);
        Assert.IsNotNull(unitFromUp);
        Assert.AreEqual(unitFromDb.Type, unitFromUp.TypeNum);
        Assert.AreEqual(unitFromDb.Id, unitFromUp.Id);
        
    }*/
}