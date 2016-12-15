using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Newtonsoft.Json.Linq;

namespace AzureData
{
    /// <summary>
    ///     Provides functionality to obtain and syncronise (storing locally on a device) the data in an Azure service.
    /// </summary>
    public class LocalAzureCRUDService : ISyncronisedAzureCrudService
    {
        private readonly IMobileServiceClient _mobileServiceClient;

        public LocalAzureCRUDService(IMobileServiceClient mobileServiceClient)
        {
            _mobileServiceClient = mobileServiceClient;
        }


        public async Task<List<T>> GetAll<T>()
        {
            InitializedCheck();

            var table = _mobileServiceClient.GetSyncTable<T>();
            return await table.ToListAsync();
        }

        public async Task<T> GetOne<T>(string identifier)
        {
            return await _mobileServiceClient.GetSyncTable<T>().LookupAsync(identifier);
        }

        public async Task Create<T>(T data)
        {
            await _mobileServiceClient.GetSyncTable<T>().InsertAsync(data);
        }

        public async Task Update<T>(T data)
        {
            await _mobileServiceClient.GetSyncTable<T>().UpdateAsync(data);
        }

        public async Task Delete<T>(string identifier)
        {
            await _mobileServiceClient.GetSyncTable<T>().DeleteAsync(new JObject {{"Id", identifier}});
        }

        public IMobileServiceTableQuery<T> CreateQuery<T>()
        {
            return _mobileServiceClient.GetSyncTable<T>().CreateQuery();
        }

        public async Task<List<T>> ExecuteQuery<T>(IMobileServiceTableQuery<T> query)
        {
            return await query.ToListAsync();
        }

        public async Task InitiateSyncTablesAsync(params Action<MobileServiceSQLiteStore>[] tableDefinitionActions)
        {
            // Check if the sync context is already initialized.
            if (_mobileServiceClient.SyncContext.IsInitialized)
                return;

            // Create the SQL data store.
            var localStore = new MobileServiceSQLiteStore("local_database.sq3");

            // Run the table generation routines.
            foreach (var definitionAction in tableDefinitionActions)
            {
                definitionAction.Invoke(localStore);
            }

            // Initialize the sync context.
            await _mobileServiceClient.SyncContext.InitializeAsync(localStore);
        }

        public async Task SyncronizeTableAsync<T>()
        {
            await SyncronizeTableAsync<T>($"full_{typeof(T).Name.ToLowerInvariant()}");
        }

        public async Task SyncronizeTableAsync<T>(string queryId)
        {
            await SyncronizeTableAsync(queryId, _mobileServiceClient.GetSyncTable<T>().CreateQuery());
        }

        public async Task SyncronizeTableAsync<T>(string queryId, IMobileServiceTableQuery<T> tableQuery)
        {
            await _mobileServiceClient.GetSyncTable<T>()
                .PullAsync(queryId, tableQuery, true, CancellationToken.None);
        }

        private void InitializedCheck()
        {
            // If not initialized, state that need to initailize first.
            var initialized = _mobileServiceClient.SyncContext.IsInitialized;
            if (!initialized)
                throw new Exception("Need to initialize sync context first.");
        }
    }
}