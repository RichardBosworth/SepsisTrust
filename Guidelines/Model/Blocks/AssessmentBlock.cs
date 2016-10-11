namespace Guidelines.Model
{
    /// <summary>
    /// Represents a guideline block that contains an assessment.
    /// </summary>
    /// <seealso cref="Guidelines.Model.Block" />
    public class AssessmentBlock : Block
    {
        public AssessmentBlock(Phase parentPhase) : base(parentPhase)
        {
        }
        
        public AssessmentType AssessmentType { get; set; } = AssessmentType.Checklist;
    }

    public enum AssessmentType
    {
        /// <summary>
        /// The block is a question-based assessment (e.g. a yes/no answer).
        /// </summary>
        Question,

        /// <summary>
        /// The block is a checklist-based assessment (e.g. number of criteria to be assessed).
        /// </summary>
        Checklist
    }
}