using System;
using System.Collections.Generic;
using System.Linq;

namespace opencreature {
    public class BattleType : DeserializedElement {
        public static Dictionary<int, BattleType> BATTLE_TYPES;

        String name;
        Dictionary<int, int> teamCreatureCounts;
        Dictionary<int, Tuple<int,int>> teamAlliances;

        public BattleType(int id, String name) {
            this.id = id;
            this.name = name;
            teamCreatureCounts = new Dictionary<int, int>();
            teamAlliances = new Dictionary<int, Tuple<int,int>>();
        }

        public static long init(
            List<Dictionary<string, string>> battle_defs,
            List<Dictionary<string, string>> alliance_defs
        ) {
            BATTLE_TYPES = new Dictionary<int, BattleType>(battle_defs.Count);
            foreach (var battle_def in battle_defs) {
                int battle_type_id = Convert.ToInt32(battle_def["battle_type_id"]);

                BattleType temp = BATTLE_TYPES.ContainsKey(battle_type_id)
                    ? BATTLE_TYPES[battle_type_id]
                    : new BattleType(
                        battle_type_id,
                        battle_def["name"]
                    );

                temp.teamCreatureCounts.Add(
                    Convert.ToInt32(battle_def["battle_team_id"]),
                    Convert.ToInt32(battle_def["team_creature_count"])
                );
            }

            foreach (var alliance_def in alliance_defs) {
                int battle_type_id = Convert.ToInt32(alliance_def["battle_type_id"]);
                BattleType temp = BATTLE_TYPES[battle_type_id];

                int team_a = Convert.ToInt32(alliance_def["battle_type_id_A"]);
                int team_b = Convert.ToInt32(alliance_def["battle_type_id_B"]);

                temp.teamAlliances.Add(battle_type_id, new Tuple<int, int>(team_a, team_b));
                temp.teamAlliances.Add(battle_type_id, new Tuple<int, int>(team_b, team_a));
            }
            
            return BATTLE_TYPES.Count;
        }

    }
}
