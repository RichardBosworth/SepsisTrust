using System;
using FormBuilding.Model;
using Xamarin.Forms;

namespace FormBuilding.UI
{
    /// <summary>
    ///     Provides functionality to build a UI form.
    /// </summary>
    public interface IFormUIBuilder
    {
        View BuildFormView(Form form);
    }

    public class CalculatorFormUIBuilder : IFormUIBuilder
    {
        public View BuildFormView(Form form)
        {
            // Check that the form provided is a calculator form.
            if (!(form is CalculatorForm))
                throw new Exception("The form provided to the Calculator Form UI Builder is not a calculator form.");

            // Get the form in the correct format.
            var calculatorForm = (CalculatorForm) form;

            // Generate the stack layout for the form variables.
            StackLayout formStackLayout = new StackLayout();

            // Build the view for each variable.
            foreach (var formVariable in calculatorForm.Variables)
            {
                // Generate a horizontal stack layout for the variable views.
                var variableStackLayout = new StackLayout {Orientation = StackOrientation.Horizontal};
                variableStackLayout.Children.Add(new Label {Text = formVariable.Name});
                var entry = new Entry {Text = formVariable.Value.ToString()};
                variableStackLayout.Children.Add(entry);

                // Add the variable stack layout to the form stack layout.
                formStackLayout.Children.Add(variableStackLayout);
            }

            // Return the stacklayout.
            return formStackLayout; 
        }
    }
}