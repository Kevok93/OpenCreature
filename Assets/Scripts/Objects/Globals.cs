using System;
using System.Collections.Generic;

public struct Globals {
    public static Random RNG;
    public static uint LAST_CAUGHT_ID = 0;
    public static void init() {
        RNG = new Random(57760);
    }
}