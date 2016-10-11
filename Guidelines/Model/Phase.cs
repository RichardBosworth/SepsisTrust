using System.Collections.Generic;
using Guidelines.Characteristics;

namespace Guidelines.Model
{
    /// <summary>
    ///     Represents a phase (that is, a defined collection of blocks) in a guideline.
    /// </summary>
    public class Phase : IIdentifiableGuidelineEntity
    {
        public Phase(Guideline parentGuideline)
        {
            ParentGuideline = parentGuideline;
        }

        public Guideline ParentGuideline { get; private set; }

        public List<Block> Blocks { get; private set; } = new List<Block>();

        /// <summary>
        /// Gets or sets the entry block.
        /// </summary>
        /// <value>
        /// The entry block is the block that is the first of the blocks within the phase.
        /// </value>
        public Block EntryBlock { get; set; }

        public List<Link> PhaseLinks { get; private set; } = new List<Link>();
        public CharacteristicsHolder CharacteristicsHolder { get; set; } = new CharacteristicsHolder();

        public string Identifier { get; set; }
    }
}