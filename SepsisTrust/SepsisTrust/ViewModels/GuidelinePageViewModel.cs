using System.Collections.ObjectModel;
using Guidelines.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SepsisTrust.GuidelineUI;
using SepsisTrust.Model.Navigation;
using Xamarin.Forms;

namespace SepsisTrust.ViewModels
{
    public class GuidelinePageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private Block _block;
        private string _proceedButtonText;
        private DelegateCommand _proceedCommand;
        private View _template;

        public GuidelinePageViewModel( INavigationService navigationService )
        {
            _navigationService = navigationService;
        }

        public ObservableCollection<BlockActivityViewModel> BlockActivityViewModels { get; set; }

        public Block Block
        {
            get { return _block; }
            set { SetProperty(ref _block, value); }
        }

        public IGuidelineRunner GuidelineRunner { get; set; }

        public View Template
        {
            get { return _template; }
            set { SetProperty(ref _template, value); }
        }

        public string ProceedButtonText
        {
            get { return _proceedButtonText; }
            set { SetProperty(ref _proceedButtonText, value); }
        }

        public DelegateCommand ProceedCommand
        {
            get { return _proceedCommand; }
            set { SetProperty(ref _proceedCommand, value); }
        }

        public void OnNavigatedFrom( NavigationParameters parameters )
        {
        }


        public void OnNavigatedTo( NavigationParameters parameters )
        {
            // If the page is not currently associated with anything, then load the data.
            // Note that if the user presses the back button, then the page will still have the data associated with it.
            if ( ( Block == null ) || ( GuidelineRunner == null ) )
            {
                var navigationModel = new GuidelinePageNavigationModel(parameters);
                Block = navigationModel.CurrentBlock;
                GenerateBlockActivityViewModels();
                GuidelineRunner = navigationModel.CurrentGuidelineRunner;
            }
            else
            {
                GuidelineRunner.SetCurrentBlock(Block);
            }

            // Determine the appropriate content template for the block.
            IGuidelineUITemplateSelector templateSelector = new ReflectiveGuidelineUITemplateSelector();
            Template = templateSelector.SelectUIForBlock(Block);
            Template.BindingContext = this;

            // Generate the proceed button text.
            if ( Block.Links.Count > 0 )
            {
                ProceedButtonText = "NEXT";
                ProceedCommand = new DelegateCommand(Proceed);
            }
            else
            {
                ProceedButtonText = "FINISH";
                ProceedCommand = new DelegateCommand(Finish);
            }
        }

        public void OnNavigatingTo( NavigationParameters parameters )
        {
            
        }

        private void GenerateBlockActivityViewModels( )
        {
            BlockActivityViewModels = new ObservableCollection<BlockActivityViewModel>();
            foreach ( var activity in Block.BlockActivities )
            {
                BlockActivityViewModels.Add(new BlockActivityViewModel(activity));
            }
        }


        private void Finish( )
        {
            _navigationService.NavigateAsync("/MainPage");
        }

        private async void Proceed( )
        {
            // Ensure that the guideline runner has this block as the current block.
            GuidelineRunner.SetCurrentBlock(Block);

            // Move to the next block in the guideline.
            var nextBlock = GuidelineRunner.MoveForwards();

            // Navigate to a page with that next block.
            var navigationModel = new GuidelinePageNavigationModel
                                  {
                                      CurrentBlock = nextBlock,
                                      CurrentGuidelineRunner = GuidelineRunner
                                  };
            await _navigationService.NavigateAsync("GuidelinePage", navigationModel.ToNavigationParameters());
        }
    }
}