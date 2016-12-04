using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IEventAggregator _eventAggregator;

        private string _description;
        private string _iconName;

        private string _identifier;
        private readonly INavigationService _navigationService;

        private DelegateCommand _startGuidelineCommand;
        private string _title;

        public GuidelineSelectionListItemViewModel( INavigationService navigationService, IEventAggregator eventAggregator )
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;

            StartGuidelineCommand = new DelegateCommand(StartGuideline);
        }

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
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

        public string Identifier
        {
            get { return _identifier; }
            set { SetProperty(ref _identifier, value); }
        }

        private async void StartGuideline( )
        {
            try
            {

                // Retrieve the guideline from the remote server.
                IGuidelineRetriever guidelineRetriever = new AzureRemoteGuidelineRetriever();
                var guideline = await guidelineRetriever.RetrieveGuidelineAsync(Identifier);

                // Start running that guideline.
                IGuidelineRunner guidelineRunner = new DefaultGuidelineRunner(guideline, _eventAggregator);
                var startBlock = guidelineRunner.Start();
                var navigationModel = new GuidelinePageNavigationModel
                {
                    CurrentBlock = startBlock,
                    CurrentGuidelineRunner = guidelineRunner
                };
                await _navigationService.NavigateAsync("GuidelinePage", navigationModel.ToNavigationParameters());
            }
            catch (Exception exception)
            {
                
                throw;
            }
        }
    }

    public class AzureRemoteGuidelineRetriever : IGuidelineRetriever
    {
        public async Task<Guideline> RetrieveGuidelineAsync( string identifier )
        {
            // Obtain the guideline content from the server.
            IAzureCRUDService azureCrudService = new RemoteAzureCRUDService(StaticAzureService.MobileServiceClient);
            var query = azureCrudService.CreateQuery<Model.Azure.Guideline>()
                .Where(guideline => guideline.GuidelineIdentifier.ToLower() == identifier.ToLower())
                .Select(guideline => guideline.GuidelineContent);
            var guidelines = await azureCrudService.ExecuteQuery(query);
            var returnedGuidelineContent = guidelines.FirstOrDefault();

            // Parse the XML for that guideline.
            var guidelineXmlParser = new GuidelineXmlParser();
            var parsedGuideline = await guidelineXmlParser.ParseGuidelineXmlAsync(returnedGuidelineContent);
            return parsedGuideline;
        }
    }
}