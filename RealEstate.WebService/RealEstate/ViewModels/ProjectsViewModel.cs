using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using RealEstate.Models;
using Xamarin.Forms;

namespace RealEstate.ViewModels
{
    public class ProjectsViewModel : BaseViewModel
    {
        public ProjectsViewModel()
        {
            Cities = new ObservableRangeCollection<CityModel>();
            Districts = new ObservableRangeCollection<DistrictModel>();

            InitSelected();
        }

        private void InitSelected()
        {
            AddCities(new CityModel()
            {
                Name = "Tất cả"
            });
            AddDistricts(new DistrictModel()
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
    }
}
