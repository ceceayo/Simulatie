namespace Terbeeldbrenger
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            button1 = new Button();
            resultLabel = new Label();
            startIdSelector = new NumericUpDown();
            button2 = new Button();
            multipleRunsAmountSelector = new NumericUpDown();
            runMultipleStepsButton = new Button();
            multiStepStatusLabel = new Label();
            Progress = new ProgressBar();
            runMultipleStepsBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            cancelButton = new Button();
            exitButton = new Button();
            label2 = new Label();
            label3 = new Label();
            button3 = new Button();
            viewSimulationButton = new Button();
            simulationTree = new TreeView();
            createSimulationBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            updateDatabaseButton = new Button();
            ((System.ComponentModel.ISupportInitialize)startIdSelector).BeginInit();
            ((System.ComponentModel.ISupportInitialize)multipleRunsAmountSelector).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(314, 15);
            label1.TabIndex = 0;
            label1.Text = "Den groten magischen fantastischen TERBEELDBRENGER™";
            // 
            // button1
            // 
            button1.Location = new Point(12, 27);
            button1.Name = "button1";
            button1.Size = new Size(406, 88);
            button1.TabIndex = 1;
            button1.Text = "Maak Simulatie";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // resultLabel
            // 
            resultLabel.AutoSize = true;
            resultLabel.Location = new Point(12, 118);
            resultLabel.Name = "resultLabel";
            resultLabel.Size = new Size(225, 15);
            resultLabel.TabIndex = 2;
            resultLabel.Text = "Hier komt het prachtige resultaat te staan";
            // 
            // startIdSelector
            // 
            startIdSelector.Location = new Point(96, 164);
            startIdSelector.Name = "startIdSelector";
            startIdSelector.Size = new Size(322, 23);
            startIdSelector.TabIndex = 3;
            // 
            // button2
            // 
            button2.Location = new Point(12, 193);
            button2.Name = "button2";
            button2.Size = new Size(406, 93);
            button2.TabIndex = 4;
            button2.Text = "Draai Simulatie";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // multipleRunsAmountSelector
            // 
            multipleRunsAmountSelector.Location = new Point(96, 292);
            multipleRunsAmountSelector.Maximum = new decimal(new int[] { 0, 0, 0, 0 });
            multipleRunsAmountSelector.Name = "multipleRunsAmountSelector";
            multipleRunsAmountSelector.Size = new Size(322, 23);
            multipleRunsAmountSelector.TabIndex = 5;
            // 
            // runMultipleStepsButton
            // 
            runMultipleStepsButton.Location = new Point(12, 321);
            runMultipleStepsButton.Name = "runMultipleStepsButton";
            runMultipleStepsButton.Size = new Size(406, 93);
            runMultipleStepsButton.TabIndex = 6;
            runMultipleStepsButton.Text = "Meerdere stappen uitvoeren";
            runMultipleStepsButton.UseVisualStyleBackColor = true;
            runMultipleStepsButton.Click += runMultipleStepsButton_Click;
            // 
            // multiStepStatusLabel
            // 
            multiStepStatusLabel.AutoSize = true;
            multiStepStatusLabel.Location = new Point(12, 479);
            multiStepStatusLabel.Name = "multiStepStatusLabel";
            multiStepStatusLabel.Size = new Size(0, 15);
            multiStepStatusLabel.TabIndex = 7;
            // 
            // Progress
            // 
            Progress.Location = new Point(12, 497);
            Progress.Name = "Progress";
            Progress.Size = new Size(406, 23);
            Progress.TabIndex = 8;
            // 
            // runMultipleStepsBackgroundWorker
            // 
            runMultipleStepsBackgroundWorker.WorkerReportsProgress = true;
            runMultipleStepsBackgroundWorker.WorkerSupportsCancellation = true;
            runMultipleStepsBackgroundWorker.DoWork += backgroundWorker1_DoWork;
            runMultipleStepsBackgroundWorker.ProgressChanged += backgroundWorker1_ProgressChanged;
            runMultipleStepsBackgroundWorker.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            // 
            // cancelButton
            // 
            cancelButton.Enabled = false;
            cancelButton.Location = new Point(220, 526);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(198, 108);
            cancelButton.TabIndex = 9;
            cancelButton.Text = "ABBRECHEN";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // exitButton
            // 
            exitButton.Location = new Point(12, 526);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(202, 108);
            exitButton.TabIndex = 10;
            exitButton.Text = "Applicatie sluiten";
            exitButton.UseVisualStyleBackColor = true;
            exitButton.Click += exitButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 166);
            label2.Name = "label2";
            label2.Size = new Size(78, 15);
            label2.TabIndex = 11;
            label2.Text = "Simulation ID";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 294);
            label3.Name = "label3";
            label3.Size = new Size(70, 15);
            label3.TabIndex = 12;
            label3.Text = "Steps to run";
            // 
            // button3
            // 
            button3.Location = new Point(12, 640);
            button3.Name = "button3";
            button3.Size = new Size(202, 108);
            button3.TabIndex = 13;
            button3.Text = "BEARBEITUNGSHANDBUCHSOEFNUNGSFARBE";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // viewSimulationButton
            // 
            viewSimulationButton.Location = new Point(12, 420);
            viewSimulationButton.Name = "viewSimulationButton";
            viewSimulationButton.Size = new Size(406, 56);
            viewSimulationButton.TabIndex = 15;
            viewSimulationButton.Text = "Bekijk simulatie";
            viewSimulationButton.UseVisualStyleBackColor = true;
            viewSimulationButton.Click += viewSimulationButton_Click;
            // 
            // simulationTree
            // 
            simulationTree.Location = new Point(450, 12);
            simulationTree.Name = "simulationTree";
            simulationTree.Size = new Size(500, 622);
            simulationTree.TabIndex = 14;
            // 
            // createSimulationBackgroundWorker
            // 
            createSimulationBackgroundWorker.DoWork += createSimulationBackgroundWorker_DoWork;
            createSimulationBackgroundWorker.RunWorkerCompleted += createSimulationBackgroundWorker_RunWorkerCompleted;
            // 
            // updateDatabaseButton
            // 
            updateDatabaseButton.Location = new Point(220, 640);
            updateDatabaseButton.Name = "updateDatabaseButton";
            updateDatabaseButton.Size = new Size(198, 108);
            updateDatabaseButton.TabIndex = 16;
            updateDatabaseButton.Text = "Update database";
            updateDatabaseButton.UseVisualStyleBackColor = true;
            updateDatabaseButton.Click += updateDatabaseButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(430, 758);
            Controls.Add(updateDatabaseButton);
            Controls.Add(viewSimulationButton);
            Controls.Add(simulationTree);
            Controls.Add(button3);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(exitButton);
            Controls.Add(cancelButton);
            Controls.Add(Progress);
            Controls.Add(multiStepStatusLabel);
            Controls.Add(runMultipleStepsButton);
            Controls.Add(multipleRunsAmountSelector);
            Controls.Add(button2);
            Controls.Add(startIdSelector);
            Controls.Add(resultLabel);
            Controls.Add(button1);
            Controls.Add(label1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            HelpButton = true;
            KeyPreview = true;
            MaximizeBox = false;
            Name = "Form1";
            Text = "De TERBEELDBRENGER";
            HelpButtonClicked += Form1_HelpButtonClicked;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)startIdSelector).EndInit();
            ((System.ComponentModel.ISupportInitialize)multipleRunsAmountSelector).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button button1;
        private Label resultLabel;
        private NumericUpDown startIdSelector;
        private Button button2;
        private NumericUpDown multipleRunsAmountSelector;
        private Button runMultipleStepsButton;
        private Label multiStepStatusLabel;
        private ProgressBar Progress;
        private System.ComponentModel.BackgroundWorker runMultipleStepsBackgroundWorker;
        private Button cancelButton;
        private Button exitButton;
        private Label label2;
        private Label label3;
        private Button button3;
        private Button viewSimulationButton;
        private TreeView simulationTree;
        private System.ComponentModel.BackgroundWorker createSimulationBackgroundWorker;
        private Button updateDatabaseButton;
    }
}
