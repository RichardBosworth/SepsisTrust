using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;

namespace SepsisTrust.ViewModels
{
    public class StartGuidelinePageViewModel : BindableBase, INavigationAware
    {
        public StartGuidelinePageViewModel(INavigationService navigationService)
        {

        }

        private string _guidelineName;

        public string GuidelineName
        {
            get { return _guidelineName; }
            set { SetProperty(ref _guidelineName, value); }
        }

        public DelegateCommand StartGuidelineCommand { get; set; }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
