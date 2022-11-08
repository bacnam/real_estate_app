using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using RealEstate.Extensions;
using RealEstate.Models;
using RealEstate.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealEstate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateRentNewsStep2Page : ContentPage
    {
        CreateRentNewsStep2ViewModel viewModel;
        List<SelectionModel> unitPriceSelections = new List<SelectionModel>();
        public CreateRentNewsStep2Page(CreateRentNewsModel rentNews)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            unitPriceSelections.Add(new SelectionModel()
            {
                Id = 1,
                Name = "Ngàn",
            });
            unitPriceSelections.Add(new SelectionModel()
            {
                Id = 2,
                Name = "Triệu",
            });
            unitPriceSelections.Add(new SelectionModel()
            {
                Id = 3,
                Name = "Tỷ",
            });
            BindingContext = viewModel = new CreateRentNewsStep2ViewModel(rentNews);
            if (viewModel.RentNews.UnitPrice == null)
            {
                viewModel.RentNews.UnitPrice = unitPriceSelections.FirstOrDefault();
            }
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

        private void onContinute(object sender, EventArgs e)
        {
            if (viewModel.RentNews.Acreage <= 0
                || viewModel.RentNews.Price <= 0
                || viewModel.RentNews.Images.Count <= 0
                || viewModel.RentNews.Floor <= 0
                || viewModel.RentNews.Direction.Id == 0
                || string.IsNullOrEmpty(viewModel.RentNews.Title)
                || string.IsNullOrEmpty(viewModel.RentNews.Desciption)
                || viewModel.RentNews.Room <= 0)
            {
                this.DisplayAlert("THÔNG BÁO", "Vui lòng nhập đầy đủ thông tin...", "OK");
            }
            else
            {
                Navigation.PushAsync(new CreateRentNewsStep3Page(viewModel.RentNews));
            }
        }

        private void onTapedUnitPrice(object sender, EventArgs e)
        {
            List<SelectionModel> selectedselections = new List<SelectionModel>();
            if (viewModel.RentNews.UnitPrice != null)
            {
                selectedselections.Add(new SelectionModel()
                {
                    Name = viewModel.RentNews.UnitPrice.Name,
                    Id = viewModel.RentNews.UnitPrice.Id
                });
            }
            SelectionPage selectionPage = new SelectionPage("Lựa chọn đơn vị", unitPriceSelections, selectedselections, SelectionMode.Single);
            selectionPage.SelectionDone = selection =>
            {
                if (selection.Count() > 0)
                {
                    viewModel.RentNews.UnitPrice = selection.FirstOrDefault();
                }
            };
            Navigation.PushAsync(selectionPage);
        }
        private DirectionModel lastDirectionModel = null;
        void onSelectDirection(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var direction = viewModel.Directions.FirstOrDefault(d => d.Name == btn.Text);
            if (lastDirectionModel != null)
            {
                lastDirectionModel.Selected = false;
            }
            if (direction != null)
            {
                direction.Selected = !direction.Selected;
                lastDirectionModel = direction;
            }
        }

        async void onAddImageClicked(object sender, EventArgs e)
        {
            var btn = sender as ImageButton;
            var index = Grid.GetColumn(btn);
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("THÔNG BÁO", "Vui lòng cấp quyền truy cập vào thư viện hình ảnh.", "OK");
                return;
            }
            var image = await CrossMedia.Current.PickPhotoAsync();
            if (image != null)
            {
                SetupImage(image, btn, index + 1);
            }
        }

        private void SetupImage(MediaFile mediaFile, ImageButton imageButton, int id)
        {
            var imageStream = mediaFile.GetStream();
            var source = ImageSource.FromStream(() => imageStream);
            imageButton.Source = source;
            imageButton.Padding = new Thickness(0, 0);
            imageButton.Aspect = Aspect.AspectFill;

            RentImageSource rentImageSource = viewModel.RentNews.Images.FirstOrDefault(img => img.Id == id);
            if (rentImageSource != null)
            {
                rentImageSource.Source = imageStream.ReadToEnd();
            }
            else
            {
                rentImageSource = new RentImageSource()
                {
                    Id = id,
                    Source = imageStream.ReadToEnd()
                };
                viewModel.RentNews.Images.Add(rentImageSource);
            }
        }

        void DirectionTapped(object sender, EventArgs e)
        {
            SelectionPage selectionPage = new SelectionPage("Lựa chọn hướng nhà", "addresses/directions", null, SelectionMode.Single);
            selectionPage.SelectionDone = selections =>
            {
                var selection = selections.FirstOrDefault();
                viewModel.RentNews.Direction = new DirectionModel()
                {
                    Id = selection.Id,
                    Name = selection.Name
                };
            };
            Navigation.PushAsync(selectionPage);
        }
    }
}