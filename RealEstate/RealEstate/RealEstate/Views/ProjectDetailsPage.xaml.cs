using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RealEstate.ViewModels;
using RealEstate.Models;
using RealEstate.Renderers;

namespace RealEstate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjectDetailsPage : ContentPage
    {
        ProjectDetailsViewModel viewModel;
        public ProjectDetailsPage(long projectId)
        {
            InitializeComponent();

            BindingContext = viewModel = new ProjectDetailsViewModel(projectId);
            if (Application.Current.MainPage is NavigationPage)
            {
                (Application.Current.MainPage as NavigationPage).BarTextColor = Color.White;
            }
            NavigationPage.SetHasNavigationBar(this, false);
        }
        void onLease(object sender, EventArgs e)
        {
            SetLease();
        }

        void onSale(object sender, EventArgs e)
        {
            SetSale();
        }

        private void SetLease()
        {
            var colorLease = lblLease.TextColor;
            var colorSale = lblSale.TextColor;

            lblLease.TextColor = colorSale;
            lineLease.IsVisible = true;

            lblSale.TextColor = colorLease;
            lineSale.IsVisible = false;

            viewModel.IsDetails = !viewModel.IsDetails;
        }

        private void SetSale()
        {
            var colorLease = lblLease.TextColor;
            var colorSale = lblSale.TextColor;

            lblLease.TextColor = colorSale;
            lineLease.IsVisible = false;

            lblSale.TextColor = colorLease;
            lineSale.IsVisible = true;

            viewModel.IsDetails = !viewModel.IsDetails;
        }

        async void onSaveReal(object sender, EventArgs e)
        {
            var btn = sender as CustomImageButton;
            await viewModel.SaveReal(btn.Tag);
        }

        async void onSaveProject(object sender, EventArgs e)
        {
            await viewModel.SaveProject();
        }

        private void onBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        void onShare(object sender, EventArgs e)
        {
            string text = string.Format("Dự án: {0}", viewModel.Project.Address);
            Xamarin.Essentials.Share.RequestAsync(text);
        }

        void onSelected(object sender, ItemTappedEventArgs e)
        {
            var realEstate = e.Item as RealDataModel;
            Navigation.PushAsync(new SearchResultsDetailsPage(realEstate.Id));
        }
    }
}