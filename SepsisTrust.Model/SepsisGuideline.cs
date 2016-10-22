using System;
using Guidelines.Model;

namespace SepsisTrust.Model
{
    /// <summary>
    /// Represents a sepsis guideline.
    /// </summary>
    public class SepsisGuideline : Guideline
    {
        public int YoungestRelevantAge { get; set; }

        public int OldestRelevantAge { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }
    }
}