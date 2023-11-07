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
            timer.Interval = 1000;
            timer.Elapsed += OnUpdate;
            timer.Start();

            _quitEvent.WaitOne();
        }

        public static void OnUpdate(object? sender, ElapsedEventArgs elapsedEventArgs)
        {
            HardwareHelper.GetInstance?.UpdateHardware();
            var test = HardwareHelper.GetInstance?.GetCpu();
            var cpu = test?.Sensors.FirstOrDefault(x => x.SensorType is SensorType.Load);

            var test2 = HardwareHelper.GetInstance?.GetGpu();
            var gpu = test2?.Sensors.FirstOrDefault(x => x.SensorType == SensorType.Load);
            
            AnsiConsole.WriteLine($"CPU Load: {cpu?.Value ?? 0}");
            AnsiConsole.WriteLine($"GPU Load: {gpu?.Value ?? 0}");
        }
    }
}