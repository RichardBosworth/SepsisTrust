using System.Collections.Generic;

namespace SepsisTrust.Model
{
    /// <summary>
    /// Represents a clinical area, related to which are a number of guidelines relevant to this clinical area.
    /// </summary>
    public class ClinicalArea
    {
        /// <summary>
        /// Gets or sets the name of the <see cref="ClinicalArea"/>.
        /// </summary>
        /// <value>
        /// The name of the <see cref="ClinicalArea"/>.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a list of the guideline identifiers for those guidelines relevant to this clinical area.
        /// </summary>
        /// <value>
        /// A list of the string identifiers of the guidelines relevant to this clinical area.
        /// </value>
        public List<string> RelevantGuidelineIdentifiers { get; set; } = new List<string>();
    }
}