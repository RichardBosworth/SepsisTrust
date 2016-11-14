namespace Guidelines.Model
{
    /// <summary>
    /// Provides functionality to enable this entity to have a representive icon.
    /// </summary>
    public interface IEntityWithIcon
    {
        /// <summary>
        /// Gets or sets the Url to the icon file that will be used to enhance this entity.
        /// </summary>
        string EntityIconName { get; set; }
    }
}