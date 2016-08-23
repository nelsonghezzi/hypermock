namespace HyperMock.Universal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AsyncVoidBehaviour : VoidBehaviour
    {
        private readonly CallInfo _callInfo;

        internal AsyncVoidBehaviour(CallInfo callInfo)
            : base(callInfo)
        {
            _callInfo = callInfo;
        }

        public void Completes()
        {
            _callInfo.ReturnValue = Task.CompletedTask;
        }
    }
}
