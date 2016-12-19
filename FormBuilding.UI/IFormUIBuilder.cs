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
            CalculatorForm calculatorForm = (CalculatorForm) form;

            // Build the view for each variable.
            foreach (var formVariable in calculatorForm.Variables)
            {
                StackLayout stackLayout = new StackLayout() {Orientation = StackOrientation.Horizontal};
                stackLayout.Children.Add(new Label() {Text = formVariable.Name});
                var entry = new Entry() {Text = formVariable.Value.ToString()};
                stackLayout.Children.Add(entry);
            }
        }
    }
}