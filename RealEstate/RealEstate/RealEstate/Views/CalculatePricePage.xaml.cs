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
    public partial class CalculatePricePage : ContentPage
    {
        ViewModels.CalculatorViewModel viewModel;

        public CalculatePricePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = viewModel = new CalculatorViewModel();
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

        void RoofTapped(object sender, EventArgs e)
        {
            List<SelectionModel> mai = new List<SelectionModel>();
            mai.Add(new SelectionModel() { Name = "Mái tôn", Id = 1 });
            mai.Add(new SelectionModel() { Name = "Mái bằng", Id = 2 });
            mai.Add(new SelectionModel() { Name = "Mái ngói", Id = 3 });
            SelectionPage selectionPage = new SelectionPage("Lựa chọn loại mái", mai, new SelectionModel[] { viewModel.CalculatorModel.Roof }, SelectionMode.Single);
            selectionPage.SelectionDone = selection =>
            {
                if (selection.Count() > 0)
                {
                    viewModel.CalculatorModel.Roof = selection.First();
                }
            };
            Navigation.PushAsync(selectionPage);
        }

        void SubstructureTapped(object sender, EventArgs e)
        {
            List<SelectionModel> mong = new List<SelectionModel>();
            mong.Add(new SelectionModel() { Name = "Móng đơn", Id = 1 });
            mong.Add(new SelectionModel() { Name = "Móng băng một phương", Id = 2 });
            mong.Add(new SelectionModel() { Name = "Móng băng hai phương", Id = 3 });
            SelectionPage selectionPage = new SelectionPage("Lựa chọn loại móng", mong, new SelectionModel[] { viewModel.CalculatorModel.Substructure }, SelectionMode.Single);
            selectionPage.SelectionDone = selection =>
            {
                if (selection.Count() > 0)
                {
                    viewModel.CalculatorModel.Substructure = selection.First();
                }
            };
            Navigation.PushAsync(selectionPage);
        }

        void MaterialTapped(object sender, EventArgs e)
        {
            List<SelectionModel> mong = new List<SelectionModel>();
            mong.Add(new SelectionModel() { Name = "Vật tư trung bình", Id = 1 });
            mong.Add(new SelectionModel() { Name = "Vật tư trung bình khá", Id = 2 });
            mong.Add(new SelectionModel() { Name = "Vật tư khá", Id = 3 });
            mong.Add(new SelectionModel() { Name = "Vật tư tốt", Id = 4 });
            SelectionPage selectionPage = new SelectionPage("Lựa chọn loại vật tư", mong, new SelectionModel[] { viewModel.CalculatorModel.Material }, SelectionMode.Single);
            selectionPage.SelectionDone = selection =>
            {
                if (selection.Count() > 0)
                {
                    viewModel.CalculatorModel.Material = selection.First();
                }
            };
            Navigation.PushAsync(selectionPage);
        }

        void onDoneClicked(object sender, EventArgs e)
        {
            if (viewModel.CanDone())
            {
                Navigation.PushAsync(new CalculatePriceResultPage(viewModel.CalculatorModel));
            }
            else
            {
                DisplayAlert("THÔNG BÁO", "Vui lòng nhập đầy đủ thông tin.", "OK");
            }
        }
    }
}