using Guidelines.IO;
using Guidelines.Model;
using Guidelines.Model.Running;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SepsisTrust.Model;
using SepsisTrust.Model.Navigation;

namespace SepsisTrust.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private string _title;

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateCommand = new DelegateCommand(Navigate);
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DelegateCommand NavigateCommand { get; set; }


        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("title"))
                Title = (string) parameters["title"] + " and Prism";
        }

        private async void Navigate()
        {
            IGuidelineRetriever guidelineRetriever = new XmlFileGuidelineRetriever();
            var guideline = await guidelineRetriever.RetrieveGuidelineAsync(string.Empty);
            IGuidelineRunner guidelineRunner = new DefaultGuidelineRunner(guideline);
            var startBlock = guidelineRunner.Start();
            var navigationModel = new GuidelinePageNavigationModel
                                  {
                                      CurrentBlock = startBlock,
                                      CurrentGuidelineRunner = guidelineRunner
                                  };
            await _navigationService.NavigateAsync("GNav/GuidelinePage", navigationModel.ToNavigationParameters());
        }
    }
}