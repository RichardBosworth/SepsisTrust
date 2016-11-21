using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace AzureData
{
    public class LocalAzureCRUDService : IAzureCRUDService
    {
        private MobileServiceClient _mobileServiceClient;

        public LocalAzureCRUDService( MobileServiceClient mobileServiceClient )
        {
            _mobileServiceClient = mobileServiceClient;
        }

        public Task<List<T>> GetAll<T>( )
        {
        }

        public Task<T> GetOne<T>( string identifier )
        {
            throw new NotImplementedException();
        }

        public Task Create<T>( T data )
        {
            throw new NotImplementedException();
        }

        public Task Update<T>( T data )
        {
            throw new NotImplementedException();
        }

        public Task Delete<T>( string identifier )
        {
            throw new NotImplementedException();
        }

        public IMobileServiceTableQuery<T> CreateQuery<T>( )
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> ExecuteQuery<T>( IMobileServiceTableQuery<T> query )
        {
            throw new NotImplementedException();
        }
    }
}