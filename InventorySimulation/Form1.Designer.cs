namespace InventorySimulation
{
    partial class Form1
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
            this.PickFileButton = new System.Windows.Forms.Button();
            this.openTestCaseDialog = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PickFileButton
            // 
            this.PickFileButton.Location = new System.Drawing.Point(282, 15);
            this.PickFileButton.Name = "PickFileButton";
            this.PickFileButton.Size = new System.Drawing.Size(323, 53);
            this.PickFileButton.TabIndex = 0;
            this.PickFileButton.Text = "Pick Test File";
            this.PickFileButton.UseVisualStyleBackColor = true;
            this.PickFileButton.Click += new System.EventHandler(this.PickFileButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(624, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(396, 50);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1239, 846);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.PickFileButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button PickFileButton;
        private System.Windows.Forms.OpenFileDialog openTestCaseDialog;
        private System.Windows.Forms.Button button1;
    }
}