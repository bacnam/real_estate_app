using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Core.Requests;
using RealEstate.Models;
using RealEstate.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealEstate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        private SearchViewModel viewModel;
        private string allTitle = "Tất cả";
        MenuItemType _menuItemType;

        public SearchPage(MenuItemType menuItemType)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = viewModel = new SearchViewModel(menuItemType);
            _menuItemType = menuItemType;

            roomNumberSelected(0);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Application.Current.MainPage is NavigationPage)
            {
                (Application.Current.MainPage as NavigationPage).BarTextColor = Color.Black;
            }

            if (_menuItemType == MenuItemType.Sale)
            {
                SetSale();
            }
            else if (_menuItemType == MenuItemType.Lease)
            {
                SetLease();
            }
        }

        private async void onGetLocation(object sender, EventArgs e)
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            var addresses = await Geocoding.GetPlacemarksAsync(location);
            var address = addresses.FirstOrDefault();
            if (address != null)
            {
                string addressStr = address.Thoroughfare + ", " + address.SubLocality + ", " + address.SubAdminArea + ", " + address.Locality;
                search.Text = addressStr;
            }
        }

        private void onBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        void onSearch(object sender, EventArgs e)
        {
            SearchRequest request = new SearchRequest()
            {
                Start = 0,
                City = viewModel.CityIds,
                District = viewModel.DistrictIds,
                Ward = viewModel.WardIds,
                RealTypes = string.Join(",", viewModel.RealTypes.Select(c => c.Title)),
                Room = viewModel.SleepRoomSelected,
                IsSale = _menuItemType == MenuItemType.Sale,
                FromAcreage = viewModel.FromAcreage,
                ToAcreage = viewModel.ToAcreage,
                FromPrice = viewModel.FromPrice,
                ToPrice = viewModel.ToPrice
            };
            Navigation.PushAsync(new SearchResultPage(request));
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

        void WardTapped(object sender, EventArgs e)
        {
            var selections = viewModel.Wards.Select(d => new SelectionModel()
            {
                Id = d.Id,
                Name = d.Name
            });
            SelectionPage selectionPage = new SelectionPage("Lựa chọn xã/phường", "search/wards/" + viewModel.DistrictIds, selections);
            selectionPage.SelectionDone = selection =>
            {
                if (selection.Count() > 0)
                {
                    viewModel.AddWards(selection.Select(s => new WardModel()
                    {
                        Id = s.Id,
                        Name = s.Name
                    }).ToArray());
                }
                else
                {
                    viewModel.AddWards(new WardModel()
                    {
                        Name = allTitle
                    });
                }
            };
            Navigation.PushAsync(selectionPage);
        }

        void StreetTapped(object sender, EventArgs e)
        {
            /*if (viewModel.WardSelected.Count > 0)
            {
                SelectionPage selectionPage = new SelectionPage("Lựa chọn đường/phố", "search/streets/" + viewModel.CitySelected.Select(c => c.Id).ToArray());
                selectionPage.SelectionDone = selection =>
                {
                    viewModel.StreetSelected.Clear();
                    if (selection.Count > 0)
                    {
                        viewModel.StreetSelected.AddRange(selection.OfType<StreetModel>());
                    }
                    else
                    {
                        viewModel.StreetSelected.Add(new StreetModel()
                        {
                            Name = allTitle
                        });
                    }
                };
                Navigation.PushAsync(selectionPage);
            }
            else
            {
                this.DisplayAlert("THÔNG BÁO", "Bạn chưa chọn Xã/phường", "OK");
            }*/
        }

        void RealTypeTapped(object sender, EventArgs e)
        {
            RealTypeSelectionPage selectionPage = new RealTypeSelectionPage(viewModel.RealTypes);
            selectionPage.SelectionDone = realTypes =>
            {
                viewModel.RealTypes = new ObservableRangeCollection<RealTypeModel>(realTypes);
            };
            Navigation.PushAsync(selectionPage);
        }

        void AcreageTapped(object sender, EventArgs e)
        {
            AcreageSelectPage selectionPage = new AcreageSelectPage();
            selectionPage.SelectionDone = (fromAcreage, toAcreage) =>
            {
                viewModel.FromAcreage = fromAcreage;
                viewModel.ToAcreage = toAcreage;
            };
            Navigation.PushAsync(selectionPage);
        }

        void PriceTapped(object sender, EventArgs e)
        {
            SelectPricePage selectionPage = new SelectPricePage(_menuItemType == MenuItemType.Sale);
            selectionPage.SelectionDone = (fromPrice, toPrice) =>
            {
                viewModel.FromPrice = fromPrice;
                viewModel.ToPrice = toPrice;
            };
            Navigation.PushAsync(selectionPage);
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
            _menuItemType = MenuItemType.Lease;
            var colorLease = lblLease.TextColor;
            var colorSale = lblSale.TextColor;

            lblLease.TextColor = colorSale;
            lineLease.IsVisible = true;

            lblSale.TextColor = colorLease;
            lineSale.IsVisible = false;
        }

        private void SetSale()
        {
            _menuItemType = MenuItemType.Sale;
            var colorLease = lblLease.TextColor;
            var colorSale = lblSale.TextColor;

            lblLease.TextColor = colorSale;
            lineLease.IsVisible = false;

            lblSale.TextColor = colorLease;
            lineSale.IsVisible = true;
        }

        void roomNumberSelected(object sender, EventArgs e)
        {
            var index = roomNumber.Children.IndexOf(sender as Button);
            roomNumberSelected(index);

            if (index >= 0)
            {
                viewModel.SleepRoomSelected = index;
            }
        }

        void roomNumberSelected(int index)
        {
            if (index >= 0 && index < roomNumber.Children.Count)
            {
                foreach (Button children in roomNumber.Children)
                {
                    children.BackgroundColor = Color.FromHex("#F6F6F8");
                    children.TextColor = Color.FromHex("#A9A9BA");
                }
                var room = roomNumber.Children.ElementAt(index) as Button;
                room.BackgroundColor = Color.FromHex("#FC9261");
                room.TextColor = Color.White;
            }
        }
    }
}