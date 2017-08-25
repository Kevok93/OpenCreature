using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq.Expressions;
using System.Linq;
using opencreature;

namespace Editor {
	public partial class MainForm : Form {
		void ElementColorButtonClick(object sender, EventArgs e) {
			ElementColorDialog.ShowDialog();
			ElementColorButton.BackColor = ElementColorDialog.Color;
		}
		void ElementUpdate() {
			var index1 = currentRowId;
			var elem1 = Element.TYPES[index1];
			SplitterPanel parent_control = (SplitterPanel)ElementRow_Table.Parent;
			ElementRow_Table = new TableLayoutPanel();
			// 
			// ElementRow_Table
			// 
			this.ElementRow_Table.AutoSize = true;
			this.ElementRow_Table.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ElementRow_Table.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.ElementRow_Table.ColumnCount = 3;
			this.ElementRow_Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
			this.ElementRow_Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.5F));
			this.ElementRow_Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.5F));
			this.ElementRow_Table.Dock = System.Windows.Forms.DockStyle.Top;
			this.ElementRow_Table.Name = "ElementRow_Table";
			this.ElementRow_Table.TabIndex = 1;
			
			this.comboBox1.Items.AddRange(new object[] {
			"1x",
			"2x",
			"1/2x",
			"0x"});
			this.comboBox1.Text = "1x";
			foreach (Element elem2 in Element.TYPES.Values) {
				var index2 = elem2.id;
				var row_id = ElementRow_Table.RowCount++;
				ElementRow_Table.RowStyles.Add(new RowStyle(SizeType.Absolute,30));
				
				var name_box = new Label() {
						Text = elem2.name,
						Dock = DockStyle.Fill,
						Font = new Font(
							"Microsoft Sans Serif",
							20
						)
				};
				var defence_box= new ComboBox() {
							FormattingEnabled = true,
							DropDownStyle=ComboBoxStyle.DropDownList,
							Dock = DockStyle.Fill,
							Items = {
								"1x", "2x", "1/2x", "0x"
							},
							
				};
				var attack_box = new ComboBox() {
							FormattingEnabled = true,
							DropDownStyle=ComboBoxStyle.DropDownList,
							Dock = DockStyle.Fill,
							Items = {
								"1x", "2x", "1/2x", "0x"
							},
							
				};
				 attack_box.SelectedIndex =  attack_box.Items.IndexOf(Element.getTypeBonus(index1,index2));
				defence_box.SelectedIndex = defence_box.Items.IndexOf(Element.getTypeBonus(index2,index1));
				ElementRow_Table.Controls.Add(name_box   , 0, row_id);
				ElementRow_Table.Controls.Add(attack_box , 1, row_id);
				ElementRow_Table.Controls.Add(defence_box, 1, row_id);
				
			}
			Console.Write("Hello world");
			parent_control.Controls.Clear();
			parent_control.Controls.Add(ElementRow_Table);
		}
		public int ElemStringToBonus (string bonus) { 
			switch (bonus) {
				case "2x"  : return 200;
				case "1/2x": return 050;
				case "0x"  : return 000;
				default    : return 100;
			}
		}
		public string ElemBonusToString (int bonus) { 
			switch (bonus) {
				case 200 : return "2x"  ;
				case 050 : return "1/2x";
				case 000 : return "0x"  ;
				default  : return "1x"  ;
			}
		}
	}
}
