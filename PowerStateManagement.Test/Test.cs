using System;
using System.Runtime.InteropServices;
using Xunit;
using Xunit.Abstractions;
using static PowerStateManagement.PowerInformationInterop;

namespace PowerStateManagement
{
    public class Test
    {
        private readonly ITestOutputHelper console;
        private readonly IPowerInformationInterop powerInformationInterop;

        public Test(ITestOutputHelper output)
        {
            console = output;
            powerInformationInterop = new PowerInformationInteropPublic();
        }

        [Fact]
        public void TestLastSleepTime()
        {
            console.WriteLine(powerInformationInterop.GetLastSleepTime());
        }

        [Fact]
        public void TestLastWakeTime()
        {
            console.WriteLine(powerInformationInterop.GetLastWakeTime());
        }

        [Fact]
        public void TestSystemBatteryState()
        {
            console.WriteLine(powerInformationInterop.GetSystemBatteryState());
        }

        [Fact]
        public void TestSystemPowerInformation()
        {
            console.WriteLine(powerInformationInterop.GetSystemPowerInformation());
        }

        [Fact]
        public void TestSystemReserveHiberFile()
        {
            console.WriteLine(powerInformationInterop.SystemReserveHiberFile());
        }

        [Fact]
        public void TestHibernationSetSuspendState()
        {
            bool hibernate = true;
            console.WriteLine(powerInformationInterop.SetSuspendState(hibernate));
        }

        [Fact]
        public void TestSuspendSetSuspendState()
        {
            bool hibernate = false;
            console.WriteLine(powerInformationInterop.SetSuspendState(hibernate));
        }
    }
}
