using PCLStorage;
using Prism.Mvvm;
using Prism.Navigation;
using SepsisTrust.Model.Storage;
using SepsisTrust.Model.User;

namespace SepsisTrust.ViewModels
{
    public class LoadingPageViewModel : BindableBase, INavigationAware
    {
        private readonly IFileStreamRetriever _fileStreamRetriever;
        private readonly IJsonObjectStreamReader _jsonObjectStreamReader;
        private readonly INavigationService _navigationService;

        public LoadingPageViewModel( INavigationService navigationService, IFileStreamRetriever fileStreamRetriever, IJsonObjectStreamReader jsonObjectStreamReader )
        {
            _navigationService = navigationService;
            _fileStreamRetriever = fileStreamRetriever;
            _jsonObjectStreamReader = jsonObjectStreamReader;
        }

        public void OnNavigatedFrom( NavigationParameters parameters )
        {
        }

        public void OnNavigatedTo( NavigationParameters parameters )
        {
        }

        public async void OnNavigatingTo( NavigationParameters parameters )
        {
            // Attempt to load the user data from the store.
            var userDataFileExists = await _fileStreamRetriever.CanObtainStreamFromPathAsync(StaticUserDataStore.UserFileName);
            if ( userDataFileExists )
            {
                // Attempt to load the user data.
                var stream = await _fileStreamRetriever.ObtainFileStreamAsync(StaticUserDataStore.UserFileName, false, FileAccess.Read);
                var appUserData = _jsonObjectStreamReader.Read<AppUserData>(stream);

                // Navigate to the homepage.
                var navigationParameters = new NavigationParameters();
                navigationParameters.Add("userData", appUserData);
                await _navigationService.NavigateAsync("/GNav/MainPage");
            }
            else
            {
                // Load the enter user details page.
                await _navigationService.NavigateAsync("/GNav/MainPage/EditUser");
            }
        }
    }
}