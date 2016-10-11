using System.Linq;

namespace Guidelines.Model.Running
{
    public class DefaultGuidelineRunner : IGuidelineRunner
    {
        private Block _workingBlock = null;
        private readonly Guideline _guideline;
        private Block _previousBlock = null;

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

            Link nextBlockLink = _workingBlock.Links.FirstOrDefault(link => link.Valid(_workingBlock.Score));
            Block block = _guideline.LinkManager.ObtainEntity(nextBlockLink.LinkedGuidelineEntityIdentifier) as Block;

            _workingBlock = block;

            return block;
        }
    }
}