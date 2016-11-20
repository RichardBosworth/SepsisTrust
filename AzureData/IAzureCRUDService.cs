using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace AzureData
{
    /// <summary>
    ///     Provides CRUD functionality for an Azure App Service.
    /// </summary>
    public interface IAzureCRUDService
    {
        /// <summary>
        ///     Retrieves the full list of all data items in a the table of this data type.
        /// </summary>
        /// <returns>Returns an instance of a list containing the data.</returns>
        Task<List<T>> GetAll<T>( );

        /// <summary>
        ///     Retrieves a single entity with the specified identifier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="identifier">The string identifier of the entity to be retrieved.</param>
        /// <returns>Returns a single entity - that has the identifier specified.</returns>
        Task<T> GetOne<T>( string identifier );

        /// <summary>
        ///     Inserts a new instance of the specified data type into the service.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The instance of the data that should be inserted into the service.</param>
        /// <returns></returns>
        Task Create<T>( T data );

        /// <summary>
        ///     Updates the data entity with the specified identifier with the data specified in the parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data that the specified entity will be updated with.</param>
        /// <returns></returns>
        Task Update<T>( T data );

        /// <summary>
        ///     Deletes the specified entity from the service.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="identifier">The identifier of the entity that will be deleted.</param>
        /// <returns></returns>
        Task Delete<T>( string identifier );

        /// <summary>
        ///     Generates a query that can be customised to provide a narrower focus of data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>
        ///     Returns the query that can then be customised. This should be executed with <see cref="ExecuteQuery{T}" />
        /// </returns>
        IMobileServiceTableQuery<T> CreateQuery<T>( );

        /// <summary>
        ///     Executes a <see cref="IMobileServiceTableQuery{T}" /> that has been created in the <see cref="CreateQuery{T}" />
        ///     method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The <see cref="IMobileServiceTableQuery{T}" /> that will be executed.</param>
        /// <returns>Returns a list of the data that is returned from the specified query.</returns>
        Task<List<T>> ExecuteQuery<T>( IMobileServiceTable<T> query );
    }
}