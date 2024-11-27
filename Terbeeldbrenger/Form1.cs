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

            // Start the asynchronous operation.
            runMultipleStepsButton.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            startIdSelector.Enabled = false;
            multipleRunsAmountSelector.Enabled = false;
            cancelButton.Enabled = false;
            exitButton.Enabled = false;
            resultLabel.Text = "Simulatie maken...";
            Progress.Style = ProgressBarStyle.Marquee;
            updateDatabaseButton.Enabled = false;
            createSimulationBackgroundWorker.RunWorkerAsync();
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
            if (runMultipleStepsBackgroundWorker.IsBusy != true)
            {
                // Start the asynchronous operation.
                runMultipleStepsButton.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
                startIdSelector.Enabled = false;
                multipleRunsAmountSelector.Enabled = false;
                cancelButton.Enabled = true;
                exitButton.Enabled = false;
                updateDatabaseButton.Enabled = false;
                runMultipleStepsBackgroundWorker.RunWorkerAsync();
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
            Simulator sim = new Simulator();
            sim.sp.GuiMode = true;
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
            Progress.Value = e.ProgressPercentage;
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
            updateDatabaseButton.Enabled = true;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            cancelButton.Enabled = false;
            runMultipleStepsBackgroundWorker.CancelAsync();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Interaction.Beep();
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Interaction.Beep();
            var psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://ceayo.neocities.org/bearbeitungshandbuchoefnungsfarberesultat.html",
                UseShellExecute = true
            };
            System.Diagnostics.Process.Start(psi);
        }


        internal List<TreeNode> viewSimulationButton_Click_Recursive(int simulationId, IUnitType unit)
        {
            List<TreeNode> nodes = new List<TreeNode>();
            foreach (IUnitType child in sim.up.GetAllOwnedBy(unit, sim.db))
            {
                TreeNode node = new TreeNode($"{sim.up.GetType(child.TypeNum).Name} ({child.Id}) [{sim.db.SimulatedUnits.Find(child.Id)!.ResourcesUsedLastRound} / {sim.db.SimulatedUnits.Find(child.Id)!.ResourcesUsedLastRoundRecursive}]");

                foreach (TreeNode node2 in viewSimulationButton_Click_Recursive(simulationId, child))
                {
                    node.Nodes.Add(node2);
                }
                nodes.Add(node);
            }
            return nodes;
        }

        public void viewSimulationButton_Click(object sender, EventArgs e)
        {
            viewSimulationButton_Click_x();
        }

        private void viewSimulationButton_Click_x()
        {
            this.Size = new System.Drawing.Size(1000, 700);
            simulationTree.Nodes.Clear();
            int simulationId = int.Parse(startIdSelector.Value.ToString());
            var simulation = sim.db.Simulations.Include(b => b.Unit).Single(b => b.Id == simulationId);
            simulationTree.BeginUpdate();
            TreeNode root = new TreeNode($"City (id {simulation.Unit.Id}) [{simulation.Unit.ResourcesUsedLastRound} / {simulation.Unit.ResourcesUsedLastRoundRecursive}]");
            simulationTree.Nodes.Add(root);
            foreach (TreeNode node2 in viewSimulationButton_Click_Recursive(simulation.Id, sim.up.GetInstance(simulation.Unit.Id, sim.db)!))
            {
                root.Nodes.Add(node2);
            }
            simulationTree.EndUpdate();

        }

        private void createSimulationBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Simulator sim = new Simulator();
            sim.sp.GuiMode = true;
            int id = sim.CreateSimulation();
            e.Result = id;
        }

        private void createSimulationBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            runMultipleStepsButton.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = true;
            startIdSelector.Enabled = true;
            multipleRunsAmountSelector.Enabled = true;
            cancelButton.Enabled = false;
            exitButton.Enabled = true;
            updateDatabaseButton.Enabled = true;
            Progress.Style = ProgressBarStyle.Blocks;
            resultLabel.Text = $"Simulatie gemaakt! Id = {e.Result}.";
        }

        private void Form1_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            button3.PerformClick();
        }

        private void updateDatabaseButton_Click(object sender, EventArgs e)
        {
            sim.db.Database.Migrate();
            Interaction.MsgBox("The database was migrated to the latest version.");
        }
    }
}
