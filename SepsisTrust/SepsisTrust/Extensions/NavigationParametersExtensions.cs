using Prism.Commands;
using Prism.Navigation;
using SepsisTrust.Model;
using Xamarin.Forms;

namespace SepsisTrust.Extensions
{
    public static class NavigationParametersExtensions
    {
        public static void ForPatientCharacteristicPage(this NavigationParameters navigationParameters, Patient patient, string characteristicName, View view, DelegateCommand command)
        {
            navigationParameters.Add("patient", patient);
            navigationParameters.Add("view", view);
            navigationParameters.Add("characteristicName", characteristicName);
            navigationParameters.Add("proceedCommand", command);
        }
    }
}