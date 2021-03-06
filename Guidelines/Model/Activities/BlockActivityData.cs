﻿using System;
using Guidelines.Model.DataBag;

namespace Guidelines.Model
{
    /// <summary>
    ///     Provides data about a block activity (the smallest unit of a guideline).
    /// </summary>
    public class BlockActivityData : IEntityWithIcon, IDataBagEntity
    {
        private bool _activated;

        public BlockActivityData( Block block, string title = "", int scoreWhenActivated = 1 )
        {
            Block = block;
            Title = title;
            ScoreWhenActivated = scoreWhenActivated;
        }

        /// <summary>
        ///     Gets and sets a category name for this block activity (used for checklist assessment blocks).
        /// </summary>
        public string CategoryName { get; set; }

        public Block Block { get; set; }

        /// <summary>
        ///     Gets or sets the title of the block activity.
        /// </summary>
        /// <value>
        ///     The title is the question/action that needs to be taken (e.g. Objective change in behaviour or mental state).
        /// </value>
        public string Title { get; set; }

        /// <summary>
        ///     Gets or sets descriptive text to expand upon the title/provide extra information.
        /// </summary>
        /// <value>
        ///     The descriptive text can be used to provide extra information for the user - e.g. If TITLE is "Send Blood",
        ///     extra information could be: Lactate, blood cultures, FBC, U&Es, CRP, coagulation screen.
        /// </value>
        public string DescriptiveText { get; set; }

        /// <summary>
        ///     Gets or sets the score of this activity when it is activated.
        /// </summary>
        /// <value>
        ///     The score that this activity contributes when it is activated. This will be used by the parent <see cref="Block" />
        ///     to calculate an overall score for the block. When the <see cref="BlockActivityData" /> is not activated, the score
        ///     will be 0.
        /// </value>
        public int ScoreWhenActivated { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="BlockActivityData" /> is activated.
        /// </summary>
        /// <value>
        ///     <c>true</c> if activated; otherwise, <c>false</c>.
        /// </value>
        public bool Activated
        {
            get { return _activated; }
            set
            {
                _activated = value;
                WhenActivated = DateTime.UtcNow;
            }
        }

        /// <summary>
        ///     Gets the score of this activity.
        /// </summary>
        /// <value>
        ///     The score for the activity is calculated based on whether it is activated or not (<see cref="Activated" />). If
        ///     Activated is True, then this value is equal to the <see cref="ScoreWhenActivated" />. When Activated is false, then
        ///     this value is equal to 0.
        /// </value>
        public int Score => Activated ? ScoreWhenActivated : 0;

        /// <summary>
        ///     Gets when this activity was activated.
        /// </summary>
        public DateTime WhenActivated { get; private set; }

        public string EntityIconName { get; set; }
        public EntityDataBag DataBag { get; set; } = new EntityDataBag();
    }
}