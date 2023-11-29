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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1239, 846);
            this.Controls.Add(this.PickFileButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button PickFileButton;
        private System.Windows.Forms.OpenFileDialog openTestCaseDialog;
    }
}