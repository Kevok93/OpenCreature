using System;
using UnityEngine;
using log4net.Appender;
using log4net;
using log4net.Core;

public class UnityLogAppender : AppenderSkeleton {
	protected override void Append(LoggingEvent e) {
		#if !COMMAND_LINE
		var level = e.Level;
		switch(level.ToString()) {
			case "TRACE":
			case "DEBUG":
			case "INFO":
			case "NOTICE":
				UnityEngine.Debug.Log(e.RenderedMessage);
				break;
			case "WARN":
				UnityEngine.Debug.LogWarning(e.RenderedMessage);
				break;
			case "ERROR":
			case "CRITICAL":
				UnityEngine.Debug.LogError(e.RenderedMessage);
				break;
			default:
				log4net.Util.LogLog.Error(typeof(UnityLogAppender),"Log level "+level.ToString()+" Not found!");
				break;
		}
		#endif
	}
}