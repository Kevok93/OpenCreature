
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Neo.IronLua;

namespace opencreature {
    public class LuaScript : CompiledScript {
        private static Lua lua;
        private LuaGlobal lua_environment;
        private LuaChunk function;
        private String[] orderedVariableNames;

        public LuaScript(String name, String text, params String[] args) {
            if (lua == null) lua = new Lua();
            this.lua_environment = lua.CreateEnvironment();
            this.orderedVariableNames = args;
            this.function = lua.CompileChunk(
                text,
                name,
                null,
                Enumerable.ToArray(args.Select(x => new KeyValuePair<String, Type>(x, typeof(Object))))
            );
        }

        public override int execute(params Object[] args) {
            return executeLua(args).ToInt32();
        }

        public override int execute(params  KeyValuePair<String,Object>[] args) {
            return executeLua(args.Select(x=>x.Value).ToArray()).ToInt32();
        }

        public LuaResult executeLua(params Object[] args) {
            return function.Run(this.lua_environment, args);
        }
        
    }
}
