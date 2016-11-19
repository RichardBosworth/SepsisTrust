using Humanizer;
using Xamarin.Forms;

namespace SepsisTrust.Behaviours
{
    public class CapitaliseTextBehaviour : Behavior<Label>
    {
        protected override void OnAttachedTo( Label bindable )
        {
            bindable.Text = bindable.Text?.Transform(To.UpperCase);
            base.OnAttachedTo(bindable);
        }
    }
}