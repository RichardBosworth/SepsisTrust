using Microsoft.WindowsAzure.MobileServices;

namespace AzureData
{
    public static class StaticAzureService
    {
        public const string AppServiceUrl = "http://sepsis.azurewebsites.net";

        /// <summary>
        ///     Gets a value indicating whether the service is initialized.
        /// </summary>
        public static bool IsInitialized { get; private set; }

        public static IMobileServiceClient MobileServiceClient { get; private set; }

        public static void Initialize( string appServiceUrl = AppServiceUrl )
        {
            if ( !string.IsNullOrEmpty(appServiceUrl) )
            {
                MobileServiceClient = new MobileServiceClient(appServiceUrl);
                IsInitialized = true;
            }
        }
    }
}