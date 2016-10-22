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

        public Block WorkingBlock
        {
            get { return _workingBlock; }
            set
            {
                _workingBlock = value;
                _workingBlock.CharacteristicsHolder.Start();
            }
        }

        public Block PreviousBlock
        {
            get { return _previousBlock; }
            set
            {
                _previousBlock = value; 
                _previousBlock.CharacteristicsHolder.Finish();
            }
        }

        public Block Start()
        {
            var block = _guideline.EntryPhase.EntryBlock;

            WorkingBlock = block;

            return block;
        }

        public Block MoveForwards()
        {
            // Save the previous block.
            PreviousBlock = WorkingBlock;

            var nextBlockLink = WorkingBlock.Links.FirstOrDefault(link => link.Valid(WorkingBlock.Score));
            var block = _guideline.LinkManager.ObtainEntity(nextBlockLink.LinkedGuidelineEntityIdentifier) as Block;

            WorkingBlock = block;

            return block;
        }
    }
}