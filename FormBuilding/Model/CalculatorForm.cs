using System.Collections.Generic;
using System.Linq;

namespace FormBuilding.Model
{
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
            // Check if the variable name has already been specified.   
            if (Variables.Any(variable => variable.Name == name))
            {
                return null;
            }

            // Otherwise, add the variable.
            var calculationVariable = new CalculationVariable() {Name = name, Value = value};
            Variables.Add(calculationVariable);

            // Return that variable to the caller.
            return calculationVariable;
        }
    }
}