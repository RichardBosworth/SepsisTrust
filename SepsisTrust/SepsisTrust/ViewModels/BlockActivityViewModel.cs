using Guidelines.Model;
using Prism.Mvvm;

namespace SepsisTrust.ViewModels
{
    public class BlockActivityViewModel : BindableBase
    {
        public BlockActivityData BlockActivityData { get; set; }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _descriptiveText;

        public string DescriptiveText
        {
            get { return _descriptiveText; }
            set { SetProperty(ref _descriptiveText, value); }
        }

        private bool _activated;

        public BlockActivityViewModel( BlockActivityData blockActivityData )
        {
            BlockActivityData = blockActivityData;
            Title = blockActivityData.Title;
            DescriptiveText = blockActivityData.DescriptiveText;
            Activated = blockActivityData.Activated;
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