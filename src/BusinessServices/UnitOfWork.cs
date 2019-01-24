#region imports

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Diagnostics.Logging;
using Core;
using Core.Diagnostics.ExceptionManagement;
using Core.Diagnostics;
using BusinessServices.UnitofWork;
using System.Transactions;
using Model;
#endregion

namespace BusinessServices
{

    /// <summary>
    /// 
    /// </summary>
    public abstract class UnitOfWork
    {
        #region Classes

        /// <summary>
        /// Contains exception policies for the Unit of Work.
        /// </summary>
        private static class ExceptionPolicy
        {
            #region Constants

            /// <summary>
            /// Exception policy for errors occurring during the actual work.
            /// </summary>
            public const string DoWork = @"UnitOfWork_Do";

            /// <summary>
            /// Exception policy for errors occurring during cleanup.
            /// </summary>
            public const string AttemptCleanUp = @"UnitOfWork_Cleanup";

            #endregion
        }

        /// <summary>
        /// Contains operations log messages.
        /// </summary>
        private static class OperationsLogMessages
        {
            #region Constants

            /// <summary>
            /// Logs the start of operations.
            /// </summary>
            public const string LogStart = @"[UoW] - START";

            /// <summary>
            /// Logs the end of operations.
            /// </summary>
            public const string LogEnd = @"[UoW] - END";

            /// <summary>
            /// Logs the start of the pre-execute phase.
            /// </summary>
            public const string LogStartPreExecute = @"[UoW] - Pre-Execute/Start";

            /// <summary>
            /// Logs the end of the pre-execute phase.
            /// </summary>
            public const string LogEndPreExecute = @"[UoW] - Pre-Execute/End";

            /// <summary>
            /// Logs the start of the actual Work.
            /// </summary>
            public const string LogStartWork = @"[UoW] - Work/Start";

            /// <summary>
            /// Logs the end of the actual Work.
            /// </summary>
            public const string LogEndWork = @"[UoW] - Work/End";

            /// <summary>
            /// Logs the start of the post-execute phase.
            /// </summary>
            public const string LogStartPostExecute = @"[UoW] - Post-Execute/Start";

            /// <summary>
            /// Logs the end of the post-execute phase.
            /// </summary>
            public const string LogEndPostExecute = @"[UoW] - Post-Execute/End";

            /// <summary>
            /// Logs the start of error handling.
            /// </summary>
            public const string LogStartErrorHandling = @"[UoW] - Error-Handling/Start";

            /// <summary>
            /// Logs the end of error handling.
            /// </summary>
            public const string LogEndErrorHandling = @"[UoW] - Error-Handling/End";

            /// <summary>
            /// Logs the start of cleanup operations.
            /// </summary>
            public const string LogStartCleanup = @"[UoW] - Cleanup/Start";

            /// <summary>
            /// Logs the end of cleanup operations.
            /// </summary>
            public const string LogEndCleanup = @"[UoW] - Cleanup/End";

            #endregion
        }

        #endregion

        #region Properties - Instance Member

        #region Properties - Instance Member - UnitOfWork Members

        /// <summary>
        /// Gets or sets a value indicating whether this instance is transaction required.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is transaction required; otherwise, <c>false</c>.
        /// </value>
        public bool IsTransactionRequired
        {
            get
            {
                return true;// ConfigurationData.IsDBTransactionNeeded;
            }

        }

        /// <summary>
        /// Gets a value indicating whether this Unit of Work is read only.
        /// </summary>
        /// <value>
        /// <c>true</c> if this Unit of Work is read only; 
        /// otherwise, <c>false</c>.
        /// </value>
        public bool IsReadOnly
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the operations log.
        /// </summary>
        /// <value>
        /// The operations log.
        /// </value>
        private OperationsCollection OperationsLog
        {
            get;
            set;
        }

        #endregion

        #endregion

        #region Methods - Instance Member

        #region Methods - Instance Member - UnitOfWork Members

        #region Methods - Instance Member - UnitOfWork Members - (constructors)

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="isReadOnly">
        /// <c>true</c> if this Unit of Work is read only; 
        /// otherwise, <c>false</c>.
        /// </param>
        protected UnitOfWork(bool isReadOnly)
        {
            IsReadOnly = isReadOnly;
            OperationsLog = new OperationsCollection();
        }

        #endregion

        #region Methods - Instance Member - UnitOfWork Members - (operations)

        /// <summary>
        /// Does the Work.
        /// </summary>
        /// <exception cref="BusinessException">An error occurs while doing the Work.</exception>
        public void DoWork()
        {
            LogStart();

            bool hasErrorOccurred = false;

            try
            {
                AttemptToDoWork();
            }
            catch (Exception ex)
            {
                LogStartErrorHandling();

                hasErrorOccurred = true;

                Exception handledException = ExceptionManager.HandleException(
                    GetType(),
                    ex,
                    ErrorSeverity.Error,
                    UnitOfWork.ExceptionPolicy.DoWork);

                if (handledException != null)
                {
                    LogEndErrorHandling();
                    throw new BusinessException(handledException.Message, handledException);
                }
                else
                {
                    LogEndErrorHandling();
                    throw new BusinessException(ex.Message, ex);
                }

            }
            finally
            {
                LogStartCleanup();
                AttemptCleanUp(hasErrorOccurred);
                LogEndCleanup();

                LogEnd();

                WriteOperationsLog();
            }
        }

        /// <summary>
        /// Attempts to do the Work.
        /// </summary>
        private void AttemptToDoWork()
        {
            LogStartPreExecute();

            PreExecute();

            LogEndPreExecute();

            if (IsReadOnly)
            {
                if (IsTransactionRequired)
                {
                    ExecuteTransaction();
                }
                else
                {
                    ExecuteNonTransaction();
                }
            }
            else
            {
                ExecuteTransaction();
            }

            LogStartPostExecute();
            PostExecute();
            LogEndPostExecute();
        }

