using Guidelines.Model;

namespace SepsisTrust.Model.Search
{
    /// <summary>
    /// Provides functionality to find a guideline that is relevant to a specified patient.
    /// </summary>
    public interface IGuidelineFinder
    {
        /// <summary>
        /// Finds the guideline that is most relevant to the specified patient.
        /// </summary>
        /// <param name="patientDetails">The details of the patient.</param>
        /// <returns>Returns the guideline that is found to be most relevant to the specified patient.</returns>
        Guideline FindMostRelevantGuideline(Patient patientDetails);
    }
}