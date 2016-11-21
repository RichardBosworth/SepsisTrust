using System.Collections.Generic;
using System.Collections.ObjectModel;
using AzureData;
using Prism.Mvvm;
using Prism.Navigation;
using SepsisTrust.Model.Azure;
using SepsisTrust.Model.User;
using SepsisTrust.ViewModels.Sub;

namespace SepsisTrust.ViewModels
{
    public class SelectClinicalAreaPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private ObservableCollection<ClinicalAreaListItemViewModel> _availableClinicalAreas = new ObservableCollection<ClinicalAreaListItemViewModel>();

        public SelectClinicalAreaPageViewModel( INavigationService navigationService )
        {
            _navigationService = navigationService;
        }

        public ObservableCollection<ClinicalAreaListItemViewModel> AvailableClinicalAreas
        {
            get { return _availableClinicalAreas; }
            set { SetProperty(ref _availableClinicalAreas, value); }
        }

        public void OnNavigatedFrom( NavigationParameters parameters )
        {
        }

        public void OnNavigatedTo( NavigationParameters parameters )
        {
        }

        public async void OnNavigatingTo( NavigationParameters parameters )
        {
            var appUserData = parameters["userData"] as AppUserData;

            if ( appUserData == null )
            {
                return;
            }

            IAzureCRUDService service = new RemoteAzureCRUDService(StaticAzureService.MobileServiceClient);
            var query = service.CreateQuery<ClinicalArea>();
            var parameterisedQuery = query.WithParameters(new Dictionary<string, string> {{"select", "name,id"}});
            var clinicalAreas = await service.ExecuteQuery(parameterisedQuery);

            foreach ( var clinicalArea in clinicalAreas )
            {
                AvailableClinicalAreas.Add(new ClinicalAreaListItemViewModel(_navigationService, appUserData) {Name = clinicalArea.Name, Id = clinicalArea.Id});
            }
        }
    }
}