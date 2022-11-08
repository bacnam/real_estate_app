using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Core.Requests;
using RealEstate.Models;
using RealEstate.Renderers;
using RealEstate.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealEstate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjectSearchResultPage : ContentPage
    {
        ProjectSearchResultsViewModel viewModel;
        public ProjectSearchResultPage(SearchProjectRequest request)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            BindingContext = viewModel = new ProjectSearchResultsViewModel(request);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            listViewProjects.SelectedItem = null;
            if (Application.Current.MainPage is NavigationPage)
            {
                (Application.Current.MainPage as NavigationPage).BarTextColor = Color.Black;
            }
        }

        private void onBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        async void onSave(object sender, EventArgs e)
        {
            var btn = sender as CustomImageButton;
            await viewModel.Save(btn.Tag);
        }

        List<string> sorts = new List<string>()
        {
            "Tin mới nhất", "Diện tích lớn nhất", "Diện tích nhỏ nhất"
        };

        async void onSort(object sender, EventArgs e)
        {
            string sort = await DisplayActionSheet("Sắp xếp theo", "Hủy", sorts[viewModel.Sort], sorts.Where(s => s != sorts[viewModel.Sort]).ToArray());
            viewModel.Sort = sorts.IndexOf(sort);
        }

        private void onSelected(object sender, ItemTappedEventArgs e)
        {
            var project = e.Item as ProjectModel;
            Navigation.PushAsync(new ProjectDetailsPage(project.Id));
        }
    }
}