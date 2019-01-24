using System.Security;

namespace Model
{
    /// <summary>
    /// Defiens the properties for the user model.
    /// </summary>
    public class UserTypeModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public short Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the user type.
        /// </summary>
        /// <value>
        /// The name of the user type.
        /// </value>
        public string UserTypeName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is horizontal menu.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is horizontal menu; otherwise, <c>false</c>.
        /// </value>
        public bool IsHorizontalMenu { get; set; }

    }
}
