using Microsoft.VisualBasic;
using Simulatie;
namespace Terbeeldbrenger
{
    public partial class Form1 : Form
    {
        Simulatie.Simulator sim = new Simulatie.Simulator();
        public Form1()
        {
            InitializeComponent();
            sim.sp.GuiMode = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Parallel.Invoke(() =>
            {
                resultLabel.Text = "Simulatie maken...";
                int id = sim.CreateSimulation();
                resultLabel.Text = $"Simulatie gemaakt! Id is {id}.";
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
                var startId = int.Parse(Interaction.InputBox("StartId"));
                var simul = sim.db.Simulations.Include(b => b.Unit).Single(b => b.Id == startId);
                var instance = sim.up.GetInstance(simul.Unit.Id, sim.db);
                if (instance != null)
                {
                    var res = sim.RunSimulationAt(instance, simul);
                }
                else
                {
                }
        }
    }
}
