using UnityEngine;
using System.IO;
public class DebugLogWriter : TextWriter {
    public DebugLogWriterDestination dest;
    public StreamWriter logfile;
    public DebugLogWriter(DebugLogWriterDestination dest) {
        this.dest = dest;
        switch (dest) {
            case DebugLogWriterDestination.output: 
                File.AppendText("debug.out");
                break;
            case DebugLogWriterDestination.error:
                File.AppendText("debug.err");
                break;
            case DebugLogWriterDestination.warning:
                File.AppendText("debug.warn");
                break;
        }
    }
    public override void Write(string value) {
        base.Write(value);
        logfile.Write(value);
        switch (dest) {
            case DebugLogWriterDestination.output:
                Debug.Log(value);
                break;
            case DebugLogWriterDestination.error:
                Debug.LogError(value);
                break;
            case DebugLogWriterDestination.warning:
                Debug.LogWarning(value);
                break;
        }
    }
    public override System.Text.Encoding Encoding {
        get { return System.Text.Encoding.UTF8; }
    }
}
public enum DebugLogWriterDestination {
    output,
    error,
    warning
}