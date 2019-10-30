namespace Model
{
    /// <summary>
    /// Defines the properties of the collection model.
    /// </summary>
    public class CollectionModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the collection.
        /// </summary>
        /// <value>
        /// The name of the collection.
        /// </value>
        public string CollectionName { get; set; }

        public string RepositoryName { get; set; }

        public short RepositoryId { get; set; }
        public string Number { get; set; }
        public string Dates { get; set; }
        public string Note { get; set; }
        public string Subjects { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string ScopeAndContent { get; set; }
        public string CustodialHistory { get; set; }
        public string Size { get; set; }
        public string Acquisitioninfo { get; set; }
        
        public string Language { get; set; }
        public string PreservationNote { get; set; }
        public string Rights { get; set; }

        public string AccessRestrictions { get; set; }
        public string PublicationRights { get; set; }
        public string PreferredCitation { get; set; }
        public string RelatedCollection { get; set; }
        public string SeparatedMaterial { get; set; }
        public string OriginalLocation { get; set; }
        public string CopiesLocation { get; set; }
        public string PublicationNote { get; set; }
        public string Creator { get; set; }
        public string Contributors { get; set; }
        public string ProcessedBy { get; set; }
        public string Sponsors { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public System.DateTime UpdatedDate { get; set; }
    }
}
