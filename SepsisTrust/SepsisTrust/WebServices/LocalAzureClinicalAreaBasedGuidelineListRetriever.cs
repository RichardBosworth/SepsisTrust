using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureData;
using Guidelines.IO;
using SepsisTrust.Model.Azure;
using Guideline = Guidelines.Model.Guideline;

namespace SepsisTrust.WebServices
{
    /// <summary>
    /// Provides functionality to retrieve a list of guidelines from Azure (using Offline Sync).
    /// </summary>
    public class LocalAzureClinicalAreaBasedGuidelineListRetriever : IGuidelineListRetriever
    {
        private readonly string _clinicalAreaId;
        private readonly bool _syncData;

        public LocalAzureClinicalAreaBasedGuidelineListRetriever(string clinicalAreaId, bool syncData)
        {
            _clinicalAreaId = clinicalAreaId;
            _syncData = syncData;
        }

        public async Task<IEnumerable<Guideline>> RetrieveAvailableGuidelinesAsync()
        {
            // Get the guidelines that match the clinical area.
            ISyncronizedAzureCrudService azureCrudService = new LocalAzureCRUDService(StaticAzureService.MobileServiceClient);

            // Sync data if required.
            if (_syncData)
            {
                await azureCrudService.SyncronizeTableAsync<Model.Azure.Guideline>();
                await azureCrudService.SyncronizeTableAsync<ClinicalArea>();
            }

            // Execute the guidelines query.
            var guidelinesOfAreaQuery = azureCrudService.CreateQuery<Model.Azure.Guideline>()
                .Where(guideline => (guideline.ClinicalAreaId == _clinicalAreaId) && (guideline.GuidelineContent != null))
                .Select(
                    guideline =>
                        new Guideline
                        {
                            Title= guideline.Title,
                            Identifier = guideline.GuidelineIdentifier,
                            Description = guideline.Description
                        });

            var guidelines = await azureCrudService.ExecuteQuery(guidelinesOfAreaQuery);
            return guidelines;
        }
    }
}