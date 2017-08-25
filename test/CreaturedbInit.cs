using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

[TestFixture()]
public class CreaturedbInit {
	[Test()]
	public void TestCase () {
		List<Dictionary<string,string>>
			species = new List<Dictionary<string,string>>() {
				{
					id = 1,
					name = "Bulbasaur",
					ability1 = 104,
					ability2 = 17,
					gender_ratio = 223,
					type1 = 11,
					type2 = 3,
					capture_rate = 45,
					max_exp = 1059860,
					ev_type = 3,
					ev_val = 1,
					base_atk = 49,
					base_def = 49,
					base_spatk = 65,
					base_spdef = 65,
					base_hp = 45,
					base_speed = 45,
					max_atk = 103,
					max_def = 103,
					max_spatk = 135,
					max_spdef = 135,
					max_hp = 200,
					max_speed = 95,
					tm_list = "800014C48B8010808023341084398720"
					misc_info = 00
					egg_steps = 32767
					egg_group1 = 0
					egg_group2 = 0
					wild_item = 0
					wild_item_pct = 0
					height = 2.6
					weight = 50
					classification = Bulb
					dex_entry = BulbaBulba
					cry = 0
					sprite_path = asdf
			},{
					id = 2
					name = Ivysaur
					ability1 = 104
					ability2 = 17
					gender_ratio = 223
					type1 = 11
					type2 = 3
					capture_rate = 45
					max_exp = 1059860
					ev_type = 3
					ev_val = 1
					base_atk = 62
					base_def = 63
					base_spatk = 80
					base_spdef = 80
					base_hp = 60
					base_speed = 60
					max_atk = 129
					max_def = 131
					max_spatk = 165
					max_spdef = 165
					max_hp = 230
					max_speed = 125
					tm_list = 00000000000000000000000000000000
					misc_info = 00
					egg_steps = 5120
					egg_group1 = 0
					egg_group2 = 0
					wild_item = 0
					wild_item_pct = 0
					height = 1
					weight = 13
					classification = Seed
					dex_entry = When the bud on its back starts swelling, a sweet aroma wafts to indicate the flower's coming bloom.
					cry = 0
					sprite_path = asdf
				}
			};

	}
}

