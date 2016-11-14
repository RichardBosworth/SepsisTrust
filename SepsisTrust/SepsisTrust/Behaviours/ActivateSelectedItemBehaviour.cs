using SepsisTrust.ViewModels;
using Xamarin.Forms;

namespace SepsisTrust.Behaviours
{
    public class ActivateSelectedItemBehaviour : Behavior<ListView>
    {
        protected override void OnAttachedTo( ListView bindable )
        {
            bindable.ItemSelected += BindableOnItemSelected;
            base.OnAttachedTo(bindable);
        }

        private void BindableOnItemSelected( object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs )
        {
            var activityViewModel = selectedItemChangedEventArgs.SelectedItem as BlockActivityViewModel;
            if ( activityViewModel != null )
            {
                activityViewModel.Activated = !activityViewModel.Activated;
            }
        }
    }
}