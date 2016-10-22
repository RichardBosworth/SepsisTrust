using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SepsisTrust.Extensions;
using SepsisTrust.Model;
using SepsisTrust.Views;
using Xamarin.Forms;

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
            var navigationParameters = new NavigationParameters();
            var patient = new Patient {Gender = true};
            var switchView = new Switch();
            switchView.SetBinding(Switch.IsToggledProperty, new Binding("Gender", BindingMode.TwoWay));
            switchView.BindingContext = patient;
            navigationParameters.ForPatientCharacteristicPage(patient, "Is this patient male?", switchView, new DelegateCommand(() => _navigationService.NavigateAsync("StartGuidelinePage")));
            await _navigationService.NavigateAsync("CharacteristicPage", navigationParameters);
        }
    }
}