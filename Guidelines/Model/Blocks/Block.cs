﻿using System.Collections.Generic;
using System.Linq;
using Guidelines.Model.DataBag;

namespace Guidelines.Model
{
    /// <summary>
    ///     Represents a block of activities within a Guideline phase.
    /// </summary>
    /// <seealso cref="IIdentifiableGuidelineEntity" />
    public class Block : IIdentifiableGuidelineEntity, IEntityWithIcon, IDataBagEntity
    {
        public Block( Phase parentPhase )
        {
            ParentPhase = parentPhase;
        }

        /// <summary>
        ///     Gets the score for this block.
        /// </summary>
        /// <value>
        ///     The value is calculated as a sum of the scores of the activities (<see cref="BlockActivityData" />) within this
        ///     block.
        /// </value>
        public int Score => BlockActivities.Sum(data => data.Score);

        /// <summary>
        ///     Gets the links to other blocks or phases.
        /// </summary>
        /// <value>
        ///     The links between this block and other linked entities.
        /// </value>
        public List<Link> Links { get; private set; } = new List<Link>();


        /// <summary>
        ///     Gets the phase that this block is contained within.
        /// </summary>
        /// <value>
        ///     The parent phase of this block.
        /// </value>
        public Phase ParentPhase { get; private set; }

        /// <summary>
        ///     Gets or sets the activities contained within this block.
        /// </summary>
        /// <value>
        ///     A list of activities that are contained within this block.
        /// </value>
        public List<BlockActivityData> BlockActivities { get; set; } = new List<BlockActivityData>();


        /// <summary>
        ///     Gets or sets the title for the block.
        /// </summary>
        /// <value>
        ///     The title for the block - should be used to succinctly define the purpose/nature of the content of the block.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        ///     Gets and sets a quick, interface friendly title that identifies the block.
        /// </summary>
        public string FriendlyTitle { get; set; }

        public EntityDataBag DataBag { get; set; } = new EntityDataBag();
        public string EntityIconName { get; set; }

        public string Identifier { get; set; }
    }
}