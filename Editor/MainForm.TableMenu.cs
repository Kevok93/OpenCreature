
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq.Expressions;
using System.Linq;
using opencreature;

namespace Editor {
	public partial class MainForm : Form {
		public string currentTable = "Elements";
		
		public void updateRowList () {
			RowList.Items.Clear();
			DeserializedElement[] TableRows = getTableRowNames(currentTable);
			if (TableRows == null) {throw new KeyNotFoundException(currentTable);}
			foreach (DeserializedElement elem in TableRows) {
				RowList.Items.Add(String.Format(rowname_format, elem.id, elem));
			}
		}
		
		void TableListAfterSelect(object sender, TreeViewEventArgs e) {
			Console.Error.WriteLine("asdf");
			if (getTableRowNames(e.Node.Text) == null) return;
			currentTable = e.Node.Text;
			updateRowList();
			DisplayOutput.SelectedTab = (
				from System.Windows.Forms.TabPage page 
				in DisplayOutput.TabPages
				where page.Text == currentTable 
				select page
			).First();
		}
	}
}

