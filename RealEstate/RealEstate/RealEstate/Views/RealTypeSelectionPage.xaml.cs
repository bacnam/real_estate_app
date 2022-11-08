using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Models;
using RealEstate.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealEstate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RealTypeSelectionPage : ContentPage
    {
        RealTypeSelectionViewModel viewModel;
        public RealTypeSelectionPage(IEnumerable<RealTypeModel> reals, SelectionMode selectionMode = SelectionMode.Multiple)
        {
            InitializeComponent();
            if (Application.Current.MainPage is NavigationPage)
            {
                (Application.Current.MainPage as NavigationPage).BarTextColor = Color.Black;
            }
            NavigationPage.SetHasNavigationBar(this, false);

            BindingContext = viewModel = new RealTypeSelectionViewModel(selectionMode);
            viewModel.Select(reals.ToArray());
        }
        public Action<IEnumerable<RealTypeModel>> SelectionDone;

        private void onBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        void itemUnSelect(object sender, EventArgs e)
        {
            viewModel.ClearSelected();
        }

        void onSelect(object sender, EventArgs e)
        {
            var btn = sender as Button;
            viewModel.OnSelect(btn.Text);

            if (viewModel.SelectionMode == SelectionMode.Single)
            {
                onDone(sender, e);
            }
        }

        void onDone(object sender, EventArgs e)
        {
            SelectionDone?.Invoke(viewModel.RealTypes.Where(r => r.Selected));
            Navigation.PopAsync();
        }
    }
}