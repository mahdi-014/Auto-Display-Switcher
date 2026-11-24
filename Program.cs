using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

internal static class Program
{
    private static readonly string LogDirectory =
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "AutoDisplaySwitcher"
        );

    private static readonly string LogFile =
        Path.Combine(LogDirectory, "diagnostic.log");

    private static void Log(string message)
    {
#if DEBUG
        try
        {
            Directory.CreateDirectory(LogDirectory);
            File.AppendAllText(
                LogFile,
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] {message}{Environment.NewLine}"
            );
        }
        catch { }
#endif
    }

    private static async Task Main()
    {
        Log("MAIN ENTRY: Application execution started.");

        try
        {
            using var mutex = new System.Threading.Mutex(
                true,
                @"Local\AutoDisplaySwitcher",
                out bool createdNew
            );

            Log($"MUTEX CHECK: createdNew = {createdNew}");

            if (!createdNew)
            {
                Log("EXIT: Another instance is already running.");
                return;
            }

            Log("Application started.");

            await MonitorPower();
        }
        catch (Exception ex)
        {
            Log("FATAL ERROR: " + ex);
        }
    }

    private static async Task MonitorPower()
    {
        int? previousAc = null;
        DateTime changeTime = DateTime.MinValue;
        string lastAppliedMode = "";

        while (true)
        {
            try
            {
                if (NativeMethods.GetSystemPowerStatus(out var power))
                {
                    int ac = power.ACLineStatus;

                    if (ac == 255)
                    {
                        await Task.Delay(250);
                        continue;
                    }

                    if (previousAc != ac)
                    {
                        previousAc = ac;
                        changeTime = DateTime.UtcNow;

                        Log(ac == 1
                            ? "Power source changed: AC CONNECTED."
                            : "Power source changed: BATTERY.");
                    }

                    if ((DateTime.UtcNow - changeTime).TotalMilliseconds >= 400)
                    {
                        if (ac == 1 && lastAppliedMode != "extend")
                        {
                            Log("AC connected. Applying EXTEND.");

                            if (RunDisplaySwitch("/extend"))
                                lastAppliedMode = "extend";
                        }
                        else if (ac == 0 && lastAppliedMode != "internal")
                        {
                            Log("Battery detected. Applying PC SCREEN ONLY.");

                            if (RunDisplaySwitch("/internal"))
                                lastAppliedMode = "internal";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log("ERROR: " + ex);
            }

            await Task.Delay(250);
        }
    }

    private static bool RunDisplaySwitch(string argument)
    {
        try
        {
            string exe = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Windows),
                "System32",
                "DisplaySwitch.exe"
            );

            using var process = Process.Start(new ProcessStartInfo
            {
                FileName = exe,
                Arguments = argument,
                UseShellExecute = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            });

            if (process == null)
            {
                Log("ERROR: DisplaySwitch process could not start.");
                return false;
            }

            if (!process.WaitForExit(3000))
            {
                Log("ERROR: DisplaySwitch timed out.");
                process.Kill();
                return false;
            }

            Log($"DisplaySwitch {argument} exited with code {process.ExitCode}.");

            return process.ExitCode == 0;
        }
        catch (Exception ex)
        {
            Log("DisplaySwitch error: " + ex.Message);
            return false;
        }
    }
}

internal static class DisplayDetector
{
    private const uint DISPLAY_DEVICE_ATTACHED_TO_DESKTOP = 0x00000001;

    public static int CountActiveMonitors()
    {
        int count = 0;

        for (uint i = 0; ; i++)
        {
            var adapter = NewDevice();

            if (!NativeMethods.EnumDisplayDevices(
                null, i, ref adapter, 0))
                break;

            if ((adapter.StateFlags & DISPLAY_DEVICE_ATTACHED_TO_DESKTOP) == 0)
                continue;

            for (uint j = 0; ; j++)
            {
                var monitor = NewDevice();

                if (!NativeMethods.EnumDisplayDevices(
                    adapter.DeviceName, j, ref monitor, 0))
                    break;

                if ((monitor.StateFlags & DISPLAY_DEVICE_ATTACHED_TO_DESKTOP) != 0)
                    count++;
            }
        }

        return count;
    }

    private static NativeMethods.DISPLAY_DEVICE NewDevice()
    {
        return new NativeMethods.DISPLAY_DEVICE
        {
            cb = Marshal.SizeOf<NativeMethods.DISPLAY_DEVICE>(),
            DeviceName = "",
            DeviceString = "",
            DeviceID = "",
            DeviceKey = ""
        };
    }
}

internal static class NativeMethods
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct SYSTEM_POWER_STATUS
    {
        public byte ACLineStatus;
        public byte BatteryFlag;
        public byte BatteryLifePercent;
        public byte SystemStatusFlag;
        public uint BatteryLifeTime;
        public uint BatteryFullLifeTime;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct DISPLAY_DEVICE
    {
        public int cb;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string DeviceName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceString;

        public uint StateFlags;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceID;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceKey;
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern bool GetSystemPowerStatus(
        out SYSTEM_POWER_STATUS status
    );

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool EnumDisplayDevices(
        string? device,
        uint deviceNumber,
        ref DISPLAY_DEVICE displayDevice,
        uint flags
    );
}
