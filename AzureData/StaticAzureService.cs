﻿using System;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;

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

        public static async void EnableLocalStorage(params Action<MobileServiceSQLiteStore>[] tableDefinitionActions)
        {
            // If not initialized, then throw an exception.
            if (!IsInitialized)
            {
                throw new Exception(
                    "The Static Azure Service has not been initialized yet, so local storage cannot be enabled.");
            }

            // Initialize the local storage for this.
            await new LocalAzureCRUDService(MobileServiceClient).InitiateSyncTablesAsync(tableDefinitionActions);
        }
    }
}