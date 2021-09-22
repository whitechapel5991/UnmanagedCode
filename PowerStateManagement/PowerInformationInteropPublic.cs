using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static PowerStateManagement.PowerInformationInterop;

namespace PowerStateManagement
{
    [ComVisible(true)]
    [Guid("266F197F-92F3-4D08-B8EC-AE8641297EEF")]
    [ClassInterface(ClassInterfaceType.None)]
    public class PowerInformationInteropPublic : IPowerInformationInterop
    {
        public string GetLastSleepTime()
        {
            IntPtr lastSleep = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ulong)));
            var result = PowerInformationInterop.CallNtPowerInformation(PowerInformationLevel.LastSleepTime, IntPtr.Zero, 0, lastSleep, (uint)Marshal.SizeOf(typeof(ulong)));
            if (result == STATUS_SUCCESS)
            {
                long lastSleepTimeInSecond = Marshal.ReadInt64(lastSleep, 0) / 10000000;
                TimeSpan t = TimeSpan.FromSeconds(lastSleepTimeInSecond);
                string answer = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                                        t.Hours,
                                        t.Minutes,
                                        t.Seconds,
                                        t.Milliseconds);
                return $"Last sleep time = {answer}";
            }

            return "error";
        }

        public string GetLastWakeTime()
        {
            var lastWake = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ulong)));
            var result = PowerInformationInterop.CallNtPowerInformation(PowerInformationLevel.LastWakeTime, IntPtr.Zero, 0, lastWake, (uint)Marshal.SizeOf(typeof(ulong)));
            if (result == STATUS_SUCCESS)
            {
                var lastWakeTimeInSeconds = Marshal.ReadInt64(lastWake, 0) / 10000000;
                TimeSpan t = TimeSpan.FromSeconds(lastWakeTimeInSeconds);
                string answer = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                                        t.Hours,
                                        t.Minutes,
                                        t.Seconds,
                                        t.Milliseconds);
                return $"Last wake time = {answer}";
            }

            return "error";
        }

        public string GetSystemBatteryState()
        {
            SystemBatteryState batteryState;
            var result = PowerInformationInterop.CallNtPowerInformation(PowerInformationLevel.SystemBatteryState, IntPtr.Zero, 0, out batteryState, (uint)Marshal.SizeOf(typeof(SystemBatteryState)));
            if (result == STATUS_SUCCESS)
            {
                return @$"System battery state: 
                {nameof(batteryState.AcOnLine)} = {batteryState.AcOnLine}
                {nameof(batteryState.BatteryPresent)} = {batteryState.BatteryPresent}
                {nameof(batteryState.Charging)} = {batteryState.Charging}
                {nameof(batteryState.DefaultAlert1)} = {batteryState.DefaultAlert1}
                {nameof(batteryState.DefaultAlert2)} = {batteryState.DefaultAlert2}
                {nameof(batteryState.Discharging)} = {batteryState.Discharging}
                {nameof(batteryState.EstimatedTime)} = {batteryState.EstimatedTime}
                {nameof(batteryState.MaxCapacity)} = {batteryState.MaxCapacity}
                {nameof(batteryState.Rate)} = {batteryState.Rate}
                {nameof(batteryState.RemainingCapacity)} = {batteryState.RemainingCapacity}
                {nameof(batteryState.Spare1)} = {batteryState.Spare1}
                {nameof(batteryState.Spare2)} = {batteryState.Spare2}
                {nameof(batteryState.Spare3)} = {batteryState.Spare3}
                {nameof(batteryState.Spare4)} = {batteryState.Spare4}
            ";
            }

            return "error";
        }

        public string GetSystemPowerInformation()
        {
            SYSTEM_POWER_INFORMATION powerInformation;
            var result = PowerInformationInterop.CallNtPowerInformation(PowerInformationLevel.SystemPowerInformation, IntPtr.Zero, 0, out powerInformation, (uint)Marshal.SizeOf(typeof(SystemBatteryState)));
            if (result == STATUS_SUCCESS)
            {
                return @$"System power information: 
                {nameof(powerInformation.CoolingMode)} = {powerInformation.CoolingMode}
                {nameof(powerInformation.Idleness)} = {powerInformation.Idleness}
                {nameof(powerInformation.MaxIdlenessAllowed)} = {powerInformation.MaxIdlenessAllowed}
                {nameof(powerInformation.TimeRemaining)} = {powerInformation.TimeRemaining}";
            }

            return "error";
        }

        public string SetSuspendState(bool hibernation)
        {
            var result = PowerInformationInterop.SetSuspendState(hibernation, false, false);
            if (result)
            {
                return $"SetSuspendState is working";
            }
            else
            {
                return $"SetSuspendState isn't working";
            }
        }

        public string SystemReserveHiberFile()
        {
            bool powerInformation;
            var result = PowerInformationInterop.CallNtPowerInformation(PowerInformationLevel.SystemReserveHiberFile, out powerInformation, (uint)Marshal.SizeOf(Marshal.SizeOf(typeof(bool))), IntPtr.Zero, (uint)Marshal.SizeOf(typeof(bool)));
            if (result == STATUS_SUCCESS)
            {
                if (powerInformation)
                {
                    return $"Hibernation file is reserved = {powerInformation}");
                }
                else
                {
                    return $"Hibernation file is removed = {powerInformation}");
                }

            }

            return "error";
        }
    }
}
