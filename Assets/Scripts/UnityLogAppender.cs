using System;
using UnityEngine;
using log4net.Appender;
using log4net;
using log4net.Core;

public class UnityLogAppender : AppenderSkeleton {
	protected override void Append(LoggingEvent e) {
		#if UNITY_EDITOR
		var level = e.Level;
		switch(level.ToString()) {
			case "Trace":
			case "Debug":
			case "Info":
			case "Notice":
				UnityEngine.Debug.Log(e.RenderedMessage);
				break;
			case "Warn":
				UnityEngine.Debug.LogWarning(e.RenderedMessage);
				break;
			case "Error":
			case "Critical":
				UnityEngine.Debug.LogError(e.RenderedMessage);
				break;
		}
		#endif
	}
}