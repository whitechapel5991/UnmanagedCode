using System;
using System.Runtime.InteropServices;

namespace PowerStateManagement
{
    public class PowerInformationInterop
    {
        public const uint STATUS_SUCCESS = 0;

        public enum PowerInformationLevel
        {
            AdministratorPowerPolicy = 9,
            LastSleepTime = 15,
            LastWakeTime = 14,
            ProcessorInformation = 11,
            ProcessorPowerPolicyAc = 18,
            ProcessorPowerPolicyCurrent = 22,
            ProcessorPowerPolicyDc = 19,
            SystemBatteryState = 5,
            SystemExecutionState = 16,
            SystemPowerCapabilities = 4,
            SystemPowerInformation = 12,
            SystemPowerPolicyAc = 0,
            SystemPowerPolicyCurrent = 8,
            SystemPowerPolicyDc = 1,
            SystemReserveHiberFile = 10,
            VerifyProcessorPowerPolicyAc = 20,
            VerifyProcessorPowerPolicyDc = 21,
            VerifySystemPolicyAc = 2,
            VerifySystemPolicyDc = 3
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SystemBatteryState
        {
            [MarshalAs(UnmanagedType.I1)]
            public bool AcOnLine;
            [MarshalAs(UnmanagedType.I1)]
            public bool BatteryPresent;
            [MarshalAs(UnmanagedType.I1)]
            public bool Charging;
            [MarshalAs(UnmanagedType.I1)]
            public bool Discharging;
            public byte Spare1;
            public byte Spare2;
            public byte Spare3;
            public byte Spare4;
            public uint MaxCapacity;
            public uint RemainingCapacity;
            public uint Rate;
            public uint EstimatedTime;
            public uint DefaultAlert1;
            public uint DefaultAlert2;
        }

        public struct SYSTEM_POWER_INFORMATION
        {
            public uint MaxIdlenessAllowed;
            public uint Idleness;
            public uint TimeRemaining;
            public byte CoolingMode;
        }

        [DllImport("powrprof.dll", EntryPoint = "CallNtPowerInformation", SetLastError = true)]
        internal static extern UInt32 CallNtPowerInformation(
             PowerInformationLevel informationLevel,
             IntPtr InputBuffer, UInt32 InputBufferLength, IntPtr OutputBuffer, UInt32 OutputBufferLength
        );

        [DllImport("powrprof.dll", EntryPoint = "CallNtPowerInformation", SetLastError = true)]
        internal static extern UInt32 CallNtPowerInformation(
             PowerInformationLevel informationLevel,
             IntPtr inputBuffer,
             UInt32 inputBufferSize,
             out SystemBatteryState outputBuffer,
             UInt32 outputBufferSize
        );

        [DllImport("powrprof.dll", EntryPoint = "CallNtPowerInformation", SetLastError = true)]
        internal static extern UInt32 CallNtPowerInformation(
             PowerInformationLevel informationLevel,
             IntPtr inputBuffer,
             UInt32 inputBufferSize,
             out SYSTEM_POWER_INFORMATION outputBuffer,
             UInt32 outputBufferSize
        );

        [DllImport("powrprof.dll", EntryPoint = "CallNtPowerInformation", SetLastError = true)]
        internal static extern UInt32 CallNtPowerInformation(
             PowerInformationLevel informationLevel,
             out bool inputBuffer,
             UInt32 inputBufferSize,
             IntPtr outputBuffer,
             UInt32 outputBufferSize
        );

        [DllImport("powrprof.dll", SetLastError = true)]
        internal static extern bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);
    }
}
