using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
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

            int startId = int.Parse(startIdSelector.Value.ToString());
            var simul = sim.db.Simulations.Include(b => b.Unit).Single(b => b.Id == startId);
            var instance = sim.up.GetInstance(simul.Unit.Id, sim.db);
            if (instance != null)
            {
                var res = sim.RunSimulationAt(instance, simul);
                resultLabel.Text = $"Simulatie gerund! Resultaat is {res}.";
            }
            else
            {
                resultLabel.Text = "Geen instance voor start_id.";
            }
        }

        private void runMultipleStepsButton_Click(object sender, EventArgs e)
        {
            int startId = int.Parse(startIdSelector.Value.ToString());
            int steps = int.Parse(multipleRunsAmountSelector.Value.ToString());
            Task<int> task = RunMultipleStepsTask(startId, steps, sim, resultLabel, multiStepStatusLabel, this, multiStepProgress);
        }
        public static async Task<int> RunMultipleStepsTask(int startId, int steps, Simulator sim, Label resultLabel, Label multiStepStatusLabel, Form form, ProgressBar multiStepProgress)
        {

            var simul = sim.db.Simulations.Include(b => b.Unit).Single(b => b.Id == startId);
            var instance = sim.up.GetInstance(simul.Unit.Id, sim.db);
            multiStepStatusLabel.Text = "Simulatie gestart...";
            for (int i = 0; i < steps; i++)
            {
                if (instance != null)
                {
                    var res = sim.RunSimulationAt(instance, simul);
                    resultLabel.Text = $"Simulatie gerund! Resultaat is {res}.";
                }
                else
                {
                    resultLabel.Text = "Geen instance voor start_id.";
                }
                multiStepStatusLabel.Text = $"Stap {i + 1} van {steps} voltooid.";
                multiStepProgress.Value = i + 1;
                multiStepProgress.Maximum = steps;
                form.Refresh();
                form.Invalidate();
            }
            return 0;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            multipleRunsAmountSelector.Maximum = decimal.MaxValue;
            startIdSelector.Maximum = decimal.MaxValue;
        }
    }
}
