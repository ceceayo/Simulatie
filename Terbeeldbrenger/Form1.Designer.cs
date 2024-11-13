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
            numericUpDown1 = new NumericUpDown();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
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
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(12, 164);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(399, 23);
            numericUpDown1.TabIndex = 3;
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(423, 397);
            Controls.Add(button2);
            Controls.Add(numericUpDown1);
            Controls.Add(resultLabel);
            Controls.Add(button1);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button button1;
        private Label resultLabel;
        private NumericUpDown numericUpDown1;
        private Button button2;
    }
}
