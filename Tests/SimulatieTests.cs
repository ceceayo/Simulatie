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

    [TestMethod]
    public void TestSimulatorCreateSimulation()
    {
        var simulator = new Simulator();
        int simulationId = simulator.CreateSimulation();
        using var db = new SimulationDatabaseContext();
        var simulation = db.Simulations.Find(simulationId);
        Assert.IsNotNull(simulation);
    }

    [TestMethod]
    public void TestSimulatorRunSimulationAt()
    {
        var simulator = new Simulator();
        int simulationId = simulator.CreateSimulation();
        using var db = new SimulationDatabaseContext();
        var simulation = db.Simulations.Find(simulationId);
        var instance = simulator.up.GetInstance(simulation.Unit.Id, db);
        int resourcesUsed = simulator.RunSimulationAt(instance, simulation);
        Assert.IsTrue(resourcesUsed >= 0);
    }

    [TestMethod]
    public void TestSimulatorRunSimulationRecursive()
    {
        var simulator = new Simulator();
        int simulationId = simulator.CreateSimulation();
        using var db = new SimulationDatabaseContext();
        var simulation = db.Simulations.Find(simulationId);
        var instance = simulator.up.GetInstance(simulation.Unit.Id, db);
        //var result = simulator.RunSimulationRecursive(instance, simulation);
        //Assert.IsTrue(result.ResourcesUsed >= 0);
    }

    [TestMethod]
    public void TestUnitProviderGetInstance()
    {
        var up = new UnitProvider();
        using var db = new SimulationDatabaseContext();
        //int idOfGeneratedUnit = up.MakeInstance(new House(-1, new Dictionary<int, string>()), db, null);
        //var unitFromDb = db.SimulatedUnits.Find(idOfGeneratedUnit);
        //var unitFromUp = up.GetInstance(idOfGeneratedUnit, db);
        //Assert.IsNotNull(unitFromDb);
       // Assert.IsNotNull(unitFromUp);
        //Assert.AreEqual(unitFromDb.Type, unitFromUp.TypeNum);
        //Assert.AreEqual(unitFromDb.Id, unitFromUp.Id);
    }

    [TestMethod]
    public void TestStatProviderFindInstance()
    {
        var sp = new StatProvider();
        using var db = new SimulationDatabaseContext();
        var simulator = new Simulator();
        int simulationId = simulator.CreateSimulation();
        var simulation = db.Simulations.Find(simulationId);
        var statInstance = sp.FindInstance(db, 1, 1, simulation, "Test message");
        Assert.IsNotNull(statInstance);
    }

    [TestMethod]
    public void TestSimulatorObjectCreation()
    {
        var simulator = new Simulator();
        Assert.IsNotNull(simulator);
    }
}
