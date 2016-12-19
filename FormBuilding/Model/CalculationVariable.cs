namespace FormBuilding.Model
{
    /// <summary>
    /// Represents a variable in a calculation.
    /// </summary>
    public class CalculationVariable
    {
        /// <summary>
        /// Gets or sets the name of the variable.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value of the variable.
        /// </summary>
        public double Value { get; set; }
    }
}