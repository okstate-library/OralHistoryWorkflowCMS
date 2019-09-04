namespace Model
{
    /// <summary>
    /// Defines the subsery properties.
    /// </summary>
    public class SubseryModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the subsery.
        /// </summary>
        /// <value>
        /// The name of the subsery.
        /// </value>
        public string SubseryName { get; set; }
        /// <summary>
        /// Gets or sets the collection identifier.
        /// </summary>
        /// <value>
        /// The collection identifier.
        /// </value>
        public short CollectionId { get; set; }
        public string CollectionName { get; set; }
    }
}
