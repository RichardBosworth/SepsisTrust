using System.Collections.Generic;
using Guidelines.Model.DataBag;
using Guidelines.Model.Links;

namespace Guidelines.Model
{
    /// <summary>
    ///     Represents a full guideline.
    /// </summary>
    public class Guideline : IIdentifiableGuidelineEntity
    {
        /// <summary>
        ///     Gets or sets the title of the guideline.
        /// </summary>
        /// <value>
        ///     The title of the guideline.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        ///     Gets or sets a description for the guideline.
        /// </summary>
        /// <value>
        ///     A short description that describes what the guideline is, for whom it is relevant etc.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets a collection of the phases that compose this guideline.
        /// </summary>
        /// <value>
        ///     A <see cref="IEnumerable{T}" /> of the <see cref="Phase" /> collection that make up this guideline.
        /// </value>
        public List<Phase> Phases { get; set; } = new List<Phase>();

        /// <summary>
        /// Gets or sets the entry phase.
        /// </summary>
        /// <value>
        /// The entry phase is the phase that should be the phase that is first in the guidelines.
        /// </value>
        public Phase EntryPhase { get; set; }

        /// <inheritdoc />
        public string Identifier { get; set; }

        /// <summary>
        /// Gets the link manager for this guideline.
        /// </summary>
        /// <value>
        /// The link manager handles the links between blocks and phases.
        /// </value>
        public IGuidelineLinkManager LinkManager { get; private set; } = new DefaultGuidelineLinkManager();

        public EntityDataBag DataBag { get; set; }
    }
}