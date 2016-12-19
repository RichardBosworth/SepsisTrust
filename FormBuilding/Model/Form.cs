using System.Collections.Generic;

namespace FormBuilding.Model
{
    /// <summary>
    /// Provides properties and methods related to all forms.
    /// </summary>
    public class Form
    {
        public string Title { get; set; }
        public string Summary { get; set; }
    }

    /// <summary>
    /// Provides properties and methods relevant to in-app calculation functionality.
    /// </summary>
    public class CalculatorForm : Form
    {
        /// <summary>
        /// Gets the list of variables in the calculation.
        /// </summary>
        public List<CalculationVariable> Variables { get; private set; } = new List<CalculationVariable>();


        /// <summary>
        /// Adds the specified variable to the calculation.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="value">The value of the variable.</param>
        /// <returns></returns>
        public CalculationVariable AddVariable( string name, double value)
        {   
        }
        
    }

    /// <summary>
    /// Represents a variable in a calculation.
    /// </summary>
    public class CalculationVariable
    {
        public string Name { get; set; }
        public double Value { get; set; }
    }
}