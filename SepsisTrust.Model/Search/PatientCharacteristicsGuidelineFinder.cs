using Guidelines.Model;

namespace SepsisTrust.Model.Search
{
    public class PatientCharacteristicsGuidelineFinder : IGuidelineFinder
    {
        /// <inheritdoc />
        public Guideline FindMostRelevantGuideline(Patient patientDetails)
        {
            return TemporaryGuidelineGenerator.Generate();
        }
    }
}