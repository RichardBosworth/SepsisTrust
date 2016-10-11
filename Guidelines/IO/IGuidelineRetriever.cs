using System.Threading.Tasks;
using Guidelines.Model;

namespace Guidelines.IO
{
    /// <summary>
    /// Provides functionality to retrieve <see cref="Guideline"/> data.
    /// </summary>
    public interface IGuidelineRetriever
    {
        /// <summary>
        /// Retrieves the guideline with the specified identifier.
        /// </summary>
        /// <param name="identifier">The identifier of the guideline that should be retrieved.</param>
        /// <returns>Returns the guideline with the specified identifier.</returns>
        Task<Guideline> RetrieveGuideline(string identifier);
    }
}