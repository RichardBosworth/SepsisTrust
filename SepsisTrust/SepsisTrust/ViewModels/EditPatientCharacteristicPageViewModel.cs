using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SepsisTrust.Model;
using Xamarin.Forms;

namespace SepsisTrust.ViewModels
{
    public class EditPatientCharacteristicPageViewModel : BindableBase, INavigationAware
    {
        private string _characteristicName;

        private Patient _patient;

        private string _proceedButtonText;

        public EditPatientCharacteristicPageViewModel()
        {
        }

        public string CharacteristicName
        {
            get { return _characteristicName; }
            set { SetProperty(ref _characteristicName, value); }
        }

        public string ProceedButtonText
        {
            get { return _proceedButtonText; }
            set { SetProperty(ref _proceedButtonText, value); }
        }

        public Patient Patient
        {
            get { return _patient; }
            set { SetProperty(ref _patient, value); }
        }

        private View _controlsView;

        public View ControlsView
        {
            get { return _controlsView; }
            set { SetProperty(ref _controlsView, value); }
        }

        private DelegateCommand _proceedCommand;

        public DelegateCommand ProceedCommand
        {
            get { return _proceedCommand; }
            set { SetProperty(ref _proceedCommand, value); }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("patient")) Patient = parameters["patient"] as Patient;
            if (parameters.ContainsKey("view")) ControlsView = parameters["view"] as View;
            if (parameters.ContainsKey("characteristicName")) CharacteristicName = parameters["characteristicName"] as string;
            if (parameters.ContainsKey("proceedCommand")) ProceedCommand = parameters["proceedCommand"] as DelegateCommand;
            ProceedButtonText = "NEXT";
        }
    }
}