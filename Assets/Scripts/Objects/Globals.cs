using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using log4net;
using log4net.Core;
using log4net.Appender;
using log4net.Layout;
using System.Reflection;

namespace opencreature {
public static class Globals {
    public static Random RNG;
    public static uint LAST_CAUGHT_ID = 0;
    public static ILog log;
    public static String binary_location;
    public static String binary_directory;
    
    static Globals() {
        SetupLogging();
        log = log4net.LogManager.GetLogger("OpenCreature");
        log.Debug("Logger initialized");
        RNG = new Random(57760); 
        binary_location = Assembly.GetCallingAssembly().Location;
        binary_directory = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar;
		Directory.GetFiles(binary_directory).AsEnumerable().ToList().ForEach(Console.Error.WriteLine);
    }
    
    public static void WriteWithPrefix(this TextWriter output, string value, string prefix) {
        log = log4net.LogManager.GetLogger("|"+prefix);
        log.Info(value);
    }
    public static IEnumerable<t> Randomize<t>(this IEnumerable<t> target){
        return target.OrderBy(x=>(RNG.Next()));
    }   
    public static float NextFloat(this Random rng) {
		return (float)rng.NextDouble();
    }
    
    public static void SetupLogging() {
        var hierarchy = (log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository();

        var patternLayout = new PatternLayout();
        patternLayout.ConversionPattern = "[%05thread] %-5level %20logger - %message%newline";
        patternLayout.ActivateOptions();
        
        var console = new ManagedColoredConsoleAppender();
        var TRACE = new ManagedColoredConsoleAppender.LevelColors(); TRACE.Level = Level.Trace; TRACE.ForeColor = ConsoleColor.White  ;
        var DEBUG = new ManagedColoredConsoleAppender.LevelColors(); DEBUG.Level = Level.Debug; DEBUG.ForeColor = ConsoleColor.Green  ;
        var INFO  = new ManagedColoredConsoleAppender.LevelColors(); INFO .Level = Level.Info ; INFO .ForeColor = ConsoleColor.Cyan ;
        var WARN  = new ManagedColoredConsoleAppender.LevelColors(); WARN .Level = Level.Warn ; WARN .ForeColor = ConsoleColor.Yellow;
        var ERROR = new ManagedColoredConsoleAppender.LevelColors(); ERROR.Level = Level.Error; ERROR.ForeColor = ConsoleColor.Magenta   ;
        var FATAL = new ManagedColoredConsoleAppender.LevelColors(); FATAL.Level = Level.Fatal; FATAL.ForeColor = ConsoleColor.Red   ;
        console.AddMapping(TRACE);
        console.AddMapping(DEBUG);
        console.AddMapping(INFO );
        console.AddMapping(WARN );
        console.AddMapping(ERROR);
        console.AddMapping(FATAL);
        console.ActivateOptions();
        console.Layout = patternLayout;
        hierarchy.Root.AddAppender(console);
        
        var unity = new UnityLogAppender();
        console.Layout = patternLayout;
        hierarchy.Root.AddAppender(unity);
        
        hierarchy.Root.Level = Level.Trace;
        hierarchy.Configured = true;
    }
    
}
}