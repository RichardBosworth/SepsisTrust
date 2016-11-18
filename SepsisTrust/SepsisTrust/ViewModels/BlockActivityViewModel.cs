using Guidelines.Model;
using Prism.Commands;
using Prism.Mvvm;
using Xamarin.Forms;

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
    }

    public class ActionBlockActivityViewModel : BlockActivityViewModel
    {
        public ActionBlockActivityViewModel( BlockActivityData blockActivityData, GuidelinePageViewModel guidelinePageViewModel ) : base(blockActivityData, guidelinePageViewModel)
        {
        }

        private string _indexText;

        public string IndexText
        {
            get { return _indexText; }
            set { SetProperty(ref _indexText, value); }
        }
        
        private Color _numberColor;

        public Color NumberColor
        {
            get { return _numberColor; }
            set { SetProperty(ref _numberColor, value); }
        }
    }
}