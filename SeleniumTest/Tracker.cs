using System.Diagnostics;

public class Tracker
{
    private long peakMemoryUsage = 0;
    private Stopwatch _watch;
    private Process _browserProcess;


    public void SetupAndStartMonitoring()
    {
        _browserProcess = Process.GetProcessesByName("Google Chrome").First();
    }
    
    public void StartTime()
    {
        _watch = Stopwatch.StartNew();
    }
    
    public void StopTime()
    {
        _watch.Stop();
    }
    
    public long GetTime()
    {
        return _watch.ElapsedMilliseconds;
    }

    public long GetMemoryUsage()
    {
        return _browserProcess.WorkingSet64;
    }
    
    public double GetCpuUsage()
    {
        return _browserProcess.TotalProcessorTime.TotalMilliseconds;
    }
}