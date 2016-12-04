using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace AzureData
{
    /// <summary>
    /// Provides functionality to obtain and syncronise (storing locally on a device) the data in an Azure service.
    /// </summary>
    public class LocalAzureCRUDService : IAzureCRUDService
    {
        private readonly MobileServiceClient _mobileServiceClient;

        public LocalAzureCRUDService(MobileServiceClient mobileServiceClient)
        {
            _mobileServiceClient = mobileServiceClient;
        }


        public Task<List<T>> GetAll<T>()
        {
            throw new System.NotImplementedException();
        }

        public Task<T> GetOne<T>(string identifier)
        {
            throw new System.NotImplementedException();
        }

        public Task Create<T>(T data)
        {
            throw new System.NotImplementedException();
        }

        public Task Update<T>(T data)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete<T>(string identifier)
        {
            throw new System.NotImplementedException();
        }

        public IMobileServiceTableQuery<T> CreateQuery<T>()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<T>> ExecuteQuery<T>(IMobileServiceTableQuery<T> query)
        {
            throw new System.NotImplementedException();
        }
    }
}