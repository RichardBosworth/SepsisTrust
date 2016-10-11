namespace Guidelines.Model
{
    /// <summary>
    /// Represents an entity within the model that should be able to be identified via a string identifier.
    /// </summary>
    public interface IIdentifiableGuidelineEntity : IGuidelineEntity
    {
        /// <summary>
        /// Gets or sets the identifier for this entity.
        /// </summary>
        /// <value>
        /// The identifier that can be used to identify the entity.
        /// </value>
        string Identifier { get; set; }
    }
}