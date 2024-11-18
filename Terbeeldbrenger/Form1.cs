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
                backgroundWorker1.RunWorkerAsync();
            } else
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
                worker.ReportProgress((i+1) * 100 / steps);
            }

            Interaction.Beep();

            return;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            multiStepStatusLabel.Text = (e.ProgressPercentage.ToString() + "%");
            multiStepProgress.Value = e.ProgressPercentage;
        }
    }
}
