using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Simulatie;
namespace Terbeeldbrenger
{
    public partial class Form1 : Form
    {
        Simulatie.Simulator sim = new Simulatie.Simulator();
        int maxSteps = 0;
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
            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.
                runMultipleStepsButton.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
                startIdSelector.Enabled = false;
                multipleRunsAmountSelector.Enabled = false;
                cancelButton.Enabled = true;
                exitButton.Enabled = false;
                backgroundWorker1.RunWorkerAsync();
            }
            else
            {
                maxSteps = int.Parse(multipleRunsAmountSelector.Value.ToString());
                Interaction.Beep();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            multipleRunsAmountSelector.Maximum = decimal.MaxValue;
            startIdSelector.Maximum = decimal.MaxValue;
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            int startId = int.Parse(startIdSelector.Value.ToString());
            int steps = int.Parse(multipleRunsAmountSelector.Value.ToString());
            var simul = sim.db.Simulations.Include(b => b.Unit).Single(b => b.Id == startId);
            var instance = sim.up.GetInstance(simul.Unit.Id, sim.db);
            //multiStepStatusLabel.Text = "Simulatie gestart...";
            for (int i = 0; i < steps; i++)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                if (instance != null)
                {
                    var res = sim.RunSimulationAt(instance, simul);
                    //resultLabel.Text = $"Simulatie gerund! Resultaat is {res}.";
                }
                else
                {
                    //resultLabel.Text = "Geen instance voor start_id.";
                }
                worker.ReportProgress((i + 1) * 100 / steps);
            }

            Interaction.Beep();

            return;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            multiStepStatusLabel.Text = (e.ProgressPercentage.ToString() + "%");
            multiStepProgress.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            runMultipleStepsButton.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = true;
            startIdSelector.Enabled = true;
            multipleRunsAmountSelector.Enabled = true;
            cancelButton.Enabled = false;
            exitButton.Enabled = true;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            cancelButton.Enabled = false;
            backgroundWorker1.CancelAsync();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Interaction.Beep();
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Interaction.Beep();
        }

        /*internal void viewSimulationButton_Click_Recursive(int simulationId, IUnitType unit, TreeNodeCollection)
        {
            foreach (IUnitType child in sim.up.GetAllOwnedBy(unit, sim.db))
            {

            }
        }*/

        private void viewSimulationButton_Click(object sender, EventArgs e)
        {
            simulationTree.Nodes.Clear();
            int simulationId = int.Parse(startIdSelector.Value.ToString());
            var simulation = sim.db.Simulations.Include(b => b.Unit).Single(b => b.Id == simulationId);
            simulationTree.BeginUpdate();
            simulationTree.Nodes.Add(simulation.Unit.ToString());


        }
    }
}
