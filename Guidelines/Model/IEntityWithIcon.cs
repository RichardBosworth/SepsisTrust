namespace Guidelines.Model
{
    /// <summary>
    /// Provides functionality to enable this entity to have a representive icon.
    /// </summary>
    public interface IEntityWithIcon
    {
        /// <summary>
        /// Gets or sets the name of the icon (based on icon fonts available) that will be used to enhance this entity.
        /// </summary>
        string EntityIconName { get; set; }
    }
}