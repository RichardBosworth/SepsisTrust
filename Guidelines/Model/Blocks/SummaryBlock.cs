namespace Guidelines.Model
{
    public class SummaryBlock : Block
    {
        public SummaryBlock(Phase parentPhase) : base(parentPhase)
        {
        }

        public string SummaryText { get; set; }

        public string SummaryImagePath { get; set; }
    }
}