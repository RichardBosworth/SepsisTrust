namespace Guidelines.Model
{
    /// <summary>
    /// Provides functionality to move through a guideline.
    /// </summary>
    public interface IGuidelineRunner
    {
        Block Start();
        Block MoveForwards();
        void SetCurrentBlock(Block block);
    }
}