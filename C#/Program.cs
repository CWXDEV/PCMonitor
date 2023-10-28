using System.Timers;
using Spectre.Console;
using LibreHardwareMonitor.Hardware;
using PCMonitorConsoleApp.Helpers;
using Timer = System.Timers.Timer;

namespace PCMonitorConsoleApp
{
    internal static class Program
    {
        private static ManualResetEvent _quitEvent = new ManualResetEvent(false);
        
        public static void Main()
        {
            Console.CancelKeyPress += ( _, args ) =>
            {
                _quitEvent.Set();
                args.Cancel = true;
            };

            var timer = new Timer();
            timer.Interval = 5000;
            timer.Elapsed += OnUpdate;
            timer.Start();

            _quitEvent.WaitOne();
        }

        public static void OnUpdate(object? sender, ElapsedEventArgs elapsedEventArgs)
        {
            HardwareHelper.GetInstance?.UpdateHardware();
            var test = HardwareHelper.GetInstance?.GetCpu();
            var cpu1 = test?.Sensors.FirstOrDefault(x => x.SensorType is SensorType.Load);
            
            AnsiConsole.WriteLine($"CPU1 Load: {cpu1?.Value ?? 0}");
        }
    }
}