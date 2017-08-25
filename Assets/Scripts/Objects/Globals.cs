using System;
using System.Collections.Generic;
using System.IO;

public static class Globals {
    public static Random RNG;
    public static uint LAST_CAUGHT_ID = 0;
    public static void init() {
        RNG = new Random(57760);
    }
    
    public static void WriteWithPrefix(this TextWriter output, string value, string prefix) {
    	output.Write(String.Format(" {0,12} | {1}\n", prefix,value));
    }
}