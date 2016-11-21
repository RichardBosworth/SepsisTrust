using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using AzureData;
using Guidelines.IO;
using Guidelines.Model;
using Guidelines.Model.Running;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using SepsisTrust.Model.Navigation;
using SepsisTrust.Model.Retrieval;

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
            IGuidelineRetriever guidelineRetriever = new AzureRemoteGuidelineRetriever();
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

    public class AzureRemoteGuidelineRetriever : IGuidelineRetriever
    {
        public async Task<Guideline> RetrieveGuidelineAsync( string identifier )
        {
            // Obtain the guideline from the server.
            if ( !StaticAzureService.IsInitialized )
            {
                StaticAzureService.Initialize();
            }
            IAzureCRUDService azureCRUDService = new RemoteAzureCRUDService(StaticAzureService.MobileServiceClient);
            var query = azureCRUDService.CreateQuery<Model.Azure.Guideline>();
            var parameterisedQuery = query.WithParameters(new Dictionary<string, string>() {{"select", "guidelineIdentifier, guidelineContent"}});
            var finalQuery = parameterisedQuery.Where(guideline => guideline.GuidelineIdentifier.ToLower() == identifier.ToLower());
            var guidelines = await azureCRUDService.ExecuteQuery(finalQuery);
            var returnedGuideline = guidelines.FirstOrDefault();
            
            // Parse the XML for that guideline.
            GuidelineXmlParser guidelineXmlParser = new GuidelineXmlParser();
            var parsedGuideline = await guidelineXmlParser.ParseGuidelineXmlAsync(returnedGuideline.GuidelineContent);
            return parsedGuideline;
        }
    }
}