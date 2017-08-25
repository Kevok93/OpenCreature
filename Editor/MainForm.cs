/*
 * Created by SharpDevelop.
 * User: kwest
 * Date: 6/7/2016
 * Time: 16:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq.Expressions;
using System.Linq;
using opencreature;

namespace Editor
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form {
		public static String rowname_format = "{0,4:D} : {1}";
		public MainForm() {
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			opencreature.Creaturedb.initialize();
			initDisplayCallback();
			InitializeComponent();
			updateRowList();
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
	}
}