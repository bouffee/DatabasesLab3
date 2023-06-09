﻿namespace DatabaseApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label connectionStatusLabel;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button compareQueriesButton;
        private System.Windows.Forms.Button insertMillionButton;
        private System.Windows.Forms.Button removeAllButton;
        private System.Windows.Forms.Button GINindex;

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
            this.connectionStatusLabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.compareQueriesButton = new System.Windows.Forms.Button();
            this.insertMillionButton = new System.Windows.Forms.Button();
            this.removeAllButton = new System.Windows.Forms.Button();
            this.GINindex = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // connectionStatusLabel
            // 
            this.connectionStatusLabel.AutoSize = true;
            this.connectionStatusLabel.Location = new System.Drawing.Point(10, 185);
            this.connectionStatusLabel.Name = "connectionStatusLabel";
            this.connectionStatusLabel.Size = new System.Drawing.Size(0, 13);
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
            // compareQueriesButton
            // 
            this.compareQueriesButton.Location = new System.Drawing.Point(10, 42);
            this.compareQueriesButton.Name = "compareQueriesButton";
            this.compareQueriesButton.Size = new System.Drawing.Size(129, 26);
            this.compareQueriesButton.TabIndex = 1;
            this.compareQueriesButton.Text = "Сравнить hash и b-tree";
            this.compareQueriesButton.UseVisualStyleBackColor = true;
            this.compareQueriesButton.Click += new System.EventHandler(this.compareQueriesButton_Click);
            // 
            // insertMillionButton
            // 
            this.insertMillionButton.Location = new System.Drawing.Point(10, 10);
            this.insertMillionButton.Name = "insertMillionButton";
            this.insertMillionButton.Size = new System.Drawing.Size(129, 26);
            this.insertMillionButton.TabIndex = 2;
            this.insertMillionButton.Text = "Вставить 350000 записей";
            this.insertMillionButton.UseVisualStyleBackColor = true;
            this.insertMillionButton.Click += new System.EventHandler(this.insertMillionButton_Click);
            // 
            // removeAllButton
            // 
            this.removeAllButton.Location = new System.Drawing.Point(10, 74);
            this.removeAllButton.Name = "removeAllButton";
            this.removeAllButton.Size = new System.Drawing.Size(129, 26);
            this.removeAllButton.TabIndex = 3;
            this.removeAllButton.Text = "Удалить все данные";
            this.removeAllButton.UseVisualStyleBackColor = true;
            this.removeAllButton.Click += new System.EventHandler(this.removeAllButton_Click);
            // 
            // GINindex
            // 
            this.GINindex.Location = new System.Drawing.Point(10, 106);
            this.GINindex.Name = "GINindex";
            this.GINindex.Size = new System.Drawing.Size(126, 23);
            this.GINindex.TabIndex = 6;
            this.GINindex.Text = "GIN индекс";
            this.GINindex.UseVisualStyleBackColor = true;
            this.GINindex.Click += new System.EventHandler(this.GINindex_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(148, 211);
            this.Controls.Add(this.GINindex);
            this.Controls.Add(this.removeAllButton);
            this.Controls.Add(this.insertMillionButton);
            this.Controls.Add(this.compareQueriesButton);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.connectionStatusLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Лабораторная 3";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}