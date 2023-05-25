namespace DatabaseApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button insertButton;
        private System.Windows.Forms.Button selectRandomButton;
        private System.Windows.Forms.Button selectRangeButton;
        private System.Windows.Forms.Button removeAllButton;
        private System.Windows.Forms.Button insertMillionButton; // New button
        private System.Windows.Forms.Label connectionStatusLabel;
        private System.Windows.Forms.ProgressBar progressBar; // Add progressBar control

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.insertButton = new System.Windows.Forms.Button();
            this.selectRandomButton = new System.Windows.Forms.Button();
            this.selectRangeButton = new System.Windows.Forms.Button();
            this.removeAllButton = new System.Windows.Forms.Button();
            this.insertMillionButton = new System.Windows.Forms.Button();
            this.connectionStatusLabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // insertButton
            // 
            this.insertButton.Location = new System.Drawing.Point(10, 10);
            this.insertButton.Name = "insertButton";
            this.insertButton.Size = new System.Drawing.Size(129, 26);
            this.insertButton.TabIndex = 0;
            this.insertButton.Text = "Insert Records";
            this.insertButton.UseVisualStyleBackColor = true;
            this.insertButton.Click += new System.EventHandler(this.insertButton_Click);
            // 
            // selectRandomButton
            // 
            this.selectRandomButton.Location = new System.Drawing.Point(10, 42);
            this.selectRandomButton.Name = "selectRandomButton";
            this.selectRandomButton.Size = new System.Drawing.Size(129, 26);
            this.selectRandomButton.TabIndex = 1;
            this.selectRandomButton.Text = "Select Random Records";
            this.selectRandomButton.UseVisualStyleBackColor = true;
            this.selectRandomButton.Click += new System.EventHandler(this.selectRandomButton_Click);
            // 
            // selectRangeButton
            // 
            this.selectRangeButton.Location = new System.Drawing.Point(10, 73);
            this.selectRangeButton.Name = "selectRangeButton";
            this.selectRangeButton.Size = new System.Drawing.Size(129, 26);
            this.selectRangeButton.TabIndex = 2;
            this.selectRangeButton.Text = "Select Records in Range";
            this.selectRangeButton.UseVisualStyleBackColor = true;
            this.selectRangeButton.Click += new System.EventHandler(this.selectRangeButton_Click);
            // 
            // removeAllButton
            // 
            this.removeAllButton.Location = new System.Drawing.Point(10, 104);
            this.removeAllButton.Name = "removeAllButton";
            this.removeAllButton.Size = new System.Drawing.Size(129, 26);
            this.removeAllButton.TabIndex = 3;
            this.removeAllButton.Text = "Remove All Data";
            this.removeAllButton.UseVisualStyleBackColor = true;
            this.removeAllButton.Click += new System.EventHandler(this.removeAllButton_Click);
            // 
            // insertMillionButton
            // 
            this.insertMillionButton.Location = new System.Drawing.Point(10, 135);
            this.insertMillionButton.Name = "insertMillionButton";
            this.insertMillionButton.Size = new System.Drawing.Size(129, 26);
            this.insertMillionButton.TabIndex = 4;
            this.insertMillionButton.Text = "Insert 1000000 values";
            this.insertMillionButton.UseVisualStyleBackColor = true;
            this.insertMillionButton.Click += new System.EventHandler(this.insertMillionButton_Click);
            // 
            // connectionStatusLabel
            // 
            this.connectionStatusLabel.AutoSize = true;
            this.connectionStatusLabel.Location = new System.Drawing.Point(10, 185);
            this.connectionStatusLabel.Name = "connectionStatusLabel";
            this.connectionStatusLabel.Size = new System.Drawing.Size(0, 14);
            this.connectionStatusLabel.TabIndex = 5;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(10, 166);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(129, 17);
            this.progressBar.TabIndex = 5;
            this.progressBar.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(148, 211);
            this.Controls.Add(this.connectionStatusLabel);
            this.Controls.Add(this.selectRangeButton);
            this.Controls.Add(this.selectRandomButton);
            this.Controls.Add(this.insertButton);
            this.Controls.Add(this.removeAllButton);
            this.Controls.Add(this.insertMillionButton);
            this.Controls.Add(this.progressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database App";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
