using Xamarin.Forms;

namespace SepsisTrust.Behaviours
{
    public class NonSelectableListBehaviour : Behavior<ListView>
    {
        protected override void OnAttachedTo( ListView bindable )
        {
            bindable.ItemSelected += ( sender, args ) => bindable.SelectedItem = null;
            base.OnAttachedTo(bindable);
        }
    }
}