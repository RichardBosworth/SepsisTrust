using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace AzureData
{
    public class RemoteAzureCRUDService : IAzureCRUDService
    {
        private readonly IMobileServiceClient _mobileServiceClient;

        public RemoteAzureCRUDService( IMobileServiceClient mobileServiceClient )
        {
            _mobileServiceClient = mobileServiceClient;
        }

        public async Task<List<T>> GetAll<T>( ) => await _mobileServiceClient?.GetTable<T>()?.ToListAsync();

        public async Task<T> GetOne<T>( string identifier ) => await _mobileServiceClient?.GetTable<T>()?.LookupAsync(identifier);

        public async Task Create<T>( T data ) => await _mobileServiceClient?.GetTable<T>()?.InsertAsync(data);

        public async Task Update<T>( T data ) => await _mobileServiceClient?.GetTable<T>()?.UpdateAsync(data);

        public async Task Delete<T>( string identifier )
        {
            var table = _mobileServiceClient.GetTable<T>();
            var lookupAsync = table?.LookupAsync(identifier);
            if ( lookupAsync != null )
            {
                var entity = await lookupAsync;
                if ( entity != null )
                {
                    await table.DeleteAsync(entity);
                }
            }
        }

        public IMobileServiceTableQuery<T> CreateQuery<T>( ) => _mobileServiceClient?.GetTable<T>()?.CreateQuery();

        public async Task<List<T>> ExecuteQuery<T>( IMobileServiceTable<T> query ) => await query?.ToListAsync();
    }
}