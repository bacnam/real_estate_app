using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using RealEstate.Extensions;
using RealEstate.Models;
using Xamarin.Forms;

namespace RealEstate.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        public SearchViewModel(MenuItemType menuItemType)
        {
            RealTypes = new ObservableRangeCollection<RealTypeModel>();
            StreetSelected = new ObservableRangeCollection<StreetModel>();
            Cities = new ObservableRangeCollection<CityModel>();
            Districts = new ObservableRangeCollection<DistrictModel>();
            Wards = new ObservableRangeCollection<WardModel>();
            OnPropertyChanged("LabelRealTypes");

            Rooms = new ObservableCollection<string>();
            Rooms.Add("Bất kỳ");
            Rooms.Add("1");
            Rooms.Add("2");
            Rooms.Add("3");
            Rooms.Add("4");
            Rooms.Add("5+");

            InitSelected();
        }

        private void InitSelected()
        {
            RealTypes.Clear();
            RealTypes.Add(new RealTypeModel()
            {
                Title = "Tất cả"
            });
            StreetSelected.Add(new StreetModel()
            {
                Name = "Tất cả"
            });

            SleepRoomSelected = 0;

            AddCities(new CityModel()
            {
                Name = "Tất cả"
            });
            AddDistricts(new DistrictModel()
            {
                Name = "Tất cả"
            });
            AddWards(new WardModel()
            {
                Name = "Tất cả"
            });
        }

        public Command ResetCommand
        {
            get { return new Command(() => InitSelected()); }
        }

        public ObservableRangeCollection<CityModel> Cities;

        public void AddCities(params CityModel[] cities)
        {
            Cities.Clear();
            Cities.AddRange(cities);
            OnPropertyChanged("CitySelectedString");
        }

        public string CitySelectedString
        {
            get
            {
                return string.Join(", ", Cities.Select(c => c.Name));
            }
        }

        public string CityIds
        {
            get
            {
                return string.Join(",", Cities.Select(c => c.Id));
            }
        }

        public ObservableRangeCollection<DistrictModel> Districts;

        public void AddDistricts(params DistrictModel[] districts)
        {
            Districts.Clear();
            Districts.AddRange(districts);
            OnPropertyChanged("DistrictSelectedString");
        }

        public string DistrictSelectedString
        {
            get
            {
                return string.Join(", ", Districts.Select(c => c.Name));
            }
        }

        public string DistrictIds
        {
            get
            {
                return string.Join(",", Districts.Select(c => c.Id));
            }
        }


        public ObservableRangeCollection<WardModel> Wards;

        public void AddWards(params WardModel[] wards)
        {
            Wards.Clear();
            Wards.AddRange(wards);
            OnPropertyChanged("WardSelectedString");
        }

        public string WardSelectedString
        {
            get
            {
                return string.Join(", ", Wards.Select(c => c.Name));
            }
        }

        public string WardIds
        {
            get
            {
                return string.Join(",", Wards.Select(c => c.Id));
            }
        }

        private ObservableRangeCollection<StreetModel> street;
        public ObservableRangeCollection<StreetModel> StreetSelected
        {
            get
            {
                return street;
            }
            set
            {
                SetProperty(ref street, value);
            }
        }

        private int sleepRoomSelected;
        public int SleepRoomSelected
        {
            get
            {
                return sleepRoomSelected;
            }
            set
            {
                SetProperty(ref sleepRoomSelected, value);
            }
        }

        private ObservableRangeCollection<RealTypeModel> realType;
        public ObservableRangeCollection<RealTypeModel> RealTypes
        {
            get
            {
                return realType;
            }
            set
            {
                SetProperty(ref realType, value);
                OnPropertyChanged("LabelRealTypes");
            }
        }

        private double fromAcreage;
        public double FromAcreage
        {
            get
            {
                return fromAcreage;
            }
            set
            {
                SetProperty(ref fromAcreage, value);
                OnPropertyChanged("AcreageString");
            }
        }

        private double toAcreage;
        public double ToAcreage
        {
            get
            {
                return toAcreage;
            }
            set
            {
                SetProperty(ref toAcreage, value);
                OnPropertyChanged("AcreageString");
            }
        }

        public string AcreageString
        {
            get
            {
                if (ToAcreage <= 0)
                {
                    return "Tất cả";
                }
                return string.Format("{0} - {1} m2", FromAcreage, ToAcreage);
            }
        }


        private double fromPrice;
        public double FromPrice
        {
            get
            {
                return fromPrice;
            }
            set
            {
                SetProperty(ref fromPrice, value);
                OnPropertyChanged("PriceString");
            }
        }

        private double toPrice;
        public double ToPrice
        {
            get
            {
                return toPrice;
            }
            set
            {
                SetProperty(ref toPrice, value);
                OnPropertyChanged("PriceString");
            }
        }

        public string PriceString
        {
            get
            {
                if (ToPrice < 0)
                {
                    return string.Format("{0}++", FromPrice.ToPrice());
                }
                else if (ToPrice > 0)
                {
                    return string.Format("{0} - {1}", FromPrice.ToPrice(), ToPrice.ToPrice());
                }
                else
                {
                    return "Tất cả";
                }
            }
        }

        public string LabelRealTypes
        {
            get
            {
                return string.Join(", ", RealTypes.Select(r => r.Title));
            }
        }

        private ObservableCollection<string> rooms;
        public ObservableCollection<string> Rooms
        {
            get
            {
                return rooms;
            }
            set
            {
                SetProperty(ref rooms, value);
            }
        }
    }
}
