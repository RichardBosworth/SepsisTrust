using System.Collections.Generic;
using Xamarin.Forms;

namespace SepsisTrust.Cells
{
    public partial class PickerCell : ViewCell
    {
        public static readonly BindableProperty ItemsProperty = BindableProperty.Create<PickerCell, List<string>>(p => p.Items, new List<string>());

        public static readonly BindableProperty LabelProperty = BindableProperty.Create<PickerCell, string>(p => p.Label, "");


        public PickerCell( )
        {
            InitializeComponent();
        }

        public string Label
        {
            get { return (string) GetValue(LabelProperty); }
            set
            {
                SetValue(LabelProperty, value); 
                DescriptionLabel.Text = value;
            }
        }

        public List<string> Items
        {
            get { return (List<string>) GetValue(ItemsProperty); }
            set
            {
                SetValue(ItemsProperty, value);
                Picker.Items.Clear();
                foreach ( var item in value )
                {
                    Picker.Items.Add(item);
                }
                Picker.SelectedIndex = 0;
            }
        }
    }
}