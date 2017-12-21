using System;
using System.Collections.Generic;

namespace opencreature {
    public class TargetList {
        private CompiledScript targetingScript;

        public TargetList(String name, String function_text) {
            this.targetingScript = CompiledScript.scriptFactory(name, function_text, "self","teams","alliances","targets");
        }

        public byte[] getTargets(byte self, List<List<Byte>> teams, List<List<Byte>> alliances) {
            List<byte> targets = new List<byte>();
            this.targetingScript.execute(self, teams, alliances, targets);
            return targets.ToArray();
        }
    }
}
