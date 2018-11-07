using Core.Enums;

namespace Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Model.BaseModel" />
    public class FindReplaceModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the find word.
        /// </summary>
        /// <value>
        /// The find word.
        /// </value>
        public string FindWord { get; set; }

        /// <summary>
        /// Gets or sets the replace word.
        /// </summary>
        /// <value>
        /// The replace word.
        /// </value>
        public string ReplaceWord { get; set; }

        /// <summary>
        /// Gets or sets the field.
        /// </summary>
        /// <value>
        /// The field.
        /// </value>
        public WellKnownFindAndReplaceType Field { get; set; }
    }
}
