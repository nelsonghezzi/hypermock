namespace HyperMock.Universal
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides return type behaviours to be added.
    /// </summary>
    /// <typeparam name="TMock">Mocked type</typeparam>
    /// <typeparam name="TResult">Return type</typeparam>
    public class AsyncReturnBehaviour<TMock, TResult> : ReturnBehaviour<TMock, Task<TResult>>
    {
        private readonly CallInfo _callInfo;

        internal AsyncReturnBehaviour(CallInfo callInfo)
            : base(callInfo)
        {
            _callInfo = callInfo;
        }

        /// <summary>
        /// The mocked type method or property returns a completed
        /// <see cref="System.Threading.Tasks.Task{TResult}"/> with this value.
        /// </summary>
        /// <param name="taskResult">Value for the result of the <see cref="System.Threading.Tasks.Task{TResult}"/></param>
        public void CompletesWith(TResult taskResult)
        {
            _callInfo.ReturnValue = Task.FromResult(taskResult);
        }

        /// <summary>
        /// The mocked type method or property returns a
        /// <see cref="System.Threading.Tasks.Task{TResult}"/> in the
        /// <seealso cref="System.Threading.Tasks.TaskStatus.Canceled"/> state.
        /// </summary>
        public void Cancels()
        {
            _callInfo.ReturnValue = Task.FromCanceled<TResult>(new CancellationToken(true));
        }

        /// <summary>
        /// The mocked type method or property returns a
        /// <see cref="System.Threading.Tasks.Task{TResult}"/> in the
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
        /// <see cref="System.Threading.Tasks.Task{TResult}"/> in the
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

            _callInfo.ReturnValue = Task.FromException<TResult>(exception);
        }
    }
}
