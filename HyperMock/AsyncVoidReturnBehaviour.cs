namespace HyperMock.Universal
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class AsyncVoidReturnBehaviour<TMock> : ReturnBehaviour<TMock, Task>
    {
        private readonly CallInfo _callInfo;

        internal AsyncVoidReturnBehaviour(CallInfo callInfo)
            : base(callInfo)
        {
            _callInfo = callInfo;
        }

        /// <summary>
        /// The mocked type method or property returns a completed <see cref="System.Threading.Tasks.Task"/>.
        /// </summary>
        public void Completes()
        {
            _callInfo.ReturnValue = Task.CompletedTask;
        }

        /// <summary>
        /// The mocked type method or property returns a
        /// <see cref="System.Threading.Tasks.Task{TResult}"/> in the
        /// <seealso cref="System.Threading.Tasks.TaskStatus.Canceled"/> state.
        /// </summary>
        public void Cancels()
        {
            _callInfo.ReturnValue = Task.FromCanceled(new CancellationToken(true));
        }

        /// <summary>
        /// The mocked type method or property returns a
        /// <see cref="System.Threading.Tasks.Task"/> in the
        /// <seealso cref="System.Threading.Tasks.TaskStatus.Faulted"/> state
        /// with an exception of type <typeparamref name="TException"/>.
        /// </summary>
        /// <typeparam name="TException">Exception type</param>
        public void FailsWith<TException>()
            where TException : Exception, new()
        {
            this.FailsWith(new TException());
        }

        /// <summary>
        /// The mocked type method or property returns a
        /// <see cref="System.Threading.Tasks.Task"/> in the
        /// <seealso cref="System.Threading.Tasks.TaskStatus.Faulted"/> state
        /// with the provided <paramref name="exception"/>.
        /// </summary>
        /// <param name="exception">Exception instance for the faulted task</param>
        public void FailsWith(Exception exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            _callInfo.ReturnValue = Task.FromException(exception);
        }
    }
}
