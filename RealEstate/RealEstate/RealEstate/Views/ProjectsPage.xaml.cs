using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Core.Requests;
using RealEstate.Models;
using RealEstate.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealEstate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjectsPage : ContentPage
    {
        private ProjectsViewModel viewModel;
        private string allTitle = "Tất cả";
        public ProjectsPage()
        {
            InitializeComponent();
            if (Application.Current.MainPage is NavigationPage)
            {
                (Application.Current.MainPage as NavigationPage).BarTextColor = Color.Black;
            }
            NavigationPage.SetHasNavigationBar(this, false);

            BindingContext = viewModel = new ProjectsViewModel();
        }
        void TinhTapped(object sender, EventArgs e)
        {
            var selections = viewModel.Cities.Select(d => new SelectionModel()
            {
                Id = d.Id,
                Name = d.Name
            });
            SelectionPage selectionPage = new SelectionPage("Lựa chọn tỉnh/thành", "search/cities", selections);
            selectionPage.SelectionDone = selection =>
            {
                if (selection.Count() > 0)
                {
                    viewModel.AddCities(selection.Select(s => new CityModel()
                    {
                        Id = s.Id,
                        Name = s.Name
                    }).ToArray());
                }
                else
                {
                    viewModel.AddCities(new CityModel()
                    {
                        Name = allTitle
                    });
                }
            };
            Navigation.PushAsync(selectionPage);
        }

        void DistrictTapped(object sender, EventArgs e)
        {
            var selections = viewModel.Districts.Select(d => new SelectionModel()
            {
                Id = d.Id,
                Name = d.Name
            });
            SelectionPage selectionPage = new SelectionPage("Lựa chọn quận/huyện", "search/districts/" + viewModel.CityIds, selections);
            selectionPage.SelectionDone = selection =>
            {
                if (selection.Count() > 0)
                {
                    viewModel.AddDistricts(selection.Select(s => new DistrictModel()
                    {
                        Id = s.Id,
                        Name = s.Name
                    }).ToArray());
                }
                else
                {
                    viewModel.AddDistricts(new DistrictModel()
                    {
                        Name = allTitle
                    });
                }
            };
            Navigation.PushAsync(selectionPage);
        }

        private void onBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        void onSearch(object sender, EventArgs e)
        {
            SearchProjectRequest request = new SearchProjectRequest()
            {
                Start = 0,
                City = viewModel.CityIds,
                District = viewModel.DistrictIds
            };
            Navigation.PushAsync(new ProjectSearchResultPage(request));
        }
    }
}