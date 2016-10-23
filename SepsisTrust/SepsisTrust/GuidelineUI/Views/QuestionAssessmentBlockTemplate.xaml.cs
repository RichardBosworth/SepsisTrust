using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guidelines.Model;
using SepsisTrust.ViewModels;
using Xamarin.Forms;

namespace SepsisTrust.GuidelineUI.Views
{
    public partial class QuestionAssessmentBlockTemplate : ContentView
    {
        public QuestionAssessmentBlockTemplate()
        {
            InitializeComponent();
        }

        private void ListView_OnItemSelected( object sender, SelectedItemChangedEventArgs e )
        {
            if ( e.SelectedItem != null )
            {
                var blockActivityData = e.SelectedItem as BlockActivityData;

                var listView = sender as ListView;
                var viewModel = listView.BindingContext as GuidelinePageViewModel;
                foreach ( var blockActivity in viewModel.Block.BlockActivities )
                {
                    blockActivity.Activated = false;
                }

                blockActivityData.Activated = true;
                listView.SelectedItem = null;

                viewModel.ProceedCommand.Execute();
            }
        }
    }
}
