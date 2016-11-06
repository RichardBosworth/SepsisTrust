﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guidelines.Model;
using SepsisTrust.ViewModels;
using Xamarin.Forms;

namespace SepsisTrust.GuidelineUI.Views
{
    public partial class AssessmentBlockTemplate : ContentView
    {
        public AssessmentBlockTemplate()
        {
            InitializeComponent();
        }

        private void Cell_OnTapped( object sender, EventArgs e )
        {
            var viewCell = sender as ViewCell;
            var blockActivityViewModel = viewCell.BindingContext as BlockActivityViewModel;
            blockActivityViewModel.Activated = !blockActivityViewModel.Activated;
        }
    }
}
