namespace Guidelines.Model
{
    public class SummaryBlock : Block
    {
        public SummaryBlock(Phase parentPhase) : base(parentPhase)
        {
        }

        public string SummaryText { get; set; }

        public string SummaryImagePath { get; set; }

        public SummaryType SummaryType { get; set; } = SummaryType.Information;
    }

    public enum SummaryType
    {
        Information = 1,
        Warning = 2,
        Alert = 3
    }
}