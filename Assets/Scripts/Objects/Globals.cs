using System;
using System.Collections.Generic;

public struct Globals {
    public static Random RNG;
    public static void init() {
        RNG = new Random(57760);
    }
    public static void Main(string[] args){
        Creaturedb.initialize();
        Console.Out.WriteLine("hi");
        Console.In.Read();
    }
}