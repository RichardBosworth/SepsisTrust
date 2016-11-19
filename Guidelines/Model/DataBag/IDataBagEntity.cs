namespace Guidelines.Model.DataBag
{
    /// <summary>
    /// Enables an entity to contain extra data.
    /// </summary>
    public interface IDataBagEntity
    {
        EntityDataBag DataBag { get; set; }
    }
}