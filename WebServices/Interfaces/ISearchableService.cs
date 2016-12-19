using System.Dynamic;
using System.Threading.Tasks;

namespace WebServices.Interfaces
{
    /// <summary>
    /// Provides functionality to search a web service.
    /// </summary>
    public interface ISearchableService
    {
        /// <summary>
        /// Invokes a search on the specified service, and returns the data as T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        Task<T> SearchServiceAsync<T>(string searchTerm);
    }
}