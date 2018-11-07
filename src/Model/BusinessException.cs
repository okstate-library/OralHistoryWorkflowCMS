using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// The exception that is thrown when a business error occurs.
    /// </summary>
    [Serializable]
    public class BusinessException : Exception
    {
        #region Methods - Instance Member

        #region Methods - Instance Member - BusinessException Members

        #region Methods - Instance Member - BusinessException Members - (constructors)

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This constructor initializes the <see cref="Exception.Message"/> property
        /// of the new instance to a default message that describes the error, as defined in
        /// <see cref="Resources.BusinessDefaultExceptionMessage"/>.
        /// </para>
        /// <para>
        /// This message takes into account the current system culture.
        /// </para>
        /// <para>
        /// The following table shows the initial property values for an instance of
        /// <see cref="BusinessException"/>.
        /// </para>
        /// <list type="table">
        /// <listheader>
        /// <term>Property</term>
        /// <description>Value</description>
        /// </listheader>
        /// <item>
        /// <term><see cref="Exception.InnerException"/></term>
        /// <description>A null reference (<b>Nothing</b> in Visual Basic).</description>
        /// </item>
        /// <item>
        /// <term><see cref="Exception.Message"/></term>
        /// <description>The localized error message string.</description>
        /// </item>
        /// </list>
        /// </remarks>
        public BusinessException()
            : this("A business error has occurred.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="message">
        /// A message that describes the error.
        /// </param>
        /// <remarks>
        /// <para>
        /// The content of the <paramref name="message"/> parameter is intended
        /// to be understood by humans. The caller of this constructor is required
        /// to ensure that this string has been localized for the current system culture.
        /// </para>
        /// <para>
        /// This message takes into account the current system culture.
        /// </para>
        /// <para>
        /// The following table shows the initial property values for an instance of
        /// <see cref="BusinessException"/>.
        /// </para>
        /// <list type="table">
        /// <listheader>
        /// <term>Property</term>
        /// <description>Value</description>
        /// </listheader>
        /// <item>
        /// <term><see cref="Exception.InnerException"/></term>
        /// <description>A null reference (<b>Nothing</b> in Visual Basic).</description>
        /// </item>
        /// <item>
        /// <term><see cref="Exception.Message"/></term>
        /// <description>The error message string.</description>
        /// </item>
        /// </list>
        /// </remarks>
        public BusinessException(string message)
            : this(message, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="message">
        /// The error message that explains the reason for the exception.
        /// </param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception. If the
        /// <paramref name="innerException"/> parameter is not a null reference, the current
        /// exception is raised in a <b>catch</b> block that handles the inner exception.
        /// </param>
        /// <remarks>
        /// <para>
        /// The content of the <paramref name="message"/> parameter is intended
        /// to be understood by humans. The caller of this constructor is required
        /// to ensure that this string has been localized for the current system culture.
        /// </para>
        /// <para>
        /// An exception that is thrown as a direct result of a previous exception should
        /// include a reference to the previous exception in the
        /// <see cref="Exception.InnerException"/> property. The <b>InnerException</b>
        /// property returns the same value that is passed into the constructor, or a null
        /// reference if the <b>InnerException</b> property does not supply the inner exception
        /// value to the constructor.
        /// </para>
        /// <para>
        /// The following table shows the initial property values for an instance of
        /// <see cref="BusinessException"/>.
        /// </para>
        /// <list type="table">
        /// <listheader>
        /// <term>Property</term>
        /// <description>Value</description>
        /// </listheader>
        /// <item>
        /// <term><see cref="Exception.InnerException"/></term>
        /// <description>The inner exception reference.</description>
        /// </item>
        /// <item>
        /// <term><see cref="Exception.Message"/></term>
        /// <description>The error message string.</description>
        /// </item>
        /// </list>
        /// </remarks>
        public BusinessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data.
        /// </param>
        /// <param name="context">
        /// The contextual information about the source or destination.
        /// </param>
        /// <remarks>
        /// This constructor is called during deserialization to reconstitute
        /// the exception object transmitted over a stream.
        /// </remarks>
        protected BusinessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion Methods - Instance Member - BusinessException Members - (constructors)

        #endregion Methods - Instance Member - BusinessException Members

        #endregion Methods - Instance Member
    }
}
