using System;
using HyperMock.Universal;
using HyperMock.Universal.Verification;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Tests.HyperMock.Universal.Support;

namespace Tests.HyperMock.Universal
{
    [TestClass]
    public class EventTest
    {
        [TestMethod]
        public void RaisedEventInvokesAttachedSubjectHandler()
        {
            var proxy = Mock.Create<IThermostatService>();
            proxy.Setup(p => p.SwitchOff());
            // ReSharper disable once UnusedVariable
            var controller = new ThemostatController(proxy);

            proxy.Raise(p => p.Hot += null, new EventArgs());
            
            proxy.Verify(p => p.SwitchOff(), Occurred.Once());
        }

        [TestMethod]
        public void RaisedEventDoesNotInvokeDettachedSubjectHandler()
        {
            var proxy = Mock.Create<IThermostatService>();
            proxy.Setup(p => p.SwitchOn());
            // ReSharper disable once UnusedVariable
            var controller = new ThemostatController(proxy);

            proxy.Raise(p => p.Cold += null, new EventArgs());

            proxy.Verify(p => p.SwitchOn(), Occurred.Never());
        }

        [TestMethod]
        public void RaisedEventPassesParameters()
        {
            const int temp = 100;
            var proxy = Mock.Create<IThermostatService>();
            proxy.Setup(p => p.ChangeTemp(temp));
            // ReSharper disable once UnusedVariable
            var controller = new ThemostatController(proxy);

            proxy.Raise(p => p.TempChanged += null, new TempChangedEventArgs(temp));

            proxy.Verify(p => p.ChangeTemp(temp), Occurred.Once());
        }
    }
}