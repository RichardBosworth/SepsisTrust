using Guidelines.Model;
using Prism.Commands;
using Prism.Mvvm;

namespace SepsisTrust.ViewModels
{
    public class BlockActivityViewModel : BindableBase
    {
        private readonly GuidelinePageViewModel _guidelinePageViewModel;
        private DelegateCommand _activateCommand;
        private bool _activated;
        private string _descriptiveText;

        private string _iconName;
        private string _title;

        public BlockActivityViewModel( BlockActivityData blockActivityData, GuidelinePageViewModel guidelinePageViewModel )
        {
            _guidelinePageViewModel = guidelinePageViewModel;
            BlockActivityData = blockActivityData;
            Title = blockActivityData.Title;
            DescriptiveText = blockActivityData.DescriptiveText;
            Activated = blockActivityData.Activated;
            EntityIconName = blockActivityData.EntityIconName;

            ActivateCommand = new DelegateCommand(( ) => Activated = true);
        }

        public BlockActivityData BlockActivityData { get; set; }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public string DescriptiveText
        {
            get { return _descriptiveText; }
            set { SetProperty(ref _descriptiveText, value); }
        }

        public bool Activated
        {
            get { return _activated; }
            set
            {
                SetProperty(ref _activated, value);
                BlockActivityData.Activated = value;
                _guidelinePageViewModel.UpdateCanProgress();
                ActivationCommandText = value ? "Not Done" : "Done";
            }
        }

        public string EntityIconName
        {
            get { return _iconName; }
            set { SetProperty(ref _iconName, value); }
        }

        public DelegateCommand ActivateCommand
        {
            get { return _activateCommand; }
            set { SetProperty(ref _activateCommand, value); }
        }

        private string _activationCommandText = "Done";

        public string ActivationCommandText
        {
            get { return _activationCommandText; }
            set { SetProperty(ref _activationCommandText, value); }
        }
    }
}