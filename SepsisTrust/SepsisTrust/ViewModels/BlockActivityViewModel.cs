using Guidelines.Model;
using Prism.Mvvm;

namespace SepsisTrust.ViewModels
{
    public class BlockActivityViewModel : BindableBase
    {
        private bool _activated;
        private string _descriptiveText;
        private string _title;

        public BlockActivityViewModel( BlockActivityData blockActivityData )
        {
            BlockActivityData = blockActivityData;
            Title = blockActivityData.Title;
            DescriptiveText = blockActivityData.DescriptiveText;
            Activated = blockActivityData.Activated;
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
            }
        }
    }
}