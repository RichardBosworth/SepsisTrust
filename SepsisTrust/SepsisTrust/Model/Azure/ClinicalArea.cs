using System;

namespace SepsisTrust.Model.Azure
{

    public class ClinicalArea
    {
        public bool deleted { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime createdAt { get; set; }
        public string version { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public Relatedguideline[] relatedGuidelines { get; set; }
    }

    public class Relatedguideline
    {
        public string title { get; set; }
        public string description { get; set; }
        public string guidelineIdentifier { get; set; }
        public int youngestRelevantAge { get; set; }
        public int oldestRelevantAge { get; set; }
        public string guidelineContent { get; set; }
        public string clinicalAreaId { get; set; }
        public string id { get; set; }
        public string version { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public bool deleted { get; set; }
    }

}