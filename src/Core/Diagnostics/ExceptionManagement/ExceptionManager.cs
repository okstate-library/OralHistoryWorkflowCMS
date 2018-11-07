
#region Imports

using System;
using System.Globalization;
using Core.Diagnostics.Logging;


#endregion Imports

namespace Core.Diagnostics.ExceptionManagement
{
    /// <summary>
    /// Manages exceptions and the handling thereof.
    /// </summary>
    public static class ExceptionManager
    {
        #region Constants

        /// <summary>
        /// The default exception policy name.
        /// </summary>
        private const string DefaultPolicyName = @"DefaultExceptionPolicy";

        #endregion Constants

        #region Methods - Static Member

        #region Methods - Static Member - (exception management)

        /// <summary>
        /// Handles the given exception originating from the given type and of the
        /// specified severity, according to the named exception management policy given,
        /// returning the exception to be thrown, if relevant.
        /// </summary>
        /// <param name="type">
        /// The type from which the exception originated.
        /// </param>
        /// <param name="exception">
        /// The exception to be managed.
        /// </param>
        /// <param name="severity">
        /// The severity of the exception.
        /// </param>
        /// <param name="policy">
        /// The policy to be used in managing the exception.
        /// </param>
        /// <returns>
        /// The exception to be thrown by the caller (as specified by <paramref name="policy"/>);
        /// otherwise, <c>null</c>.
        /// </returns>
        public static Exception HandleException(Type type,
            Exception exception, ErrorSeverity severity, string policy)
        {
            Exception returnedException = null;

            // check whether the provided exception is a valid reference
            if (exception != null)
            {
                try
                {
                    // validate the provided exception policy
                    string validatedPolicy = string.IsNullOrEmpty(policy) ?
                        ExceptionManager.DefaultPolicyName : policy;

                    // prepare exception message
                    string message = string.Format(CultureInfo.CurrentCulture,
                        Resource.ExceptionMessageText,
                        validatedPolicy, exception.Message);

                    // log exception data
                    LogManager.Log(type, severity, message, exception);

                    // set the configured exception for return
                    returnedException = ExceptionManager.GetException(exception, validatedPolicy);
                }
                catch (Exception ex)
                {
                    // log exception data
                    LogManager.Log(typeof(ExceptionManager), ErrorSeverity.Error, ex.Message, ex);

                    // set the provided exception for return
                    returnedException = exception;

                    // sink exception; this block is for absolute assurance that any further
                    // errors during exception handling will not be propagated up the call stack
                }
            }

            return returnedException;
        }

        /// <summary>
        /// Handles the given exception originating from the given type and of the
        /// specified severity, according to the named exception management policy given,
        /// throwing an exception in turn, as dictated by the policy.
        /// </summary>
        /// <param name="type">
        /// The type from which the exception originated.
        /// </param>
        /// <param name="exception">
        /// The exception to be managed.
        /// </param>
        /// <param name="severity">
        /// The severity of the exception.
        /// </param>
        /// <param name="policy">
        /// The policy to be used in managing the exception.
        /// </param>
        /// <exception cref="Exception">
        /// The exception to be thrown (as dictated by <paramref name="policy"/>).
        /// </exception>
        public static void HandleExceptionWithThrow(Type type,
            Exception exception, ErrorSeverity severity, string policy)
        {
            // handle the exception
            Exception returnedException = ExceptionManager.HandleException(type,
                exception, severity, policy);

            // identify if the returned exception is a valid reference
            if (returnedException != null)
            {
                // throw the exception
                throw returnedException;
            }
        }

        #endregion Methods - Static Member - (exception management)

        #region Methods - Static Member - (helpers)

        /// <summary>
        /// Gets the exception to be utilized in handling a caught exception,
        /// based on the specified policy.
        /// </summary>
        /// <param name="exception">
        /// The caught exception, requiring handling.
        /// </param>
        /// <param name="policy">
        /// The policy guiding the approach to be taken in handling the exception.
        /// </param>
        /// <returns>
        /// The <see cref="Exception"/> to be utilized in handling a caught exception.
        /// </returns>
        private static Exception GetException(Exception exception, string policy)
        {
            ExceptionHandlingApproach approach = ExceptionHandlingApproach.Rethrow;
            Type type = null;

            // TODO: implement configuration-based policy management
            // HACK: setup dummy type, in case approach is set to Swap; remove when configuration is implemented
            type = exception.GetType();

            // apply exception management policy
            return ExceptionManager.ApplyExceptionManagementPolicySettings(
                exception, approach, type);
        }

        /// <summary>
        /// Applies the exception management policy specified by the Exception Handling
        /// Approach and the related Exception type.
        /// </summary>
        /// <param name="exception">
        /// The exception requiring management.
        /// </param>
        /// <param name="approach">
        /// The approach to be taken in handling the exception.
        /// </param>
        /// <param name="exceptionType">
        /// The <see cref="Type"/> of the Exception defined in the policy related
        /// to <paramref name="approach"/>.
        /// </param>
        /// <returns>
        /// The <see cref="Exception"/> to be utilized in handling a caught exception.
        /// </returns>
        private static Exception ApplyExceptionManagementPolicySettings(
            Exception exception, ExceptionHandlingApproach approach, Type exceptionType)
        {
            Exception returnException = null;

            #region // apply policy

            // check and apply the configured approach
            switch (approach)
            {
                case ExceptionHandlingApproach.Sink:
                    // return a null reference, forcing the exception to be sunk
                    returnException = null;

                    break;

                case ExceptionHandlingApproach.Wrap:
                    try
                    {
                        // create container exception, with caught exception wrapped
                        returnException = (Exception)ObjectFactory.CreateObject(exceptionType,
                            exception.Message, exception);
                    }
                    catch
                    {
                        // return the same exception
                        returnException = exception;

                        // sink exception
                    }

                    break;

                case ExceptionHandlingApproach.Swap:
                    try
                    {
                        // create container exception, swapping out original exception
                        // however, the message from the original exception is retained
                        returnException = (Exception)ObjectFactory.CreateObject(exceptionType,
                            exception.Message);
                    }
                    catch
                    {
                        // return the same exception
                        returnException = exception;

                        // sink exception
                    }

                    break;

                case ExceptionHandlingApproach.Rethrow:
                default:
                    // return the same exception
                    returnException = exception;

                    break;
            }

            #endregion // apply policy

            return returnException;
        }

        #endregion Methods - Static Member - (helpers)

        #endregion Methods - Static Member
    }
}