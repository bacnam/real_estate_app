using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using RealEstate.Core.Requests;
using RealEstate.Models;
using RealEstate.Services;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace RealEstate.ViewModels
{
    public class SearchResultViewModel : BaseViewModel
    {
        public SearchResultViewModel(SearchRequest request)
        {
            currentRequest = request;
            IsSale = request.IsSale;
            IsLease = !request.IsSale;
            realDatas = new ObservableRangeCollection<RealDataModel>();
            LoadMoreAsync();
        }

        private SearchRequest currentRequest;

        private ObservableRangeCollection<RealDataModel> realDatas;
        public ObservableRangeCollection<RealDataModel> RealDatas
        {
            get
            {
                ObservableRangeCollection<RealDataModel> _realDatas = new ObservableRangeCollection<RealDataModel>();
                if (Sort == 0)
                {
                    _realDatas.AddRange(realDatas.OrderByDescending(s => s.StartAt));
                }
                else if (Sort == 1)
                {
                    _realDatas.AddRange(realDatas.OrderByDescending(s => s.PriceValue));
                }
                else if (Sort == 2)
                {
                    _realDatas.AddRange(realDatas.OrderBy(s => s.PriceValue));
                }
                else if (Sort == 3)
                {
                    _realDatas.AddRange(realDatas.OrderByDescending(s => s.Acreage));
                }
                else if (Sort == 4)
                {
                    _realDatas.AddRange(realDatas.OrderBy(s => s.Acreage));
                }
                return _realDatas;
            }
        }

        public int RealCount
        {
            get { return realDatas.Count; }
        }

        public bool IsLease { get; }

        public bool IsSale { get; }

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

                var service = DependencyService.Get<ISearchService>();
                var res = await service.SearchAsync(currentRequest);
                if (res.Success)
                {
                    currentRequest.Start += res.Data.Length;
                    var data = res.Data.Select(d => new RealDataModel()
                    {
                        Acreage = d.Acreage,
                        Address = d.Address,
                        Direction = d.Direction,
                        Floor = d.Floor,
                        Id = d.Id,
                        IsSaved = d.IsSaved == 1,
                        Price = d.Price,
                        PriceType = d.PriceType,
                        RealType = d.RealType,
                        Room = d.Room,
                        Longitude = d.Longitude,
                        Latitude = d.Latitude,
                        Images = d.Images,
                        StartAt = d.StartAt,
                        EndAt = d.EndAt
                    });
                    realDatas.InsertRange(0, data);
                    OnPropertyChanged("RealCount");
                    OnPropertyChanged("RealDatas");
                }
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
