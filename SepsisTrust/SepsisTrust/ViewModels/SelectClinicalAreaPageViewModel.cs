using System.Collections.ObjectModel;
using Prism.Mvvm;
using SepsisTrust.Model;

namespace SepsisTrust.ViewModels
{
    public class SelectClinicalAreaPageViewModel : BindableBase
    {
        private ObservableCollection<ClinicalArea> _availableClinicalAreas = new ObservableCollection<ClinicalArea>();

        public SelectClinicalAreaPageViewModel()
        {
            AvailableClinicalAreas.Add(new ClinicalArea
            {
                Name = "Emergency Department", RelevantGuidelineIdentifiers = {"ED-ADULT"}
            });
        }

        public ObservableCollection<ClinicalArea> AvailableClinicalAreas
        {
            get { return _availableClinicalAreas; }
            set { SetProperty(ref _availableClinicalAreas, value); }
        }
    }
}