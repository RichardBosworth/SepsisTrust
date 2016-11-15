using Xamarin.Forms;

namespace SepsisTrust.Actions
{
    public class FadeTriggerAction : TriggerAction<VisualElement>
    {
        public FadeTriggerAction() { }

        public double DesiredOpacity { set; get; }

        protected override async void Invoke(VisualElement visual)
        {
            visual.FadeTo(DesiredOpacity, 250, Easing.SinInOut);
        }
    }
}