namespace SepsisTrust.Model.Azure
{
    public class Guideline : AzureEntityData
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ClinicalAreaId { get; set; }
        public string Identifier { get; set; }
        public string IconName { get; set; }
        public string Description { get; set; }
    }
}