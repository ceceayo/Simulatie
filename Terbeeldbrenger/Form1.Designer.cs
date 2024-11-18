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
            multiStepProgress = new ProgressBar();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
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
            button1.Size = new Size(399, 88);
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
            startIdSelector.Location = new Point(12, 164);
            startIdSelector.Name = "startIdSelector";
            startIdSelector.Size = new Size(399, 23);
            startIdSelector.TabIndex = 3;
            // 
            // button2
            // 
            button2.Location = new Point(12, 193);
            button2.Name = "button2";
            button2.Size = new Size(399, 93);
            button2.TabIndex = 4;
            button2.Text = "Draai Simulatie";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // multipleRunsAmountSelector
            // 
            multipleRunsAmountSelector.Location = new Point(12, 292);
            multipleRunsAmountSelector.Maximum = new decimal(new int[] { 0, 0, 0, 0 });
            multipleRunsAmountSelector.Name = "multipleRunsAmountSelector";
            multipleRunsAmountSelector.Size = new Size(399, 23);
            multipleRunsAmountSelector.TabIndex = 5;
            // 
            // runMultipleStepsButton
            // 
            runMultipleStepsButton.Location = new Point(12, 321);
            runMultipleStepsButton.Name = "runMultipleStepsButton";
            runMultipleStepsButton.Size = new Size(399, 93);
            runMultipleStepsButton.TabIndex = 6;
            runMultipleStepsButton.Text = "Meerdere stappen uitvoeren";
            runMultipleStepsButton.UseVisualStyleBackColor = true;
            runMultipleStepsButton.Click += runMultipleStepsButton_Click;
            // 
            // multiStepStatusLabel
            // 
            multiStepStatusLabel.AutoSize = true;
            multiStepStatusLabel.Location = new Point(12, 417);
            multiStepStatusLabel.Name = "multiStepStatusLabel";
            multiStepStatusLabel.Size = new Size(38, 15);
            multiStepStatusLabel.TabIndex = 7;
            multiStepStatusLabel.Text = "label2";
            // 
            // multiStepProgress
            // 
            multiStepProgress.Location = new Point(12, 435);
            multiStepProgress.Name = "multiStepProgress";
            multiStepProgress.Size = new Size(399, 23);
            multiStepProgress.TabIndex = 8;
            // 
            // backgroundWorker1
            // 
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(423, 477);
            Controls.Add(multiStepProgress);
            Controls.Add(multiStepStatusLabel);
            Controls.Add(runMultipleStepsButton);
            Controls.Add(multipleRunsAmountSelector);
            Controls.Add(button2);
            Controls.Add(startIdSelector);
            Controls.Add(resultLabel);
            Controls.Add(button1);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
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
        private ProgressBar multiStepProgress;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}
