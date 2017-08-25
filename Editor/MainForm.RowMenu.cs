
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq.Expressions;
using System.Linq;
using opencreature;

namespace Editor {
	public partial class MainForm : Form {
		public int currentRowId = 0;
		public Dictionary<string,Action> IndexChanged;
		
		void initDisplayCallback() {
			IndexChanged = new Dictionary<string, Action>();
			IndexChanged.Add("Elements"     , ElementUpdate);
			IndexChanged.Add("Abilities"    , ElementUpdate);
			IndexChanged.Add("Egg Groups"   , ElementUpdate);
			IndexChanged.Add("Items"        , ElementUpdate);
			IndexChanged.Add("Item Classes" , ElementUpdate);
			IndexChanged.Add("Moves"        , ElementUpdate);
			IndexChanged.Add("Natures"      , ElementUpdate);
			IndexChanged.Add("Effects"      , ElementUpdate);
			IndexChanged.Add("Species"      , ElementUpdate);
			IndexChanged.Add("NPCs"         , ElementUpdate);
			IndexChanged.Add("Plot Flags"   , ElementUpdate);
		}

		DeserializedElement[] getTableRowNames (string table) {
			DeserializedElement[] tabledata = null;
			switch (table) {
				case "Elements"     : tabledata = (DeserializedElement[])opencreature.Element.TYPES.Values.ToArray()         ; break;
				case "Abilities"    : tabledata = (DeserializedElement[])opencreature.Ability.ABILITIES.Values.ToArray()     ; break;
				case "Egg Groups"   : tabledata = (DeserializedElement[])opencreature.EggGroup.EGG_GROUPS.Values.ToArray()   ; break;
				case "Items"        : tabledata = (DeserializedElement[])opencreature.Item.ITEMS.Values.ToArray()            ; break;
				case "Item Classes" : tabledata = (DeserializedElement[])opencreature.ItemType.ITEM_TYPES.Values.ToArray()   ; break;
				case "Moves"        : tabledata = (DeserializedElement[])opencreature.Move.MOVES.Values.ToArray()            ; break;
				case "Natures"      : tabledata = (DeserializedElement[])opencreature.Nature.NATURES.Values.ToArray()        ; break;
				case "Effects"      : tabledata = (DeserializedElement[])opencreature.Effect.EFFECTS.Values.ToArray()        ; break;
				case "Species"      : tabledata = (DeserializedElement[])opencreature.Species.SPECIES.Values.ToArray()       ; break;
				case "NPCs"         : tabledata = (DeserializedElement[])opencreature.Npc.NPCS.Values.ToArray()              ; break;
				case "Plot Flags"   : tabledata = (DeserializedElement[])opencreature.PlotFlag.PLOT_FLAG_ID.Values.ToArray() ; break;
			}
			return tabledata;
		}
		
		void RowListSelectedIndexChanged(object sender, EventArgs e) {
			int rowIndex = RowList.SelectedIndex;
			currentRowId = getTableRowNames(currentTable)[RowList.SelectedIndex].id;
			IndexChanged[currentTable].Invoke();
			//currentRowId = ((DeserializedElement)RowList.SelectedValue).id;
		}
	}
}
