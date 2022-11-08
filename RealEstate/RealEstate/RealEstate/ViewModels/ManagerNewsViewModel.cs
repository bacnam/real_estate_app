using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using RealEstate.Models;
using RealEstate.Services;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace RealEstate.ViewModels
{
    public class ManagerNewsViewModel : BaseViewModel
    {
        public ManagerNewsViewModel(bool isSale)
        {
            RealDatas = new ObservableRangeCollection<RealDataModel>();
            IsSale = isSale;

            LoadMoreCommand.Execute(null);
        }

        private ObservableRangeCollection<RealDataModel> realDatas;
        public ObservableRangeCollection<RealDataModel> RealDatas
        {
            get
            {
                return realDatas;
            }
            set
            {
                SetProperty(ref realDatas, value);
            }
        }

        public int RealCount
        {
            get { return RealDatas.Count; }
        }

        public string RealCountString
        {
            get
            {
                if (IsSale)
                {
                    return string.Format("Bạn đã rao bán {0} tin", RealDatas.Count);
                }
                return string.Format("Bạn đã rao cho thuê {0} tin", RealDatas.Count);
            }
        }

        public bool IsSale { get; set; }

        int sort;
        public int Sort
        {
            get { return sort; }
            set { SetProperty(ref sort, value); OnPropertyChanged("RealDatas"); }
        }

        private Command loadMoreCommand;
        public Command LoadMoreCommand
        {
            get { return loadMoreCommand ?? (loadMoreCommand = new Command(async () => await LoadMoreAsync())); }
        }

        public async Task Save(long id)
        {
            if (IsBusy)
            {
                return;
            }
            try
            {
                IsBusy = true;

                var service = DependencyService.Get<IRealEstateService>();
                var success = await service.ChangeRealEstateAsync(id);
                if (success)
                {
                    var real = RealDatas.FirstOrDefault(r => r.Id == id);
                    if (real != null)
                    {
                        real.IsSaved = !real.IsSaved;
                    }
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task LoadMoreAsync()
        {
            if (IsBusy)
            {
                return;
            }
            try
            {
                IsBusy = true;

                var service = DependencyService.Get<IRealEstateService>();
                var res = await service.GetMyRealEstatesAsync(IsSale);
                if (res.Success)
                {
                    RealDatas.Clear();
                    foreach (var item in res.Data)
                    {
                        RealDataModel realData = new RealDataModel()
                        {
                            Acreage = item.Acreage,
                            Address = item.Address,
                            Direction = item.Direction,
                            Floor = item.Floor,
                            Id = item.Id,
                            IsSaved = item.IsSaved == 1,
                            Price = item.Price,
                            PriceType = item.PriceType,
                            RealType = item.RealType,
                            Room = item.Room,
                            Longitude = item.Longitude,
                            Latitude = item.Latitude,
                            Images = item.Images,
                            StartAt = item.StartAt,
                            EndAt = item.EndAt
                        };
                        RealDatas.Add(realData);
                    }
                    OnPropertyChanged("RealCountString");
                    OnPropertyChanged("RealDatas");
                }
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

    }
}
