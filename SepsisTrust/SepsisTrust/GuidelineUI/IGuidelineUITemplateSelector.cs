using Guidelines.Model;
using Xamarin.Forms;

namespace SepsisTrust.GuidelineUI
{
    public interface IGuidelineUITemplateSelector
    {
        View SelectUIForBlock(Block block);
    }
}