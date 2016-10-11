namespace Guidelines.Model.Links
{
    /// <summary>
    ///     Provides functionality to manage links within a guideline.
    /// </summary>
    public interface IGuidelineLinkManager
    {
        void Register(IIdentifiableGuidelineEntity entity);

        IIdentifiableGuidelineEntity ObtainEntity(string identifier);
    }
}