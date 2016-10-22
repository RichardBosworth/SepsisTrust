using Prism.Navigation;

namespace SepsisTrust.Model.Navigation
{
    /// <summary>
    /// Provides a strongly typed model for the mandatory data for a ViewModel.
    /// </summary>
    public interface INavigationModel
    {
        NavigationParameters ToNavigationParameters();
    }
}