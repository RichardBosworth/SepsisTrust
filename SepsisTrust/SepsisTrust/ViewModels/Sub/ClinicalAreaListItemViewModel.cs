using AzureData;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SepsisTrust.Model;
using SepsisTrust.Model.User;

namespace SepsisTrust.ViewModels.Sub
{
    public class ClinicalAreaListItemViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private string _id;
        private string _name;
        private DelegateCommand _selectAsClinicalAreaCommand;

        public ClinicalAreaListItemViewModel( INavigationService navigationService, AppUserData currentAppUserData )
        {
            _navigationService = navigationService;
            SelectAsClinicalAreaCommand = new DelegateCommand(( ) => SelectClinicalArea(currentAppUserData));
        }

        public DelegateCommand SelectAsClinicalAreaCommand
        {
            get { return _selectAsClinicalAreaCommand; }
            set { SetProperty(ref _selectAsClinicalAreaCommand, value); }
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public string Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private async void SelectClinicalArea( AppUserData currentAppUserData )
        {
            currentAppUserData.ClinicalArea = new ClinicalArea
                                              {
                                                  Id = Id,
                                                  Name = Name
                                              };
            NavigationParameters navigationParameters = new NavigationParameters();
            navigationParameters.Add("userData", currentAppUserData);
            await _navigationService.GoBackAsync(navigationParameters);
        }
    }
}