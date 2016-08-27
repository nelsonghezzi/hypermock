using System;

namespace Tests.HyperMock.Universal.Support
{
    public class ThemostatController
    {
        private readonly IThermostatService _thermostatService;

        public ThemostatController(IThermostatService thermostatService)
        {
            _thermostatService = thermostatService;
            _thermostatService.Hot += OnSwitchOff;
            _thermostatService.Cold -= OnSwitchOn;
            _thermostatService.TempChanged += OnTempChanged;
        }

        private void OnSwitchOff(object sender, EventArgs args)
        {
            _thermostatService.SwitchOff();
        }

        private void OnSwitchOn(object sender, EventArgs args)
        {
            _thermostatService.SwitchOn();
        }

        private void OnTempChanged(object sender, TempChangedEventArgs args)
        {
            _thermostatService.ChangeTemp(args.Value);
        }
    }
}