using System;
using log4net;
using log4net.Core;

public static class GlobalNamespace {    
    public static void Trace(this ILog log, string message, Exception exception) {
		log.Logger.Log(
    		typeof(LogImpl), log4net.Core.Level.Trace, message, exception
		);
    }
    public static void Verbose(this ILog log, string message, Exception exception) {
        log.Logger.Log(
    		typeof(LogImpl), log4net.Core.Level.Verbose, message, exception
        );
    }

    public static void Trace(this ILog log, string message) {log.Trace(message, null);}
    public static void Verbose(this ILog log, string message) {log.Verbose(message, null);}
}