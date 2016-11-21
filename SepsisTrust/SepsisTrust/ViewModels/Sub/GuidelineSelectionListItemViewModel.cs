using Guidelines.IO;
using Guidelines.Model;
using Guidelines.Model.Running;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using SepsisTrust.Model.Navigation;

namespace SepsisTrust.ViewModels.Sub
{
    public class GuidelineSelectionListItemViewModel : BindableBase
    {
        private string _iconName;
        private INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;

        private DelegateCommand _startGuidelineCommand;
        private string _title;

        private string _description;

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        public GuidelineSelectionListItemViewModel( INavigationService navigationService, IEventAggregator  eventAggregator)
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;

            StartGuidelineCommand = new DelegateCommand(StartGuideline);
        }

        public DelegateCommand StartGuidelineCommand
        {
            get { return _startGuidelineCommand; }
            set { SetProperty(ref _startGuidelineCommand, value); }
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public string IconName
        {
            get { return _iconName; }
            set { SetProperty(ref _iconName, value); }
        }

        private string _identifier;

        public string Identifier
        {
            get { return _identifier; }
            set { SetProperty(ref _identifier, value); }
        }

        private async void StartGuideline( )
        {
            IGuidelineRetriever guidelineRetriever = new LocalXmlGuidelineRetriever();
            var guideline = await guidelineRetriever.RetrieveGuidelineAsync(Identifier);
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