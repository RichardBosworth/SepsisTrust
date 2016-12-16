using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading.Tasks;
using AzureData;
using Guidelines.Extensions;
using Humanizer;
using PCLStorage;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using SepsisTrust.Model.Azure;
using SepsisTrust.Model.Storage;
using SepsisTrust.Model.User;
using SepsisTrust.ViewModels.Sub;

namespace SepsisTrust.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private static AuditGuidelineExtension _auditGuidelineExtension;
        private readonly IEventAggregator _eventAggregator;
        private readonly IFileStreamRetriever _fileStreamRetriever;
        private readonly IJsonObjectStreamReader _jsonObjectStreamReader;
        private readonly INavigationService _navigationService;
        private AppUserData _appUserData;

        private string _currentClinicalAreaName;

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private DelegateCommand _refreshCommand;

        public DelegateCommand RefreshCommand
        {
            get { return _refreshCommand; }
            set { SetProperty(ref _refreshCommand, value); }
        }

        public string CurrentClinicalAreaName
        {
            get { return _currentClinicalAreaName; }
            set { SetProperty(ref _currentClinicalAreaName, value); }
        }

        private DelegateCommand _navigateToUserDetailsCommand;

        private ObservableCollection<GuidelineSelectionListItemViewModel> _selectableGuidelines;
        private Model.ClinicalArea _currentClinicalArea;

        public MainPageViewModel(INavigationService navigationService, IFileStreamRetriever fileStreamRetriever,
            IJsonObjectStreamReader jsonObjectStreamReader, IEventAggregator eventAggregator)
        {
            // Save variables.
            _navigationService = navigationService;
            _fileStreamRetriever = fileStreamRetriever;
            _jsonObjectStreamReader = jsonObjectStreamReader;
            _eventAggregator = eventAggregator;

            // Generate commands.
            NavigateToUserDetailsCommand = new DelegateCommand(() => navigationService.NavigateAsync("EditUser"));

            // Register guideline extensions.
            GuidelineExtensions.Register<AuditGuidelineExtension>(eventAggregator);

            // Initialize properties.
            SelectableGuidelines = new ObservableCollection<GuidelineSelectionListItemViewModel>();

            // Generate the refresh command.
            RefreshCommand = new DelegateCommand(RefreshListData);
        }

        private async void RefreshListData()
        {
            IsBusy = true;
            await RefreshGuidelineDataAsync(_currentClinicalArea.Id, true);
            IsBusy = false;
        }

        public ObservableCollection<GuidelineSelectionListItemViewModel> SelectableGuidelines
        {
            get { return _selectableGuidelines; }
            set { SetProperty(ref _selectableGuidelines, value); }
        }

        public DelegateCommand NavigateToUserDetailsCommand
        {
            get { return _navigateToUserDetailsCommand; }
            set { SetProperty(ref _navigateToUserDetailsCommand, value); }
        }


        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public async void OnNavigatingTo(NavigationParameters parameters)
        {
            // Generate the most appropriate app user data.
            await EstablishUserData(parameters);
            CurrentClinicalAreaName = _appUserData.ClinicalArea.Name.Humanize();

            // Get the clinical area for the app user.
            _currentClinicalArea = _appUserData?.ClinicalArea;
            var clinicalAreaId = _currentClinicalArea?.Id;

            // Refresh the guideline data based on the clinical area of the user.
            await RefreshGuidelineDataAsync(clinicalAreaId);
        }

        private async Task RefreshGuidelineDataAsync(string clinicalAreaId, bool syncData = false)
        {
            // Get the guidelines that match the clinical area.
            ISyncronizedAzureCrudService azureCrudService = new LocalAzureCRUDService(StaticAzureService.MobileServiceClient);

            if (syncData)
            {
                await azureCrudService.SyncronizeTableAsync<Guideline>();
                await azureCrudService.SyncronizeTableAsync<ClinicalArea>();
            }

            var guidelinesOfAreaQuery = azureCrudService.CreateQuery<Guideline>()
                .Where(guideline => (guideline.ClinicalAreaId == clinicalAreaId) && (guideline.GuidelineContent != null))
                .Select(
                    guideline =>
                        new
                        {
                            guideline.Title,
                            guideline.GuidelineIdentifier,
                            guideline.IconName,
                            guideline.Description
                        });
            var guidelines = await azureCrudService.ExecuteQuery(guidelinesOfAreaQuery);

            // Generate view models for those guidelines
            SelectableGuidelines.Clear();
            foreach (var guideline in guidelines)
            {
                var itemViewModel = new GuidelineSelectionListItemViewModel(_navigationService, _eventAggregator)
                {
                    Title = guideline.Title,
                    Identifier = guideline.GuidelineIdentifier,
                    IconName = guideline.IconName,
                    Description = guideline.Description
                };
                SelectableGuidelines.Add(itemViewModel);
            }
        }

        private async Task EstablishUserData(NavigationParameters parameters)
        {
            // Check if user data has been provided in the parameters.
            if (parameters.ContainsKey("userData"))
                _appUserData = parameters["userData"] as AppUserData;

            if (_appUserData == null)
                if (StaticUserDataStore.UserData != null)
                {
                    // This copies the memory-based user data, thus ensuring that
                    // the Save/Back functionality works.
                    // If this is directly set to the memory-based store, changes will
                    // be automatically "saved".
                    var memoryAppUserData = StaticUserDataStore.UserData;
                    _appUserData = new AppUserData();
                    foreach (var runtimeProperty in memoryAppUserData.GetType().GetRuntimeProperties())
                    {
                        var memoryAppData = runtimeProperty.GetValue(memoryAppUserData);
                        runtimeProperty.SetValue(_appUserData, memoryAppData);
                    }
                }

            // Otherwise, load app user data from file path.
            if (_appUserData == null)
            {
                // This loads the data from persistent storage into a new instance
                // of app user data.
                _appUserData = new AppUserData();
                _appUserData = await LoadAppUserDataFromFileAsync(StaticUserDataStore.UserFileName);
            }
        }


        private async Task<AppUserData> LoadAppUserDataFromFileAsync(string userFileName)
        {
            var fileReadStream = await _fileStreamRetriever.ObtainFileStreamAsync(userFileName, false, FileAccess.Read);
            return await _jsonObjectStreamReader.Read<AppUserData>(fileReadStream);
        }
    }
}