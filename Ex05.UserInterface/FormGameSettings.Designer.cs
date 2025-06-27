using System.Windows.Forms;

namespace Ex05.UserInterface
{
    partial class FormGameSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.numberOfChancesButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // numberOfChancesButton
            // 
            this.numberOfChancesButton.Location = new System.Drawing.Point(12, 12);
            this.numberOfChancesButton.Name = "numberOfChancesButton";
            this.numberOfChancesButton.Size = new System.Drawing.Size(258, 29);
            this.numberOfChancesButton.TabIndex = 0;
            this.numberOfChancesButton.UseVisualStyleBackColor = true;
            this.numberOfChancesButton.Click += new System.EventHandler(this.numberOfChancesButton_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(162, 68);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(108, 23);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // FormGameSettings
            // 
            this.ClientSize = new System.Drawing.Size(282, 103);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.numberOfChancesButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGameSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bool Pgia";
            this.ResumeLayout(false);

        }

        #endregion

        private Button numberOfChancesButton;
        private Button startButton;
    }
}