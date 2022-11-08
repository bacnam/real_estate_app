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
    public partial class RegisterReceiveNewsPage : ContentPage
    {
        RegisterReceiveNewsViewModel viewModel;
        public RegisterReceiveNewsPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            BindingContext = viewModel = new RegisterReceiveNewsViewModel();
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

        private void onManagerNews(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MnanagerNewsPage());
        }

        void RealTypeTapped(object sender, EventArgs e)
        {
            RealTypeSelectionPage selectionPage = new RealTypeSelectionPage(new RealTypeModel[] { viewModel.RegisterNews.RealType }, SelectionMode.Single);
            selectionPage.SelectionDone = realTypes =>
            {
                if (realTypes.Count() > 0)
                {
                    viewModel.RegisterNews.RealType = realTypes.FirstOrDefault();
                }
            };
            Navigation.PushAsync(selectionPage);
        }

        void TinhTapped(object sender, EventArgs e)
        {
            var selections = new SelectionModel[] {
                new SelectionModel()
                {
                    Id = viewModel.RegisterNews.City.Id,
                    Name = viewModel.RegisterNews.City.Name
                }
            };
            SelectionPage selectionPage = new SelectionPage("Lựa chọn tỉnh/thành", "search/cities", selections, SelectionMode.Single);
            selectionPage.SelectionDone = selection =>
            {
                if (selection.Count() > 0)
                {
                    viewModel.RegisterNews.City = selection.Select(s => new CityModel()
                    {
                        Id = s.Id,
                        Name = s.Name
                    }).FirstOrDefault();
                }
            };
            Navigation.PushAsync(selectionPage);
        }

        void DistrictTapped(object sender, EventArgs e)
        {
            var selections = new SelectionModel[] {
                new SelectionModel()
                {
                    Id = viewModel.RegisterNews.District.Id,
                    Name = viewModel.RegisterNews.District.Name
                }
            };
            SelectionPage selectionPage = new SelectionPage("Lựa chọn quận/huyện", "search/districts/" + viewModel.RegisterNews.City.Id, selections, SelectionMode.Single);
            selectionPage.SelectionDone = selection =>
            {
                if (selection.Count() > 0)
                {
                    viewModel.RegisterNews.District = selection.Select(s => new DistrictModel()
                    {
                        Id = s.Id,
                        Name = s.Name
                    }).FirstOrDefault();
                }
            };
            Navigation.PushAsync(selectionPage);
        }

        void WardTapped(object sender, EventArgs e)
        {
            var selections = new SelectionModel[] {
                new SelectionModel()
                {
                    Id = viewModel.RegisterNews.Ward.Id,
                    Name = viewModel.RegisterNews.Ward.Name
                }
            };
            SelectionPage selectionPage = new SelectionPage("Lựa chọn xã/phường", "search/wards/" + viewModel.RegisterNews.District.Id, selections, SelectionMode.Single);
            selectionPage.SelectionDone = selection =>
            {
                if (selection.Count() > 0)
                {
                    viewModel.RegisterNews.Ward = selection.Select(s => new WardModel()
                    {
                        Id = s.Id,
                        Name = s.Name
                    }).FirstOrDefault();
                }
            };
            Navigation.PushAsync(selectionPage);
        }

        void DirectionTapped(object sender, EventArgs e)
        {
            var selections = new SelectionModel[] {
                new SelectionModel()
                {
                    Id = viewModel.RegisterNews.Direction.Id,
                    Name = viewModel.RegisterNews.Direction.Name
                }
            };
            SelectionPage selectionPage = new SelectionPage("Lựa chọn hướng nhà", "addresses/directions", selections, SelectionMode.Single);
            selectionPage.SelectionDone = selection =>
            {
                if (selection.Count() > 0)
                {
                    viewModel.RegisterNews.Direction = selection.Select(s => new DirectionModel()
                    {
                        Id = s.Id,
                        Name = s.Name
                    }).FirstOrDefault();
                }
            };
            Navigation.PushAsync(selectionPage);
        }

        void ProjectTapped(object sender, EventArgs e)
        {
            var selections = new SelectionModel[] {
                new SelectionModel()
                {
                    Id = viewModel.RegisterNews.Project.Id,
                    Name = viewModel.RegisterNews.Project.Name
                }
            };
            SelectionPage selectionPage = new SelectionPage("Lựa chọn dự án", "search/projects", selections, SelectionMode.Single);
            selectionPage.SelectionDone = selection =>
            {
                if (selection.Count() > 0)
                {
                    viewModel.RegisterNews.Project = selection.Select(s => new ProjectModel()
                    {
                        Id = s.Id,
                        Name = s.Name
                    }).FirstOrDefault();
                }
            };
            Navigation.PushAsync(selectionPage);
        }

        void PriceTapped(object sender, EventArgs e)
        {
            var selections = new SelectionModel[] {
                new SelectionModel()
                {
                    Id = viewModel.RegisterNews.Project.Id,
                    Name = viewModel.RegisterNews.Project.Name
                }
            };
            var options = new SelectionModel[]
            {
                new SelectionModel(){Id = 1, Name = "100 - 300 triệu"},
                new SelectionModel(){Id = 2, Name = "300 - 500 triệu"},
                new SelectionModel(){Id = 3, Name = "500 - 700 triệu"},
                new SelectionModel(){Id = 4, Name = "700 triệu - 1 tỷ"},
                new SelectionModel(){Id = 5, Name = "1 - 3 tỷ"},
                new SelectionModel(){Id = 6, Name = "3 - 5 tỷ"},
                new SelectionModel(){Id = 7, Name = "5 - 10 tỷ"},
                new SelectionModel(){Id = 8, Name = "Trên 10 tỷ"}
            };
            SelectionPage selectionPage = new SelectionPage("Mức giá", options, selections, SelectionMode.Single);
            selectionPage.SelectionDone = selection =>
            {
                if (selection.Count() > 0)
                {
                    viewModel.RegisterNews.Price = selection.FirstOrDefault();
                }
            };
            Navigation.PushAsync(selectionPage);
        }

        void onContinue(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterReceiveNewsStep2Page(viewModel.RegisterNews));
        }
    }
}