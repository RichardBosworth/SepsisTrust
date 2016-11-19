using System.Linq;
using Prism.Events;
using SepsisTrust.Messages.Guideline;
using Xamarin.Forms;

namespace Guidelines.Model.Running
{
    public class DefaultGuidelineRunner : IGuidelineRunner
    {
        private readonly Guideline _guideline;
        private readonly IEventAggregator _eventAggregator;
        private Block _previousBlock;
        private Block _workingBlock;

        public DefaultGuidelineRunner(Guideline guideline, IEventAggregator eventAggregator)
        {
            _guideline = guideline;
            _eventAggregator = eventAggregator;
        }

        public Block WorkingBlock
        {
            get { return _workingBlock; }
            set
            {
                _eventAggregator.GetEvent<GuidelineEntityFinishedEvent>().Publish(_workingBlock);
                _workingBlock = value;
                _eventAggregator.GetEvent<GuidelineEntityStartedEvent>().Publish(_workingBlock);
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

            var nextBlockLink = WorkingBlock.Links.FirstOrDefault(link => link.Valid(WorkingBlock.Score));
            var block = _guideline.LinkManager.ObtainEntity(nextBlockLink.LinkedGuidelineEntityIdentifier) as Block;

            WorkingBlock = block;

            return block;
        }

        public void SetCurrentBlock(Block block)
        {
            WorkingBlock = block;
        }
    }
}