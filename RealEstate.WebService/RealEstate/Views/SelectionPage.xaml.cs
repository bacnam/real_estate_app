using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RealEstate.ViewModels;
using RealEstate.Models;

namespace RealEstate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectionPage : ContentPage
    {
        private SelectionViewModel viewModel;

        public Action<IEnumerable<SelectionModel>> SelectionDone;
        public SelectionPage(string path)
            : this("", path)
        {
            InitializeComponent();
        }

        public SelectionPage(string title, string path, IEnumerable<SelectionModel> selections = null, SelectionMode selectionMode = SelectionMode.Multiple)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            BindingContext = viewModel = new SelectionViewModel(title, path, selections, selectionMode);
        }

        public SelectionPage(string title, IEnumerable<SelectionModel> options, IEnumerable<SelectionModel> selections = null, SelectionMode selectionMode = SelectionMode.Multiple)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            BindingContext = viewModel = new SelectionViewModel(title, options, selections, selectionMode);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Application.Current.MainPage is NavigationPage)
            {
                (Application.Current.MainPage as NavigationPage).BarTextColor = Color.Black;
            }
        }

        private void onBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        void onDone(object sender, EventArgs e)
        {
            SelectionDone?.Invoke(viewModel.SelectedItems);
            Navigation.PopAsync();
        }

        private void collectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var selections = e.CurrentSelection.OfType<SelectionModel>();
                viewModel.Selected(selections);
                if (viewModel.SelectionMode == SelectionMode.Single)
                {
                    SelectionDone?.Invoke(selections);
                    Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}