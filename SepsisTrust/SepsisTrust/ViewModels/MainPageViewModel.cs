using System.Linq;
using AzureData;
using Guidelines.Extensions;
using Guidelines.IO;
using Guidelines.Model;
using Guidelines.Model.Running;
using Microsoft.WindowsAzure.MobileServices;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using SepsisTrust.Model;
using SepsisTrust.Model.Navigation;
using SepsisTrust.Model.Retrieval;
using ClinicalArea = SepsisTrust.Model.Azure.ClinicalArea;

namespace SepsisTrust.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;
        private string _title;
        private static AuditGuidelineExtension _auditGuidelineExtension;

        private DelegateCommand _navigateToUserDetailsCommand;

        public DelegateCommand NavigateToUserDetailsCommand
        {
            get { return _navigateToUserDetailsCommand; }
            set { SetProperty(ref _navigateToUserDetailsCommand, value); }
        }

        public MainPageViewModel( INavigationService navigationService, IEventAggregator eventAggregator )
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
            NavigateCommand = new DelegateCommand<string>(Navigate);

            GuidelineExtensions.Register<AuditGuidelineExtension>(eventAggregator);

            NavigateToUserDetailsCommand = new DelegateCommand(( ) => navigationService.NavigateAsync("EditUser"));
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

        public async void OnNavigatingTo( NavigationParameters parameters )
        {
            StaticAzureService.Initialize(StaticAzureService.AppServiceUrl);
        }

        private async void Navigate( string guidelineFileName )
        {
            IGuidelineRetriever guidelineRetriever = new LocalXmlGuidelineRetriever();
            var guideline = await guidelineRetriever.RetrieveGuidelineAsync(guidelineFileName);
            IGuidelineRunner guidelineRunner = new DefaultGuidelineRunner(guideline, _eventAggregator);
            var startBlock = guidelineRunner.Start();
            var navigationModel = new GuidelinePageNavigationModel
                                  {
                                      CurrentBlock = startBlock,
                                      CurrentGuidelineRunner = guidelineRunner
                                  };
            await _navigationService.NavigateAsync("GuidelinePage", navigationModel.ToNavigationParameters());
        }
    }
}