        /// <summary>
        /// Executes the Unit of Work in a transactional manner.
        /// </summary>
        private void ExecuteTransaction()
        {
            LogStartWork();

            using (TransactionScope transactionScope = new TransactionScope())
            {
                Execute();

                transactionScope.Complete();
            }

            LogEndWork();
        }

        /// <summary>
        /// Executes the Unit of Work in a non-transactional manner.
        /// </summary>
        private void ExecuteNonTransaction()
        {
            LogStartWork();

            Execute();

            LogEndWork();
        }

        /// <summary>
        /// The actual Work to be done.
        /// </summary>
        protected abstract void Execute();

        /// <summary>
        /// Runs prior to the work being done.
        /// </summary>
        protected virtual void PreExecute()
        {
        }

        /// <summary>
        /// Runs after the work has been done.
        /// </summary>
        protected virtual void PostExecute()
        {
        }

        /// <summary>
        /// Attempts to clean up the Unit of Work.
        /// </summary>
        /// <param name="hasErrorOccurred">
        /// <c>true</c> if an error has occurred;
        /// otherwise, <c>false</c>.
        /// </param>
        private void AttemptCleanUp(bool hasErrorOccurred)
        {
            try
            {
                CleanUp(hasErrorOccurred);
            }
            catch (Exception ex)
            {
                ExceptionManager.HandleException(
                    GetType(),
                    ex,
                    ErrorSeverity.Warning,
                    UnitOfWork.ExceptionPolicy.AttemptCleanUp);

                // sink exception
            }
        }

        /// <summary>
        /// Cleans up the Unit of Work.
        /// </summary>
        /// <param name="hasErrorOccurred">
        /// <c>true</c> if an error has occurred;
        /// otherwise, <c>false</c>.
        /// </param>
        /// <remarks>
        /// Errors in this method will not be allowed to trickle up the call stack,
        /// to allow the original exception data to be made available.
        /// </remarks>
        protected virtual void CleanUp(bool hasErrorOccurred)
        {
        }

        #endregion

        #region Methods - Instance Member - UnitOfWork Members - (diagnostics)

        /// <summary>
        /// Logs the given operations message.
        /// </summary>
        /// <param name="message">
        /// The message describing the operation.
        /// </param>
        protected void LogOperation(string message)
        {
            if (ConfigurationData.MustLogOperationalPerformance)
            {
                Operation operation = new Operation();

                operation.Timestamp = DateTimeHelper.Now;
                operation.Message = message;

                OperationsLog.Add(operation);

            }
        }

        /// <summary>
        /// Writes the operations log.
        /// </summary>
        protected void WriteOperationsLog()
        {
            if (ConfigurationData.MustLogOperationalPerformance)
            {
                string logText = OperationsLog.ToString();

                LogManager.Log(
                    GetType(),
                    ErrorSeverity.Information,
                    logText);
            }
        }

        /// <summary>
        /// Logs the start of operations.
        /// </summary>
        private void LogStart()
        {
            LogOperation(UnitOfWork.OperationsLogMessages.LogStart);
        }

        /// <summary>
        /// Logs the end of operations.
        /// </summary>
        private void LogEnd()
        {
            LogOperation(UnitOfWork.OperationsLogMessages.LogEnd);
        }

        /// <summary>
        /// Logs the start of the pre-execute phase.
        /// </summary>
        private void LogStartPreExecute()
        {
            LogOperation(UnitOfWork.OperationsLogMessages.LogStartPreExecute);
        }

        /// <summary>
        /// Logs the end of the pre-execute phase.
        /// </summary>
        private void LogEndPreExecute()
        {
            LogOperation(UnitOfWork.OperationsLogMessages.LogEndPreExecute);
        }

        /// <summary>
        /// Logs the start of the actual Work.
        /// </summary>
        private void LogStartWork()
        {
            LogOperation(UnitOfWork.OperationsLogMessages.LogStartWork);
        }

        /// <summary>
        /// Logs the end of the actual Work.
        /// </summary>
        private void LogEndWork()
        {
            LogOperation(UnitOfWork.OperationsLogMessages.LogEndWork);
        }

        /// <summary>
        /// Logs the start of the post-execute phase.
        /// </summary>
        private void LogStartPostExecute()
        {
            LogOperation(UnitOfWork.OperationsLogMessages.LogStartPostExecute);
        }

        /// <summary>
        /// Logs the end of the post-execute phase.
        /// </summary>
        private void LogEndPostExecute()
        {
            LogOperation(UnitOfWork.OperationsLogMessages.LogEndPostExecute);
        }

        /// <summary>
        /// Logs the start of error handling.
        /// </summary>
        private void LogStartErrorHandling()
        {
            LogOperation(UnitOfWork.OperationsLogMessages.LogStartErrorHandling);
        }

        /// <summary>
        /// Logs the end of error handling.
        /// </summary>
        private void LogEndErrorHandling()
        {
            LogOperation(UnitOfWork.OperationsLogMessages.LogEndErrorHandling);
        }

        /// <summary>
        /// Logs the start of cleanup operations.
        /// </summary>
        private void LogStartCleanup()
        {
            LogOperation(UnitOfWork.OperationsLogMessages.LogStartCleanup);
        }

        /// <summary>
        /// Logs the end of cleanup operations.
        /// </summary>
        private void LogEndCleanup()
        {
            LogOperation(UnitOfWork.OperationsLogMessages.LogEndCleanup);
        }

        #endregion

        #endregion

        #endregion

    }
}
