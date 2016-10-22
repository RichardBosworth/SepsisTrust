using Prism.Commands;
using Prism.Mvvm;
using SepsisTrust.Model;
using Xamarin.Forms;

namespace SepsisTrust.ViewModels
{
    public class EditPatientCharacteristicPageViewModel : BindableBase
    {
        private string _characteristicName;

        private Patient _patient;

        private string _proceedButtonText;

        public EditPatientCharacteristicPageViewModel(DelegateCommand proceedCommand, View controlsView)
        {
            ProceedCommand = proceedCommand;
            ControlsView = controlsView;
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

        public View ControlsView { get; set; }

        public DelegateCommand ProceedCommand { get; set; }
    }
}