using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HyperMock.Universal.Behaviours;
using HyperMock.Universal.Exceptions;

namespace HyperMock.Universal
{
    /// <summary>
    /// Set of extensions on the proxy for setting up method and property behaviours.
    /// </summary>
    public static class MockExtensions
    {
        /// <summary>
        /// Setup of a method with no return.
        /// </summary>
        /// <typeparam name="TMock">Mocked type</typeparam>
        /// <param name="instance">Mocked instance</param>
        /// <param name="expression">Method expression</param>
        /// <returns>Method behaviour</returns>
        public static VoidBehaviour Setup<TMock>(
            this TMock instance, Expression<Action<TMock>> expression)
        {
            var dispatcher = GetDispatcher(instance);

            var callInfo = dispatcher.CreateForMethod(expression);

            return new VoidBehaviour(callInfo);
        }

        /// <summary>
        /// Setup of a function with a return value.
        /// </summary>
        /// <typeparam name="TMock">Mocked type</typeparam>
        /// <typeparam name="TReturn">Return type</typeparam>
        /// <param name="instance">Mocked instance</param>
        /// <param name="expression">Function expression</param>
        /// <returns>Function behaviours</returns>
        public static ReturnBehaviour<TMock, TReturn> Setup<TMock, TReturn>(
            this TMock instance, Expression<Func<TMock, TReturn>> expression)
        {
            var dispatcher = GetDispatcher(instance);

            var callInfo = dispatcher.CreateForFunction(expression);

            return new ReturnBehaviour<TMock, TReturn>(callInfo);
        }

        /// <summary>
        /// Setup of a function with a return of type <see cref="System.Threading.Tasks.Task"/>.
        /// </summary>
        /// <typeparam name="TMock">Mocked type</typeparam>
        /// <param name="instance">Mocked instance</param>
        /// <param name="expression">Method expression</param>
        /// <returns>Function behaviours</returns>
        public static AsyncVoidReturnBehaviour<TMock> Setup<TMock>(
            this TMock instance, Expression<Func<TMock, Task>> expression)
        {
            var dispatcher = GetDispatcher(instance);

            var callInfo = dispatcher.CreateForFunction(expression);

            return new AsyncVoidReturnBehaviour<TMock>(callInfo);
        }

        /// <summary>
        /// Setup of a function with a return of type <see cref="System.Threading.Tasks.Task{TResult}"/>.
        /// </summary>
        /// <typeparam name="TMock">Mocked type</typeparam>
        /// <typeparam name="TReturn">Return type</typeparam>
        /// <param name="instance">Mocked instance</param>
        /// <param name="expression">Function expression</param>
        /// <returns>Function behaviours</returns>
        public static AsyncReturnBehaviour<TMock, TReturn> Setup<TMock, TReturn>(
            this TMock instance, Expression<Func<TMock, Task<TReturn>>> expression)
        {
            var dispatcher = GetDispatcher(instance);

            var callInfo = dispatcher.CreateForFunction(expression);

            return new AsyncReturnBehaviour<TMock, TReturn>(callInfo);
        }

        /// <summary>
        /// Setup of a property read.
        /// </summary>
        /// <typeparam name="TMock">Mocked type</typeparam>
        /// <typeparam name="TReturn">Return type</typeparam>
        /// <param name="instance">Mocked instance</param>
        /// <param name="expression">Read property expression</param>
        /// <returns>Read property behaviours</returns>
        public static ReturnBehaviour<TMock, TReturn> SetupGet<TMock, TReturn>(
            this TMock instance, Expression<Func<TMock, TReturn>> expression)
        {
            var dispatcher = GetDispatcher(instance);

            var callInfo = dispatcher.CreateForReadProperty(expression);

            return new ReturnBehaviour<TMock, TReturn>(callInfo);
        }

        /// <summary>
        /// Setup of a property read with a return of type <see cref="System.Threading.Tasks.Task"/>.
        /// </summary>
        /// <typeparam name="TMock">Mocked type</typeparam>
        /// <typeparam name="TReturn">Return type</typeparam>
        /// <param name="instance">Mocked instance</param>
        /// <param name="expression">Read property expression</param>
        /// <returns>Read property behaviours</returns>
        public static AsyncVoidReturnBehaviour<TMock> SetupGet<TMock, TReturn>(
            this TMock instance, Expression<Func<TMock, Task>> expression)
        {
            var dispatcher = GetDispatcher(instance);

            var callInfo = dispatcher.CreateForReadProperty(expression);

            return new AsyncVoidReturnBehaviour<TMock>(callInfo);
        }

        /// <summary>
        /// Setup of a property read with a return of type <see cref="System.Threading.Tasks.Task{TResult}"/>.
        /// </summary>
        /// <typeparam name="TMock">Mocked type</typeparam>
        /// <typeparam name="TReturn">Return type</typeparam>
        /// <param name="instance">Mocked instance</param>
        /// <param name="expression">Read property expression</param>
        /// <returns>Read property behaviours</returns>
        public static AsyncReturnBehaviour<TMock, TReturn> SetupGet<TMock, TReturn>(
            this TMock instance, Expression<Func<TMock, Task<TReturn>>> expression)
        {
            var dispatcher = GetDispatcher(instance);

            var callInfo = dispatcher.CreateForReadProperty(expression);

            return new AsyncReturnBehaviour<TMock, TReturn>(callInfo);
        }

        /// <summary>
        /// Setup of a property write.
        /// </summary>
        /// <typeparam name="TMock">Mocked type</typeparam>
        /// <typeparam name="TReturn">Return type</typeparam>
        /// <param name="instance">Mocked instance</param>
        /// <param name="expression">Write property expression</param>
        /// <returns>Write property behaviours</returns>
        public static VoidBehaviour SetupSet<TMock, TReturn>(
            this TMock instance, Expression<Func<TMock, TReturn>> expression)
        {
            var dispatcher = GetDispatcher(instance);

            var callInfo = dispatcher.CreateForWriteProperty(expression);

            return new VoidBehaviour(callInfo);
        }

        public static void Raise<TMock, TArgs>(this TMock instance, Action<TMock> expression, TArgs args)
            where TArgs : EventArgs
        {
            // Invoke it with the null handler to set the last method called to extract the event handler name
            expression(instance);

            var dispatcher = GetDispatcher(instance);
            dispatcher.RaiseEvent(instance, dispatcher.LastMethod, args);
        }

        private static MockProxyDispatcher GetDispatcher<TMock>(TMock instance)
        {
            var dispatcher = instance as MockProxyDispatcher;

            if (dispatcher == null)
                throw new MockException("Unable to get the dispatcher from the instance.");

            return dispatcher;
        }
    }
}