using System;

namespace Tests.HyperMock.Universal.Support
{
    public interface IThermostatService
    {
        event EventHandler Hot;
        event EventHandler Cold;
        event EventHandler<TempChangedEventArgs> TempChanged;

        void SwitchOn();
        void SwitchOff();
        void ChangeTemp(int temp);
    }
}