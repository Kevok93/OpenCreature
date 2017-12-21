using System;
using System.Collections.Generic;
using System.Dynamic;

namespace opencreature {
    public abstract class CompiledScript {
        public abstract int execute(params  Object[] args);
        public abstract int execute(params  KeyValuePair<String,Object>[] args);

        public static CompiledScript scriptFactory(String name, String text, params String[] args) {
            return new LuaScript(name, text, args);
        }
    }
}
