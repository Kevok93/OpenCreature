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
	    binary_directory = Path.GetDirectoryName(binary_location) + Path.DirectorySeparatorChar;
		Directory.GetFiles(binary_directory).AsEnumerable().ToList().ForEach(Console.Error.WriteLine);
    }
    
    public static IEnumerable<t> Randomize<t>(this IEnumerable<t> target){
        return target.OrderBy(x=>(RNG.Next()));
    }   
    public static float NextFloat(this Random rng) {
		return (float)rng.NextDouble();
    }
    public static float NextFloatBetween(this Random rng, float min, float max) {
    	float range = Math.Abs(max - min);
    	return (rng.NextFloat() * range) + min;
    }
    public static t_val Dequeue<t_val>(this SortedSet<t_val> queue) {
    	t_val value = queue.FirstOrDefault();
    	queue.Remove(value);
    	return value;
    }

    public static void SetupLogging() {
        var hierarchy = (log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository();

        var patternLayout = new PatternLayout();
        patternLayout.ConversionPattern = "[%05thread] %-5level %20logger - %message%newline";
        patternLayout.ActivateOptions();
        
        var console = new ManagedColoredConsoleAppender();
        var TRACE   = new ManagedColoredConsoleAppender.LevelColors{Level = Level.Trace, ForeColor = ConsoleColor.White   } ;
        var DEBUG   = new ManagedColoredConsoleAppender.LevelColors{Level = Level.Debug, ForeColor = ConsoleColor.Green   } ;
        var INFO    = new ManagedColoredConsoleAppender.LevelColors{Level = Level.Info , ForeColor = ConsoleColor.Cyan    } ;
        var WARN    = new ManagedColoredConsoleAppender.LevelColors{Level = Level.Warn , ForeColor = ConsoleColor.Yellow  } ;
        var ERROR   = new ManagedColoredConsoleAppender.LevelColors{Level = Level.Error, ForeColor = ConsoleColor.Magenta } ;
        var FATAL   = new ManagedColoredConsoleAppender.LevelColors{Level = Level.Fatal, ForeColor = ConsoleColor.Red     } ;

        console.AddMapping( TRACE );
        console.AddMapping( DEBUG );
        console.AddMapping( INFO  );
        console.AddMapping( WARN  );
        console.AddMapping( ERROR );
        console.AddMapping( FATAL );

        console.ActivateOptions();
        console.Layout = patternLayout;
        hierarchy.Root.AddAppender(console);
        
        hierarchy.Root.Level = Level.Trace;
        hierarchy.Configured = true;
    }

	public static sbyte toRoundedSbyte(Object byte_obj) {
		byte byte_val = Convert.ToByte(byte_obj);
		sbyte sbyte_val = (sbyte)(
			(byte_val > sbyte.MaxValue) 
				? byte_val - 256
				: byte_val
		);
		return sbyte_val;
	}
    
}
}