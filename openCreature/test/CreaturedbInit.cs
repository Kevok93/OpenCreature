using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using opencreature; 

namespace Tests{
	[TestFixture]
	public class CreaturedbInit {
		[Test]
		[SetUp]
		public void Initialization () {
			Creaturedb.initialize();
			Assert.False(Creaturedb.initialize());
		}

        [Test]
        public void CheckForNulls() {
            foreach (string DeserializedElementDictionary_name in Creaturedb.TABLE_OBJECTS.Keys) {
                TypeCastDictionary<int, DeserializedElement> DeserializedElementDictionary_collection = Creaturedb.TABLE_OBJECTS[DeserializedElementDictionary_name];
                foreach (DeserializedElement element in DeserializedElementDictionary_collection.Values) {
                    Assert.IsNotNull(element);
                    Assert.IsNotNull(element.id);
                    Assert.IsNotNull(element.ToString());
                    Assert.AreSame(element, DeserializedElementDictionary_collection[element.id]);
                }
            }
        }

	    [Test]
	    public void CheckLinksSpecies() {
	        foreach (Species s in Species.SPECIES.Values) {
	            if (s.ability_id1   != 0) Assert.IsNotNull(s.ability1   );
	            if (s.ability_id2   != 0) Assert.IsNotNull(s.ability2   );
	            if (s.type_id1      != 0) Assert.IsNotNull(s.type1      );
	            if (s.type_id2      != 0) Assert.IsNotNull(s.type2      );
	            if (s.egg_group_id1 != 0) Assert.IsNotNull(s.egg_group1 );
	            if (s.egg_group_id2 != 0) Assert.IsNotNull(s.egg_group2 );
	            if (s.wild_item_id  != 0) Assert.IsNotNull(s.wild_item  );
	        }
	    }

	    [Test]
	    public void CheckLinksAbilties() {
	        foreach (Ability a in Ability.ABILITIES.Values) {
	            if (a.world_effect_id  != 0) Assert.IsNotNull(a.world_effect );
	            if (a.battle_effect_id != 0) Assert.IsNotNull(a.battle_effect);
	        }
	    }

	    [Test]
	    public void CheckLinksNPCs() {
	        foreach (Npc n in Npc.NPCS.Values) {
	            Assert.IsNotNull(n.trainer );
	            Assert.IsNotNull(n.style   );
	        }
	    }

	    [Test]
	    public void CheckLinksMoves() {
	        foreach (Move m in Move.MOVES.Values) {
	            if (m.world_effect_id  != 0) Assert.IsNotNull(m.world_effect );
	            if (m.battle_effect_id != 0) Assert.IsNotNull(m.battle_effect);
	            Assert.IsNotNull (m.movetype);
	        }
	    }

	    [Test]
	    public void CheckLinksItems() {
	        foreach (Item i in Item.ITEMS.Values) {
	            if (i.world_effect_id  != 0) Assert.IsNotNull(i.world_effect  );
	            if (i.battle_effect_id != 0) Assert.IsNotNull(i.battle_effect );
	            if (i.held_effect_id   != 0) Assert.IsNotNull(i.held_effect   );
	            if (i.item_type_id     != 0) Assert.IsNotNull(i.item_type     );
	        }
	    }

	}
}