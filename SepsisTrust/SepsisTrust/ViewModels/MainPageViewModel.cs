using Guidelines.IO;
using Guidelines.Model;
using Guidelines.Model.Running;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SepsisTrust.Model.Navigation;

namespace SepsisTrust.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private string _title;

        public MainPageViewModel( INavigationService navigationService )
        {
            _navigationService = navigationService;
            NavigateCommand = new DelegateCommand<string>(s => Navigate(s));
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DelegateCommand<string> NavigateCommand { get; set; }


        public void OnNavigatedFrom( NavigationParameters parameters )
        {
        }

        public async void OnNavigatedTo( NavigationParameters parameters )
        {
        }

        public void OnNavigatingTo( NavigationParameters parameters )
        {
            
        }

        private async void Navigate( string guidelineFileName )
        {
            IGuidelineRetriever guidelineRetriever = new XmlFileGuidelineRetriever();
            var guideline = await guidelineRetriever.RetrieveGuidelineAsync(guidelineFileName);
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