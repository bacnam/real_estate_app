using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

using RealEstate.ViewModels;
using RealEstate.Models;

namespace RealEstate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateRentNewsPage : ContentPage
    {
        CreateRentNewsViewModel viewModel;

        public CreateRentNewsPage(bool isSale)
        {
            InitializeComponent();

            if (Application.Current.MainPage is NavigationPage)
            {
                (Application.Current.MainPage as NavigationPage).BarTextColor = Color.Black;
            }
            NavigationPage.SetHasNavigationBar(this, false);

            BindingContext = viewModel = new CreateRentNewsViewModel(isSale);

            viewModel.SearchAddressDone = position =>
            {
                UpdateMap(position);
            };
        }

        private void UpdateMap(Position position)
        {
            maps.MoveToRegion(new MapSpan(position, 0.01, 0.01));
            var pin = new Pin()
            {
                Position = position,
                Address = viewModel.RentNews.FullAddress,
                Label = viewModel.RentNews.Address
            };
            maps.Pins.Clear();
            maps.Pins.Add(pin);
        }

        private void onBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void onContinute(object sender, EventArgs e)
        {
            if (viewModel.RentNews.RealType.Id == 0
                || viewModel.RentNews.City.Id == 0
                || viewModel.RentNews.District.Id == 0
                || viewModel.RentNews.Ward.Id == 0
                || string.IsNullOrEmpty(viewModel.RentNews.Address)
                || viewModel.RentNews.Project.Id == 0)
            {
                this.DisplayAlert("THÔNG BÁO", "Vui lòng nhập đầy đủ thông tin...", "OK");
            }
            else
            {
                Navigation.PushAsync(new CreateRentNewsStep2Page(viewModel.RentNews));
            }
        }

        void TinhTapped(object sender, EventArgs e)
        {
            SelectionModel[] selectedWards =
            {
                new SelectionModel()
                {
                    Name = viewModel.RentNews.City?.Name,
                    Id = viewModel.RentNews.City != null ? viewModel.RentNews.City.Id : 0
                }
            };
            SelectionPage selectionPage = new SelectionPage("Lựa chọn tỉnh/thành", "search/cities", selectedWards, SelectionMode.Single);
            selectionPage.SelectionDone = selections =>
            {
                if (selections.Count() > 0)
                {
                    var selection = selections.FirstOrDefault();
                    viewModel.RentNews.City = new CityModel()
                    {
                        Name = selection.Name,
                        Id = selection.Id
                    };

                    viewModel.SearchAddressCommand.Execute(null);
                }
            };
            Navigation.PushAsync(selectionPage);
        }

        void DistrictTapped(object sender, EventArgs e)
        {
            SelectionModel[] selectedWards =
            {
                new SelectionModel()
                {
                    Name = viewModel.RentNews.District?.Name,
                    Id = viewModel.RentNews.District != null ? viewModel.RentNews.District.Id : 0
                }
            };
            SelectionPage selectionPage = new SelectionPage("Lựa chọn quận/huyện", "search/districts/" + viewModel.RentNews.City.Id, selectedWards, SelectionMode.Single);
            selectionPage.SelectionDone = selections =>
            {
                if (selections.Count() > 0)
                {
                    var selection = selections.FirstOrDefault();
                    viewModel.RentNews.District = new DistrictModel()
                    {
                        Name = selection.Name,
                        Id = selection.Id
                    };
                    viewModel.SearchAddressCommand.Execute(null);
                }
            };
            Navigation.PushAsync(selectionPage);
        }

        void WardTapped(object sender, EventArgs e)
        {
            SelectionModel[] selectedWards =
            {
                new SelectionModel()
                {
                    Name = viewModel.RentNews.Ward?.Name,
                    Id = viewModel.RentNews.Ward != null ? viewModel.RentNews.Ward.Id : 0
                }
            };
            SelectionPage selectionPage = new SelectionPage("Lựa chọn xã/phường", "search/wards/" + viewModel.RentNews.District.Id, selectedWards, SelectionMode.Single);
            selectionPage.SelectionDone = selections =>
            {
                if (selections.Count() > 0)
                {
                    var selection = selections.FirstOrDefault();
                    viewModel.RentNews.Ward = new WardModel()
                    {
                        Name = selection.Name,
                        Id = selection.Id
                    };
                    viewModel.SearchAddressCommand.Execute(null);
                }
            };
            Navigation.PushAsync(selectionPage);
        }

        void RealTypeTapped(object sender, EventArgs e)
        {
            RealTypeSelectionPage selectionPage = new RealTypeSelectionPage(new RealTypeModel[] { viewModel.RentNews.RealType }, SelectionMode.Single);
            selectionPage.SelectionDone = realTypes =>
            {
                viewModel.RentNews.RealType = realTypes.FirstOrDefault();
            };
            Navigation.PushAsync(selectionPage);
        }

        void ProjectTapped(object sender, EventArgs e)
        {
            SelectionPage selectionPage = new SelectionPage("Lựa chọn dự án", "search/projects", null, SelectionMode.Single);
            selectionPage.SelectionDone = selections =>
            {
                if (selections.Count() > 0)
                {
                    var selection = selections.FirstOrDefault();
                    viewModel.RentNews.Project = new ProjectModel()
                    {
                        Name = selection.Name,
                        Id = selection.Id
                    };
                }
            };
            Navigation.PushAsync(selectionPage);
        }
    }
}