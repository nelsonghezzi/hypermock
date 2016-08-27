namespace HyperMock.Universal
{
    using System;

    /// <summary>
    /// Provides void method behaviours to be added.
    /// </summary>
    public class VoidBehaviour
    {
        private readonly CallInfo _callInfo;

        internal VoidBehaviour(CallInfo callInfo)
        {
            _callInfo = callInfo;
        }

        /// <summary>
        /// The mocked type method or parameter throws an excpetion.
        /// </summary>
        /// <typeparam name="TException">Exception type</typeparam>
        public void Throws<TException>()
            where TException : Exception, new()
        {
            this.Throws(new TException());
        }

        /// <summary>
        /// The mocked type method or parameter throws an exception.
        /// </summary>
        /// <param name="exception">Exception instance to throw</param>
        public void Throws(Exception exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            _callInfo.Exception = exception;
        }
    }
}