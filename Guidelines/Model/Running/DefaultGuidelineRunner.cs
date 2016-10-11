using System.Linq;

namespace Guidelines.Model.Running
{
    public class DefaultGuidelineRunner : IGuidelineRunner
    {
        private readonly Guideline _guideline;
        private Block _previousBlock;
        private Block _workingBlock;

        public DefaultGuidelineRunner(Guideline guideline)
        {
            _guideline = guideline;
        }

        public Block Start()
        {
            var block = _guideline.EntryPhase.EntryBlock;

            _workingBlock = block;

            return block;
        }

        public Block MoveForwards()
        {
            // Save the previous block.
            _previousBlock = _workingBlock;

            var nextBlockLink = _workingBlock.Links.FirstOrDefault(link => link.Valid(_workingBlock.Score));
            var block = _guideline.LinkManager.ObtainEntity(nextBlockLink.LinkedGuidelineEntityIdentifier) as Block;

            _workingBlock = block;

            return block;
        }
    }
}