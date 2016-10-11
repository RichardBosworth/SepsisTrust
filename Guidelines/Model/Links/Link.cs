namespace Guidelines.Model
{
    /// <summary>
    /// Represents a link between entities.
    /// </summary>
    public class Link
    {
        public Link(double minimumScoreToActivate, double maximumScoreToActivate, IIdentifiableGuidelineEntity linkedGuidelineEntity)
        {
            MaximumScoreToActivate = maximumScoreToActivate;
            MinimumScoreToActivate = minimumScoreToActivate;
            LinkedGuidelineEntityIdentifier = linkedGuidelineEntity.Identifier;
        }

        public Link(double minimumScoreToActivate, double maximumScoreToActivate, string linkedGuidelineEntityIdentifier)
        {
            MaximumScoreToActivate = maximumScoreToActivate;
            MinimumScoreToActivate = minimumScoreToActivate;
            LinkedGuidelineEntityIdentifier = linkedGuidelineEntityIdentifier;
        }

        public double MaximumScoreToActivate { get; set; }

        public double MinimumScoreToActivate { get; set; }

        public string LinkedGuidelineEntityIdentifier { get; set; }

        public bool Valid(double score)
        {
            return score >= MinimumScoreToActivate && score <= MaximumScoreToActivate;
        }
    }
}