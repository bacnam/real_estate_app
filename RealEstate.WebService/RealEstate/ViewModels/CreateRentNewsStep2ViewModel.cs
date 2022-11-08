using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using RealEstate.Models;
using RealEstate.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RealEstate.ViewModels
{
    public class CreateRentNewsStep2ViewModel : BaseViewModel
    {
        public CreateRentNewsStep2ViewModel(CreateRentNewsModel rentNews)
        {
            RentNews = rentNews;

            directions = new ObservableRangeCollection<DirectionModel>();
            GetDirectionsAsync().ConfigureAwait(false);
        }

        CreateRentNewsModel rentNews;
        public CreateRentNewsModel RentNews { get { return rentNews; } set { SetProperty(ref rentNews, value); } }

        private ObservableRangeCollection<DirectionModel> directions;
        public ObservableRangeCollection<DirectionModel> Directions
        {
            get
            {
                return directions;
            }
        }

        public int DirectionHeight
        {
            get { return Directions.Count * 54 / 3; }
        }

        private async Task GetDirectionsAsync()
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;
            try
            {
                var service = DependencyService.Get<IAddressService>();
                var _directions = await service.GetDirectionsAsync();
                Directions.Clear();
                Directions.AddRange(_directions);
                OnPropertyChanged("DirectionHeight");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public Command RoomAddCommand
        {
            get
            {
                return new Command(() => { RentNews.Room++; });
            }
        }

        public Command RoomSubCommand
        {
            get
            {
                return new Command(() => { RentNews.Room--; });
            }
        }

        public Command FloorAddCommand
        {
            get
            {
                return new Command(() => { RentNews.Floor++; });
            }
        }

        public Command FloorSubCommand
        {
            get
            {
                return new Command(() => { RentNews.Floor--; });
            }
        }
    }
}
