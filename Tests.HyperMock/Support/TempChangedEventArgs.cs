using System;

namespace Tests.HyperMock.Universal.Support
{
    public class TempChangedEventArgs : EventArgs
    {
        public TempChangedEventArgs(int temp)
        {
            Value = temp;
        }

        public int Value { get; }
    }
}