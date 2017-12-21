using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Neo.IronLua;
using NUnit.Framework;
using opencreature;

namespace Tests {
    [TestFixture]
    public class LUATest {
        private Lua lua;
        private LuaGlobalPortable lua_environment;

        [SetUp]
        [Test]
        public void Initialize() {
            this.lua = new Lua();
            lua_environment = lua.CreateEnvironment();
        }

        [Test]
        public void TestReturn() {
            foreach (int i in Enumerable.Range(1,10)) {
                int result = lua_environment.DoChunk(
                    "return testObject",
                    "TestReturn",
                    new KeyValuePair<string, object>("testObject", i)
                ).ToInt32();
                Assert.AreEqual(i, result);
            }
        }
        
        [Test]
        public void TestAccess() {
            String testObject = "Hello World!";
            StringComparison comparator = StringComparison.Ordinal;
            int result = lua_environment.DoChunk(
                "return testObject.IndexOf('World', Ordinal)",
                "TestAccess",
                new KeyValuePair<string, object>("testObject", testObject),
                new KeyValuePair<string, object>("Ordinal", comparator)
            ).ToInt32();
            Assert.AreEqual(testObject.IndexOf("World", comparator), result);
        }
        
        [Test]
        public void TestCompile() {
            LuaChunk function = lua.CompileChunk(
                "return testObject",
                "TestCompile",
                null,
                new KeyValuePair<String,Type>("testObject",typeof(int))
            );
            foreach (int i in Enumerable.Range(1,10)) {
                Assert.AreEqual(i, function.Run(this.lua_environment, i).ToInt32());
            }
        }
        
        
        [Test]
        public void TestLambda() {
            Delegate function = lua.CreateLambda(
                "TestCompile",
                "return testObject",
                null,
                typeof(int),
                new KeyValuePair<String,Type>("testObject",typeof(int))
            );
            foreach (int i in Enumerable.Range(1,10)) {
                Assert.AreEqual(i, function.DynamicInvoke(i));
            }
        }
        
        [Test]
        public void CompiledScript() {
            CompiledScript function = opencreature.CompiledScript.scriptFactory(
                "TestCompile",
                "return testObject",
                "testObject"
            );
            foreach (int i in Enumerable.Range(1,10)) {
                Assert.AreEqual(i, function.execute(i));
            }
        }

        [Test]
        public void CompiledScriptWithNoReturn() {
            var testObject = new Dictionary<int, int>();
            CompiledScript function = opencreature.CompiledScript.scriptFactory(
                "TestCompile",
                "testObject[testValue] = testValue",
                "testObject",
                "testValue"
            );
            foreach (int i in Enumerable.Range(1,10)) {
                int retc = function.execute(testObject, i);
                Assert.IsTrue(testObject.ContainsKey(i));
                Assert.AreEqual(i, testObject[i]);
                Assert.AreEqual(0,retc);
            }
        }
        
        
    }
}
