﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using BizHawk.Client.Common;
using BizHawk.Emulation.Common;
using BizHawk.Emulation.Cores.Nintendo.Gameboy;

namespace BizHawk.Client.EmuHawk
{
	public partial class MultiDiskFileSelector : UserControl
	{
		public string GetName()
		{
			return textBox1.Text;
		}

		public void SetName(string val)
		{
			textBox1.Text = val;
		}

		public event EventHandler NameChanged;

		private void HandleLabelTextChanged(object sender, EventArgs e)
		{
			this.OnNameChanged(EventArgs.Empty);
		}

		public MultiDiskFileSelector()
		{
			InitializeComponent();
			textBox1.TextChanged += this.HandleLabelTextChanged;
		}

		protected virtual void OnNameChanged(EventArgs e)
		{
			EventHandler handler = this.NameChanged;
			if (handler != null)
			{
				handler(this, e);
			}
		}

		private void textBox1_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop) &&
				((string[])e.Data.GetData(DataFormats.FileDrop)).Length == 1)
			{
				e.Effect = DragDropEffects.Copy;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		private void textBox1_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				var ff = (string[])e.Data.GetData(DataFormats.FileDrop);
				if (ff.Length == 1)
				{
					textBox1.Text = ff[0];
				}
			}				
		}

		private void button1_Click(object sender, EventArgs e)
		{
			using (var ofd = new OpenFileDialog
			{
				InitialDirectory = PathManager.MakeAbsolutePath(Global.Config.PathEntries["Global_NULL", "ROM"].Path, "Global_NULL"),
				Filter = MainForm.RomFilter,
				RestoreDirectory = true
			})
			{
				var result = ofd.ShowDialog(this);
				if (result == DialogResult.OK)
				{
					textBox1.Text = ofd.FileName;
				}
			}
		}

		private void UseCurrentRomButton_Click(object sender, EventArgs e)
		{
			textBox1.Text = GlobalWin.MainForm.CurrentlyOpenRom;
		}

		private void DualGBFileSelector_Load(object sender, EventArgs e)
		{
			UpdateValues();
		}

		public void UpdateValues()
		{
			UseCurrentRomButton.Enabled = Global.Emulator != null // For the designer
				&& !string.IsNullOrEmpty(GlobalWin.MainForm.CurrentlyOpenRom)
				&& !GlobalWin.MainForm.CurrentlyOpenRom.Contains('|') && // Can't be archive
				!GlobalWin.MainForm.CurrentlyOpenRom.Contains(".xml"); // Can't already be an xml
		}
	}
}
