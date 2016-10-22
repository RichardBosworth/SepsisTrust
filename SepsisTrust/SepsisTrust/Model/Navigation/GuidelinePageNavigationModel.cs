using Guidelines.Model;
using Prism.Navigation;

namespace SepsisTrust.Model.Navigation
{
    public class GuidelinePageNavigationModel : INavigationModel
    {
        private const string CurrentBlockParameterName = "currentBlock";
        private const string CurrentGuidelineRunnerParameterName = "currentGuidelineRunner";

        public GuidelinePageNavigationModel()
        {
        }

        public GuidelinePageNavigationModel(NavigationParameters navigationParameters)
        {
            CurrentBlock = navigationParameters[CurrentBlockParameterName] as Block;
            CurrentGuidelineRunner = navigationParameters[CurrentGuidelineRunnerParameterName] as IGuidelineRunner;
        }

        public Block CurrentBlock { get; set; }
        public IGuidelineRunner CurrentGuidelineRunner { get; set; }

        public NavigationParameters ToNavigationParameters()
        {
            return new NavigationParameters
            {
                {CurrentBlockParameterName, CurrentBlock},
                {CurrentGuidelineRunnerParameterName, CurrentGuidelineRunner}
            };
        }
    }
}