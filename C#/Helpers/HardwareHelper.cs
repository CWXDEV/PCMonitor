using LibreHardwareMonitor.Hardware;
using PCMonitorConsoleApp.Models;

namespace PCMonitorConsoleApp.Helpers;

public sealed class HardwareHelper
{
    private static HardwareHelper? _instance;
    private static readonly object Padlock = new object();
    private static Computer? _computer;
    
    private HardwareHelper()
    {
        _computer = new Computer()
        {
            IsCpuEnabled = true,
            IsBatteryEnabled = true,
            IsControllerEnabled = true,
            IsGpuEnabled = true,
            IsMemoryEnabled = true,
            IsMotherboardEnabled = true,
            IsNetworkEnabled = true,
            IsPsuEnabled = true,
            IsStorageEnabled = true
        };
        
        _computer.Open();
        _computer.Accept(new UpdateVisitor());
    }

    public static HardwareHelper? GetInstance
    {
        get
        {
            lock (Padlock)
            {
                return _instance ??= new HardwareHelper();
            }
        }
    }

    public IHardware? GetCpu()
    {
        return _computer?.Hardware.FirstOrDefault(x => x.HardwareType is HardwareType.Cpu);
    }

    public IHardware? GetGpu()
    {
        return _computer?.Hardware.FirstOrDefault(x =>
            x.HardwareType is HardwareType.GpuNvidia or HardwareType.GpuAmd or HardwareType.GpuIntel);
    }

    public void UpdateHardware()
    {
        foreach (var hardware in _computer.Hardware)
        {
            hardware.Update();
        }
    }
}