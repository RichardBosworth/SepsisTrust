namespace SepsisTrust.Model.Azure
{
    public class AzureFullGuideline : AzureEntityData
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string GuidelineIdentifier { get; set; }
        public int YoungestRelevantAge { get; set; }
        public int OldestRelevantAge { get; set; }
        public string GuidelineContent { get; set; }
    }
}