using System.Threading.Tasks;
using System.Xml;
using Guidelines.Model;
using PCLStorage;
using SepsisTrust.Model.Retrieval;

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
        Task<Guideline> RetrieveGuidelineAsync(string identifier);
    }

    public class LocalXmlGuidelineRetriever : IGuidelineRetriever
    {
        public async Task<Guideline> RetrieveGuidelineAsync( string identifier )
        {
            var file = await FileSystem.Current.GetFileFromPathAsync($"{identifier}.xml");
            var xmlText = await file.ReadAllTextAsync();
            return await new GuidelineXmlParser().ParseGuidelineXmlAsync(xmlText);
        }
    }
}