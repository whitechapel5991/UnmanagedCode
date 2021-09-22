using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PowerStateManagement
{
    [ComVisible(true)]
    [Guid("606761A1-506D-4FEB-B6EC-D7941959EAF7")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IPowerInformationInterop
    {
        string GetLastSleepTime();
        string GetLastWakeTime();
        string GetSystemBatteryState();
        string GetSystemPowerInformation();
        string SystemReserveHiberFile();
        string SetSuspendState(bool hibernation);

    }
}
