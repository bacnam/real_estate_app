using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Models;
using RealEstate.Renderers;
using RealEstate.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealEstate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SavedPage : ContentPage
    {
        SavedViewModel viewModel;
        public SavedPage(int menuItemType)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            BindingContext = viewModel = new SavedViewModel();
            if (menuItemType == 1)
            {
                viewModel.ChangeState(false);
            }
            else
            {
                viewModel.ChangeState(true);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Application.Current.MainPage is NavigationPage)
            {
                (Application.Current.MainPage as NavigationPage).BarTextColor = Color.Black;
            }

            if (viewModel.IsReal)
            {
                viewModel.LoadNewsSaved.Execute(null);
                SetNewsSaved();
            }
            else
            {
                SetProjectSaved();
                viewModel.LoadProjectSaved.Execute(null);
            }
        }

        private void onBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void onSelected(object sender, ItemTappedEventArgs e)
        {
            var realEstate = e.Item as RealDataModel;
            Navigation.PushAsync(new SearchResultsDetailsPage(realEstate.Id));
        }

        private void onProjectSelected(object sender, ItemTappedEventArgs e)
        {
            var project = e.Item as ProjectModel;
            Navigation.PushAsync(new ProjectDetailsPage(project.Id));
        }

        void onProjectSaved(object sender, EventArgs e)
        {
            SetProjectSaved();
            viewModel.ChangeState(true);
        }

        void onNewsSaved(object sender, EventArgs e)
        {
            SetNewsSaved();
            viewModel.ChangeState(false);
        }

        private void SetProjectSaved()
        {
            var colorNewsSave = lblNewsSaved.TextColor;
            var colorProjectSaved = lblProjectSaved.TextColor;

            lblProjectSaved.TextColor = colorNewsSave;
            lineProjectSaved.IsVisible = true;

            lblNewsSaved.TextColor = colorProjectSaved;
            lineNewsSaved.IsVisible = false;
        }

        private void SetNewsSaved()
        {
            var colorNewsSave = lblNewsSaved.TextColor;
            var colorProjectSaved = lblProjectSaved.TextColor;

            lblProjectSaved.TextColor = colorNewsSave;
            lineProjectSaved.IsVisible = false;

            lblNewsSaved.TextColor = colorProjectSaved;
            lineNewsSaved.IsVisible = true;
        }

        async void onSaveProject(object sender, EventArgs e)
        {
            var btn = sender as CustomImageButton;
            await viewModel.SaveProject(btn.Tag);
        }

        async void onSaveNews(object sender, EventArgs e)
        {
            var btn = sender as CustomImageButton;
            await viewModel.SaveNews(btn.Tag);
        }
    }
}