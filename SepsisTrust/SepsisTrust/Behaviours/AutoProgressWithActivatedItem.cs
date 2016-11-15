using Guidelines.Model;
using SepsisTrust.ViewModels;
using Xamarin.Forms;

namespace SepsisTrust.Behaviours
{
    public class AutoProgressWithSelectedItemBehaviour : Behavior<ListView>
    {
        private ListView _bindable;

        protected override void OnAttachedTo( ListView bindable )
        {
            _bindable = bindable;
            bindable.ItemSelected += BindableOnItemSelected;
            base.OnAttachedTo(bindable);
        }

        private void BindableOnItemSelected( object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs )
        {
            if ( _bindable.SelectedItem != null )
            {
                var activityViewModel = selectedItemChangedEventArgs.SelectedItem as BlockActivityViewModel;
                var viewModel = _bindable.BindingContext as GuidelinePageViewModel;
                foreach ( var blockActivity in viewModel.Block.BlockActivities )
                {
                    blockActivity.Activated = false;
                }

                activityViewModel.Activated = true;
                _bindable.SelectedItem = null;

                viewModel.ProceedCommand.Execute();
            }
        }
    }
}