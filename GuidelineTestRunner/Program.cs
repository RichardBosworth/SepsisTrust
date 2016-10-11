using System;
using System.Linq;
using Guidelines.Model;
using SepsisTrust.Model;

namespace GuidelineTestRunner
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var guideline = TemporaryGuidelineGenerator.Generate();

            Console.WriteLine(guideline.Title);
            Console.WriteLine(guideline.Description);

            IGuidelineRunner guidelineRunner = new TestGuidelineRunner(guideline);
            var startBlock = guidelineRunner.Start();
            RenderBlock(startBlock);

            while (true)
            {
                var newBlock = guidelineRunner.MoveForwards();
                RenderBlock(newBlock);
            }
        }

        private static void RenderBlock(Block block)
        {
            Console.WriteLine(String.Empty);
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine(String.Empty);
            Console.WriteLine(block.Title);
            if (block is SummaryBlock)
            {
                var summaryBlock = block as SummaryBlock;
                Console.WriteLine(summaryBlock?.SummaryText);
            }
            Console.WriteLine(String.Empty);
            Console.WriteLine("Activities:");
            foreach (var activity in block.BlockActivities)
            {
                Console.WriteLine(activity.Title);
            }

            var line = Console.ReadLine();

            for (var i = 0; i < line.Length; i++)
            {
                block.BlockActivities[i].Activated = line[i] != '0';
            }
        }
    }

    public class TestGuidelineRunner : IGuidelineRunner
    {
        private Block _currentBlock;
        private readonly Guideline _guideline;
        private bool _started = false;
        private Block _previousBlock = null;

        public TestGuidelineRunner(Guideline guideline)
        {
            _guideline = guideline;
        }

        public Block Start()
        {
            var block = _guideline.EntryPhase.EntryBlock;
            _currentBlock = block;
            return block;
        }

        public Block MoveForwards()
        {
            _previousBlock = _currentBlock;

            var nextBlockIdentifier = _currentBlock.Links.First(link => link.Valid(_currentBlock.Score)).LinkedGuidelineEntityIdentifier;
            Block block = _guideline.LinkManager.ObtainEntity(nextBlockIdentifier) as Block;
            _currentBlock = block;
            return block;
        }

        public Block MoveBackwards()
        {
            throw new NotImplementedException();
        }
    }
}