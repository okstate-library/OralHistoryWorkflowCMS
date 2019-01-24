#region Imports

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using BusinessServices.UnitofWork;

#endregion

namespace BusinessServices
{

    /// <summary>
    /// Contains operations.
    /// </summary>
    internal class OperationsCollection : System.Collections.ObjectModel.Collection<Operation>
    {

        #region Classes

        /// <summary>
        /// Contains formats for output.
        /// </summary>
        private static class OutputFormats
        {

            #region Constants

            /// <summary>
            /// The starting message.
            /// </summary>
            public const string Start = @"[UoW] log starting...";

            /// <summary>
            /// The message for items without a duration.
            /// </summary>
            public const string ItemWithoutDuration = @"[UoW] '{0}' @ {1}...";

            /// <summary>
            /// The message for items with a duration.
            /// </summary>
            public const string ItemWithDuration = @"[UoW] '{0}' @ {1}; duration: {2}ms...";

            /// <summary>
            /// The ending message.
            /// </summary>
            public const string End = @"[UoW] log complete!";

            #endregion

        }

        #endregion

        #region Methods - Instance Member

        #region Methods - Instance Member - Object Members

        /// <summary>
        /// Returns a <see cref="String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder textBuffer = new StringBuilder();

            int index = 0;

            textBuffer.AppendLine(OperationsCollection.OutputFormats.Start);

            foreach (Operation currentOperation in Items)
            {
                if (index == 0)
                {
                    textBuffer.AppendFormat(
                        CultureInfo.InvariantCulture,
                        OperationsCollection.OutputFormats.ItemWithoutDuration,
                        currentOperation.Message,
                        currentOperation.Timestamp);
                }
                else
                {
                    Operation previousOperation = Items[index - 1];
                    TimeSpan duration = currentOperation.Timestamp - previousOperation.Timestamp;

                    textBuffer.AppendFormat(
                        CultureInfo.InvariantCulture,
                        OperationsCollection.OutputFormats.ItemWithDuration,
                        currentOperation.Message,
                        currentOperation.Timestamp,
                        duration.TotalMilliseconds);
                }

                textBuffer.AppendLine();

                index++;
            }

            textBuffer.AppendLine(OperationsCollection.OutputFormats.End);
            textBuffer.AppendLine();

            return textBuffer.ToString();
        }

        #endregion

        #endregion

    }

}
