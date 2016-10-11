using System;
using System.Linq;
using Guidelines.Model;
using Guidelines.Model.Running;
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

            IGuidelineRunner guidelineRunner = new DefaultGuidelineRunner(guideline);
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
            if (block == null)
            {
                return;
            }

            Console.WriteLine(String.Empty);
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine(String.Empty);
            Console.WriteLine(block?.Title);
            if (block is SummaryBlock)
            {
                var summaryBlock = block as SummaryBlock;
                Console.WriteLine(summaryBlock?.SummaryText);
            }
            Console.WriteLine(String.Empty);
            Console.WriteLine(block.BlockActivities.Count > 0 ? "Activities:" : string.Empty);
            foreach (var activity in block?.BlockActivities)
            {
                Console.WriteLine(activity.Title);
            }

            var line = Console.ReadLine();

            for (var i = 0; i < line?.Length; i++)
            {
                block.BlockActivities[i].Activated = line[i] != '0';
            }
        }
    }
}