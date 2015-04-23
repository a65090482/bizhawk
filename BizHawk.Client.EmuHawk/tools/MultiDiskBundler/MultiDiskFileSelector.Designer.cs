﻿namespace BizHawk.Client.EmuHawk
{
	partial class MultiDiskFileSelector
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.PathBox = new System.Windows.Forms.TextBox();
			this.UseCurrentRomButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(290, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(60, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "Browse...";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// PathBox
			// 
			this.PathBox.AllowDrop = true;
			this.PathBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PathBox.Location = new System.Drawing.Point(3, 5);
			this.PathBox.Name = "PathBox";
			this.PathBox.Size = new System.Drawing.Size(285, 20);
			this.PathBox.TabIndex = 1;
			this.PathBox.TextChanged += new System.EventHandler(this.PathBox_TextChanged);
			this.PathBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox1_DragDrop);
			this.PathBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox1_DragEnter);
			// 
			// UseCurrentRomButton
			// 
			this.UseCurrentRomButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.UseCurrentRomButton.Location = new System.Drawing.Point(357, 3);
			this.UseCurrentRomButton.Name = "UseCurrentRomButton";
			this.UseCurrentRomButton.Size = new System.Drawing.Size(62, 23);
			this.UseCurrentRomButton.TabIndex = 3;
			this.UseCurrentRomButton.Text = "Current";
			this.UseCurrentRomButton.UseVisualStyleBackColor = true;
			this.UseCurrentRomButton.Click += new System.EventHandler(this.UseCurrentRomButton_Click);
			// 
			// MultiDiskFileSelector
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Controls.Add(this.UseCurrentRomButton);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.PathBox);
			this.Name = "MultiDiskFileSelector";
			this.Size = new System.Drawing.Size(425, 29);
			this.Load += new System.EventHandler(this.DualGBFileSelector_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox PathBox;
		private System.Windows.Forms.Button UseCurrentRomButton;

	}
}
