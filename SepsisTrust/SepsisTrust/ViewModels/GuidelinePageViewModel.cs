using System;
using System.Collections.Generic;
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

        private string _blockTitle;

        private bool _canProgress = true;

        private string _instructionalText;
        private string _proceedButtonText;
        private DelegateCommand _proceedCommand;
        private View _template;

        public GuidelinePageViewModel( INavigationService navigationService )
        {
            _navigationService = navigationService;
        }

        public bool CanProgress
        {
            get { return _canProgress; }
            set { SetProperty(ref _canProgress, value); }
        }

        public string InstructionalText
        {
            get { return _instructionalText; }
            set { SetProperty(ref _instructionalText, value); }
        }

        public ObservableCollection<BlockActivityViewModel> BlockActivityViewModels { get; set; }

        public Block Block
        {
            get { return _block; }
            set { SetProperty(ref _block, value); }
        }

        public string BlockTitle
        {
            get { return _blockTitle; }
            set { SetProperty(ref _blockTitle, value); }
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
        }

        public void OnNavigatingTo( NavigationParameters parameters )
        {
            Initiate(parameters);
        }

        private void Initiate( NavigationParameters parameters )
        {
            // If the page is not currently associated with anything, then load the data.
            // Note that if the user presses the back button, then the page will still have the data associated with it.
            if ( ( Block == null ) || ( GuidelineRunner == null ) )
            {
                var navigationModel = new GuidelinePageNavigationModel(parameters);
                Block = navigationModel.CurrentBlock;
                BlockTitle = Block.Title;
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

            // Determine if action block, and therefore whether can progress.
            if ( Block is ActionBlock )
            {
                CanProgress = false;
            }

            // Generate the proceed button text.
            var hasLinks = Block.Links.Count > 0;
            ProceedButtonText = hasLinks ? "NEXT" : "FINISH";
            ProceedCommand = hasLinks ? new DelegateCommand(Proceed).ObservesCanExecute(o => CanProgress) : new DelegateCommand(Finish).ObservesCanExecute(o => CanProgress);
            DetermineInstructionalText();
        }

        private void DetermineInstructionalText( )
        {
            if ( Block is AssessmentBlock )
            {
                var assessmentBlock = Block as AssessmentBlock;
                switch ( assessmentBlock.AssessmentType )
                {
                    case AssessmentType.Question:
                        InstructionalText = "Select the relevant answer.";
                        break;
                    case AssessmentType.Checklist:
                        InstructionalText = "Select all that apply.";
                        break;
                }
            }
            if ( Block is ActionBlock )
            {
                InstructionalText = "Complete the actions by tapping them.";
            }
        }

        private void GenerateBlockActivityViewModels( )
        {
            BlockActivityViewModels = new ObservableCollection<BlockActivityViewModel>();
            for ( var index = 0; index < Block.BlockActivities.Count; index++ )
            {
                var activity = Block.BlockActivities[index];
                var blockActivityViewModel = Block is ActionBlock ? new ActionBlockActivityViewModel(activity, this) {NumberColor = GenerateRandomColor(), IndexText = (index+1).ToString()} : new BlockActivityViewModel(activity, this);
                BlockActivityViewModels.Add(blockActivityViewModel);
            }
        }

        private Color GenerateRandomColor( )
        {
            var red = (Color) Application.Current.Resources["RedBaseColor"];
            var yellow = (Color) Application.Current.Resources["YellowBaseColor"];
            var orange = (Color) Application.Current.Resources["OrangeBaseColor"];
            var grey = (Color) Application.Current.Resources["GreyBaseColor"];
            var colours = new List<Color> {red, yellow, orange, grey};
            Random random = new Random();
            var next = random.Next(0, 3);
            return colours[next];
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

        public void UpdateCanProgress( )
        {
            if ( Block is ActionBlock )
            {
                CanProgress = Block.BlockActivities.TrueForAll(data => data.Activated);
            }
            else
            {
                CanProgress = true;
            }
        }
    }
}