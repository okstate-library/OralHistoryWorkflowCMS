#region Imports

using System;
using Core;

#endregion

namespace BusinessServices.UnitofWork
{

    /// <summary>
    /// Contains operation data.
    /// </summary>
    [Serializable]
    internal class Operation
    {

        #region Properties - Instance Member

        #region Properties - Instance Member - Operation Members

        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>
        public DateTime Timestamp
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message
        {
            get;
            set;
        }

        #endregion

        #endregion

        #region Methods - Instance Member

        #region Methods - Instance Member - Operation Members

        #region Methods - Instance Member - Operation Members - (constructors)

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="Operation"/> class.
        /// </summary>
        public Operation() : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="Operation"/> class, with the given message.
        /// </summary>
        /// <param name="message">
        /// The message describing the operation.
        /// </param>
        public Operation(string message) : this(DateTimeHelper.Now, message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="Operation"/> class, with the given message and timestamp.
        /// </summary>
        /// <param name="timestamp">
        /// The timestamp of the operation.
        /// </param>
        /// <param name="message">
        /// The message describing the operation.
        /// </param>
        public Operation(DateTime timestamp, string message)
        {
            this.Timestamp = timestamp;
            this.Message = string.IsNullOrEmpty(message) ? string.Empty : message;
        }

        #endregion

        #endregion

        #endregion

    }

}
