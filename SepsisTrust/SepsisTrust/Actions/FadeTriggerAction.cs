using Xamarin.Forms;

namespace SepsisTrust.Actions
{
    public class FadeTriggerAction : TriggerAction<VisualElement>
    {
        public double DesiredOpacity { set; get; }
        public int Time { get; set; } = 250;

        protected override async void Invoke( VisualElement visual )
        {
            visual.FadeTo(DesiredOpacity, (uint) Time, Easing.CubicInOut);
        }
    }
}