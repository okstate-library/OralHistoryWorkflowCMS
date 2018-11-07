namespace Core
{
    /// <summary>
    /// Defies the globally using constants within this class.
    /// </summary>
    public static class GlobalConstant
    {
        /// <summary>
        /// Gets or sets the name of the user profile image.
        /// </summary>
        /// <value>
        /// The name of the user profile image.
        /// </value>
        public static string UserProfileImageName
        {
            get
            {
                return "myimage.png";
            }
        }

        /// <summary>
        /// Gets the name of the organization logo.
        /// </summary>
        /// <value>
        /// The name of the organization logo.
        /// </value>
        public static string OrganizationLogoName
        {
            get
            {
                return "logo.png";
            }
        }

        public static string CertificateFileName
        {
            get
            {
                return "certificate.png";
            }
        }

    }
}