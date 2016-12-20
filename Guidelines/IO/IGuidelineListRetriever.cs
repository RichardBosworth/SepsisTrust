using System.Collections.Generic;
using System.Threading.Tasks;
using Guidelines.Model;

namespace Guidelines.IO
{
    /// <summary>
    /// Provides functionality to obtain a list of available guidelines.
    /// </summary>
    public interface IGuidelineListRetriever
    {
        /// <summary>
        /// Retrieves an enumeration of available guidelines in an asyncronous manner.
        /// </summary>
        /// <returns>Returns a <see cref="IEnumerable{T}"/> of available guidelines.</returns>
        Task<IEnumerable<Guideline>> RetrieveAvailableGuidelinesAsync();
    }
}