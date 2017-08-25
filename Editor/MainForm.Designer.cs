﻿
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq.Expressions;
using System.Linq;
using opencreature;

namespace Editor {
	partial class MainForm : Form {
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.TreeView TableList;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.ListBox RowList;
		private System.Windows.Forms.TabControl DisplayOutput;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage Elements_page;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private System.Windows.Forms.SplitContainer splitContainer4;
		private System.Windows.Forms.SplitContainer splitContainer5;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button ElementColorButton;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.TabPage tabPage6;
		private System.Windows.Forms.TabPage tabPage7;
		private System.Windows.Forms.TabPage tabPage8;
		private System.Windows.Forms.TabPage tabPage9;
		private System.Windows.Forms.TabPage tabPage10;
		private System.Windows.Forms.ColorDialog ElementColorDialog;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TableLayoutPanel ElementRow_Table;
		private System.Windows.Forms.SplitContainer splitContainer6;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.TabPage tabPage11;
		private System.Windows.Forms.ComboBox comboBox1;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		public void InitializeComponent() {
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Abilities");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Effects");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Egg Groups");
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Elements");
			System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Items");
			System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Item Classes");
			System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Moves");
			System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Natures");
			System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Species");
			System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Creature Data", new System.Windows.Forms.TreeNode[] {
			treeNode1,
			treeNode2,
			treeNode3,
			treeNode4,
			treeNode5,
			treeNode6,
			treeNode7,
			treeNode8,
			treeNode9});
			System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("NPCs");
			System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Plot Flags");
			System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("World Data", new System.Windows.Forms.TreeNode[] {
			treeNode11,
			treeNode12});
			System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Creaturedb", new System.Windows.Forms.TreeNode[] {
			treeNode10,
			treeNode13});
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.TableList = new System.Windows.Forms.TreeView();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.RowList = new System.Windows.Forms.ListBox();
			this.DisplayOutput = new System.Windows.Forms.TabControl();
			this.tabPage11 = new System.Windows.Forms.TabPage();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.Elements_page = new System.Windows.Forms.TabPage();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.splitContainer4 = new System.Windows.Forms.SplitContainer();
			this.splitContainer5 = new System.Windows.Forms.SplitContainer();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.ElementColorButton = new System.Windows.Forms.Button();
			this.splitContainer6 = new System.Windows.Forms.SplitContainer();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.ElementRow_Table = new System.Windows.Forms.TableLayoutPanel();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this.tabPage6 = new System.Windows.Forms.TabPage();
			this.tabPage7 = new System.Windows.Forms.TabPage();
			this.tabPage8 = new System.Windows.Forms.TabPage();
			this.tabPage9 = new System.Windows.Forms.TabPage();
			this.tabPage10 = new System.Windows.Forms.TabPage();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.ElementColorDialog = new System.Windows.Forms.ColorDialog();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.DisplayOutput.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.Elements_page.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
			this.splitContainer4.Panel1.SuspendLayout();
			this.splitContainer4.Panel2.SuspendLayout();
			this.splitContainer4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
			this.splitContainer5.Panel1.SuspendLayout();
			this.splitContainer5.Panel2.SuspendLayout();
			this.splitContainer5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).BeginInit();
			this.splitContainer6.Panel1.SuspendLayout();
			this.splitContainer6.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.ElementRow_Table.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.IsSplitterFixed = true;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.TableList);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new System.Drawing.Size(893, 896);
			this.splitContainer1.SplitterDistance = 170;
			this.splitContainer1.TabIndex = 0;
			// 
			// TableList
			// 
			this.TableList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TableList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TableList.Location = new System.Drawing.Point(0, 0);
			this.TableList.Name = "TableList";
			treeNode1.Name = "Abilities_node";
			treeNode1.Text = "Abilities";
			treeNode2.Name = "Effects_node";
			treeNode2.Text = "Effects";
			treeNode3.Name = "EggGroups_node";
			treeNode3.Text = "Egg Groups";
			treeNode4.Name = "Elements_node";
			treeNode4.Text = "Elements";
			treeNode5.Name = "Item_node";
			treeNode5.Text = "Items";
			treeNode6.Name = "ItemClass_node";
			treeNode6.Text = "Item Classes";
			treeNode7.Name = "Moves_node";
			treeNode7.Text = "Moves";
			treeNode8.Name = "Nature_node";
			treeNode8.Text = "Natures";
			treeNode9.Name = "Species_node";
			treeNode9.Text = "Species";
			treeNode10.Checked = true;
			treeNode10.Name = "CreatureData_node";
			treeNode10.Text = "Creature Data";
			treeNode11.Name = "NPC_node";
			treeNode11.Text = "NPCs";
			treeNode12.Name = "PlotFlag_node";
			treeNode12.Text = "Plot Flags";
			treeNode13.Checked = true;
			treeNode13.Name = "WorldData_node";
			treeNode13.Text = "World Data";
			treeNode14.Checked = true;
			treeNode14.Name = "Creaturedb_node";
			treeNode14.Text = "Creaturedb";
			this.TableList.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
			treeNode14});
			this.TableList.PathSeparator = "/";
			this.TableList.Size = new System.Drawing.Size(170, 896);
			this.TableList.TabIndex = 0;
			this.TableList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TableListAfterSelect);
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.RowList);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.DisplayOutput);
			this.splitContainer2.Size = new System.Drawing.Size(719, 896);
			this.splitContainer2.SplitterDistance = 200;
			this.splitContainer2.TabIndex = 0;
			// 
			// RowList
			// 
			this.RowList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RowList.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.RowList.FormattingEnabled = true;
			this.RowList.ItemHeight = 16;
			this.RowList.Items.AddRange(new object[] {
			"0001",
			"0002",
			"0003",
			"0004",
			"0005",
			"0006",
			"0007",
			"0008",
			"0009",
			"0010"});
			this.RowList.Location = new System.Drawing.Point(0, 0);
			this.RowList.Name = "RowList";
			this.RowList.Size = new System.Drawing.Size(200, 896);
			this.RowList.TabIndex = 0;
			this.RowList.SelectedIndexChanged += new System.EventHandler(this.RowListSelectedIndexChanged);
			// 
			// DisplayOutput
			// 
			this.DisplayOutput.Appearance = System.Windows.Forms.TabAppearance.Buttons;
			this.DisplayOutput.Controls.Add(this.tabPage11);
			this.DisplayOutput.Controls.Add(this.tabPage1);
			this.DisplayOutput.Controls.Add(this.tabPage2);
			this.DisplayOutput.Controls.Add(this.tabPage3);
			this.DisplayOutput.Controls.Add(this.Elements_page);
			this.DisplayOutput.Controls.Add(this.tabPage5);
			this.DisplayOutput.Controls.Add(this.tabPage6);
			this.DisplayOutput.Controls.Add(this.tabPage7);
			this.DisplayOutput.Controls.Add(this.tabPage8);
			this.DisplayOutput.Controls.Add(this.tabPage9);
			this.DisplayOutput.Controls.Add(this.tabPage10);
			this.DisplayOutput.Controls.Add(this.tabPage4);
			this.DisplayOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DisplayOutput.ItemSize = new System.Drawing.Size(5, 5);
			this.DisplayOutput.Location = new System.Drawing.Point(0, 0);
			this.DisplayOutput.Name = "DisplayOutput";
			this.DisplayOutput.SelectedIndex = 0;
			this.DisplayOutput.Size = new System.Drawing.Size(515, 896);
			this.DisplayOutput.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.DisplayOutput.TabIndex = 0;
			// 
			// tabPage11
			// 
			this.tabPage11.Location = new System.Drawing.Point(4, 9);
			this.tabPage11.Name = "tabPage11";
			this.tabPage11.Size = new System.Drawing.Size(507, 883);
			this.tabPage11.TabIndex = 11;
			this.tabPage11.Text = "Empty Page";
			this.tabPage11.UseVisualStyleBackColor = true;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.radioButton1);
			this.tabPage1.Location = new System.Drawing.Point(4, 9);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(507, 883);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Abilities";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// radioButton1
			// 
			this.radioButton1.Checked = true;
			this.radioButton1.Location = new System.Drawing.Point(210, 219);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(104, 24);
			this.radioButton1.TabIndex = 0;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "radioButton1";
			this.radioButton1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 9);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(507, 883);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Effects";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// tabPage3
			// 
			this.tabPage3.Location = new System.Drawing.Point(4, 9);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(507, 883);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Egg Groups";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// Elements_page
			// 
			this.Elements_page.Controls.Add(this.splitContainer3);
			this.Elements_page.Location = new System.Drawing.Point(4, 9);
			this.Elements_page.Name = "Elements_page";
			this.Elements_page.Size = new System.Drawing.Size(507, 883);
			this.Elements_page.TabIndex = 3;
			this.Elements_page.Text = "Elements";
			this.Elements_page.UseVisualStyleBackColor = true;
			// 
			// splitContainer3
			// 
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer3.IsSplitterFixed = true;
			this.splitContainer3.Location = new System.Drawing.Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.splitContainer4);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.AutoScroll = true;
			this.splitContainer3.Panel2.AutoScrollMargin = new System.Drawing.Size(17, 0);
			this.splitContainer3.Panel2.AutoScrollMinSize = new System.Drawing.Size(17, 0);
			this.splitContainer3.Panel2.Controls.Add(this.ElementRow_Table);
			this.splitContainer3.Panel2.Margin = new System.Windows.Forms.Padding(0, 0, 17, 0);
			this.splitContainer3.Panel2.Padding = new System.Windows.Forms.Padding(0, 0, 17, 0);
			this.splitContainer3.Size = new System.Drawing.Size(507, 883);
			this.splitContainer3.SplitterDistance = 83;
			this.splitContainer3.SplitterWidth = 1;
			this.splitContainer3.TabIndex = 0;
			// 
			// splitContainer4
			// 
			this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer4.IsSplitterFixed = true;
			this.splitContainer4.Location = new System.Drawing.Point(0, 0);
			this.splitContainer4.Name = "splitContainer4";
			this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer4.Panel1
			// 
			this.splitContainer4.Panel1.Controls.Add(this.splitContainer5);
			// 
			// splitContainer4.Panel2
			// 
			this.splitContainer4.Panel2.Controls.Add(this.splitContainer6);
			this.splitContainer4.Size = new System.Drawing.Size(507, 83);
			this.splitContainer4.SplitterDistance = 43;
			this.splitContainer4.TabIndex = 0;
			// 
			// splitContainer5
			// 
			this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer5.Location = new System.Drawing.Point(0, 0);
			this.splitContainer5.Name = "splitContainer5";
			// 
			// splitContainer5.Panel1
			// 
			this.splitContainer5.Panel1.Controls.Add(this.textBox1);
			// 
			// splitContainer5.Panel2
			// 
			this.splitContainer5.Panel2.Controls.Add(this.ElementColorButton);
			this.splitContainer5.Size = new System.Drawing.Size(507, 43);
			this.splitContainer5.SplitterDistance = 249;
			this.splitContainer5.TabIndex = 0;
			// 
			// textBox1
			// 
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBox1.Location = new System.Drawing.Point(0, 0);
			this.textBox1.MaxLength = 16;
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(249, 43);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "ELEMENT";
			// 
			// ElementColorButton
			// 
			this.ElementColorButton.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ElementColorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ElementColorButton.Location = new System.Drawing.Point(0, 0);
			this.ElementColorButton.Name = "ElementColorButton";
			this.ElementColorButton.Size = new System.Drawing.Size(254, 43);
			this.ElementColorButton.TabIndex = 0;
			this.ElementColorButton.Text = "Type Color";
			this.ElementColorButton.UseVisualStyleBackColor = true;
			this.ElementColorButton.Click += new System.EventHandler(this.ElementColorButtonClick);
			// 
			// splitContainer6
			// 
			this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer6.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer6.IsSplitterFixed = true;
			this.splitContainer6.Location = new System.Drawing.Point(0, 0);
			this.splitContainer6.Margin = new System.Windows.Forms.Padding(0);
			this.splitContainer6.Name = "splitContainer6";
			// 
			// splitContainer6.Panel1
			// 
			this.splitContainer6.Panel1.Controls.Add(this.tableLayoutPanel1);
			this.splitContainer6.Panel2MinSize = 16;
			this.splitContainer6.Size = new System.Drawing.Size(507, 36);
			this.splitContainer6.SplitterDistance = 481;
			this.splitContainer6.SplitterWidth = 1;
			this.splitContainer6.TabIndex = 0;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.5F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.5F));
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(481, 36);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(4, 1);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(208, 34);
			this.label1.TabIndex = 0;
			this.label1.Text = "Opposite Element";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label2
			// 
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(219, 1);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(125, 34);
			this.label2.TabIndex = 1;
			this.label2.Text = "On Attack";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label3
			// 
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(351, 1);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(126, 34);
			this.label3.TabIndex = 2;
			this.label3.Text = "On Defence";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
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
			this.ElementRow_Table.Controls.Add(this.comboBox1, 1, 0);
			this.ElementRow_Table.Dock = System.Windows.Forms.DockStyle.Top;
			this.ElementRow_Table.Location = new System.Drawing.Point(0, 0);
			this.ElementRow_Table.Name = "ElementRow_Table";
			this.ElementRow_Table.RowCount = 2;
			this.ElementRow_Table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.ElementRow_Table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.ElementRow_Table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.ElementRow_Table.Size = new System.Drawing.Size(490, 63);
			this.ElementRow_Table.TabIndex = 1;
			// 
			// comboBox1
			// 
			this.comboBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[] {
			"1x",
			"2x",
			"1/2x",
			"0x"});
			this.comboBox1.Location = new System.Drawing.Point(223, 4);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(127, 21);
			this.comboBox1.TabIndex = 2;
			// 
			// tabPage5
			// 
			this.tabPage5.Location = new System.Drawing.Point(4, 9);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Size = new System.Drawing.Size(507, 883);
			this.tabPage5.TabIndex = 4;
			this.tabPage5.Text = "Items";
			this.tabPage5.UseVisualStyleBackColor = true;
			// 
			// tabPage6
			// 
			this.tabPage6.Location = new System.Drawing.Point(4, 9);
			this.tabPage6.Name = "tabPage6";
			this.tabPage6.Size = new System.Drawing.Size(507, 883);
			this.tabPage6.TabIndex = 5;
			this.tabPage6.Text = "Item Classes";
			this.tabPage6.UseVisualStyleBackColor = true;
			// 
			// tabPage7
			// 
			this.tabPage7.Location = new System.Drawing.Point(4, 9);
			this.tabPage7.Name = "tabPage7";
			this.tabPage7.Size = new System.Drawing.Size(507, 883);
			this.tabPage7.TabIndex = 6;
			this.tabPage7.Text = "Moves";
			this.tabPage7.UseVisualStyleBackColor = true;
			// 
			// tabPage8
			// 
			this.tabPage8.Location = new System.Drawing.Point(4, 9);
			this.tabPage8.Name = "tabPage8";
			this.tabPage8.Size = new System.Drawing.Size(507, 883);
			this.tabPage8.TabIndex = 7;
			this.tabPage8.Text = "Natures";
			this.tabPage8.UseVisualStyleBackColor = true;
			// 
			// tabPage9
			// 
			this.tabPage9.Location = new System.Drawing.Point(4, 9);
			this.tabPage9.Name = "tabPage9";
			this.tabPage9.Size = new System.Drawing.Size(507, 883);
			this.tabPage9.TabIndex = 8;
			this.tabPage9.Text = "Species";
			this.tabPage9.UseVisualStyleBackColor = true;
			// 
			// tabPage10
			// 
			this.tabPage10.Location = new System.Drawing.Point(4, 9);
			this.tabPage10.Name = "tabPage10";
			this.tabPage10.Size = new System.Drawing.Size(507, 883);
			this.tabPage10.TabIndex = 9;
			this.tabPage10.Text = "NPCs";
			this.tabPage10.UseVisualStyleBackColor = true;
			// 
			// tabPage4
			// 
			this.tabPage4.Location = new System.Drawing.Point(4, 9);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(507, 883);
			this.tabPage4.TabIndex = 10;
			this.tabPage4.Text = "Plot Flags";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// ElementColorDialog
			// 
			this.ElementColorDialog.AnyColor = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(893, 896);
			this.Controls.Add(this.splitContainer1);
			this.Name = "MainForm";
			this.Text = "Editor";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.DisplayOutput.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.Elements_page.ResumeLayout(false);
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel2.ResumeLayout(false);
			this.splitContainer3.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
			this.splitContainer4.Panel1.ResumeLayout(false);
			this.splitContainer4.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
			this.splitContainer4.ResumeLayout(false);
			this.splitContainer5.Panel1.ResumeLayout(false);
			this.splitContainer5.Panel1.PerformLayout();
			this.splitContainer5.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
			this.splitContainer5.ResumeLayout(false);
			this.splitContainer6.Panel1.ResumeLayout(false);
			this.splitContainer6.Panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).EndInit();
			this.splitContainer6.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ElementRow_Table.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
