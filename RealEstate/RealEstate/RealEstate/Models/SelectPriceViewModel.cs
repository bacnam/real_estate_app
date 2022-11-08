using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using RealEstate.ViewModels;
using Xamarin.Forms;

namespace RealEstate.Models
{
    public class SelectPriceViewModel : BaseViewModel
    {
        public SelectPriceViewModel(bool isSale)
        {
            OtherCommand = new Command(() => DoOtherCommand());

            prices = new ObservableCollection<PriceModel>();

            if (isSale)
            {
                prices.Add(new PriceModel() { FromPrice = 100000000, ToPrice = 200000000 });
                prices.Add(new PriceModel() { FromPrice = 200000000, ToPrice = 500000000 });
                prices.Add(new PriceModel() { FromPrice = 500000000, ToPrice = 1000000000 });
                prices.Add(new PriceModel() { FromPrice = 1000000000, ToPrice = 2000000000 });
                prices.Add(new PriceModel() { FromPrice = 2000000000, ToPrice = 3000000000 });
                prices.Add(new PriceModel() { FromPrice = 3000000000, ToPrice = 5000000000 });
                prices.Add(new PriceModel() { FromPrice = 5000000000, ToPrice = 10000000000 });
                prices.Add(new PriceModel() { FromPrice = 10000000000, ToPrice = -1 });
            }
            else
            {
                prices.Add(new PriceModel() { FromPrice = 100000, ToPrice = 500000 });
                prices.Add(new PriceModel() { FromPrice = 500000, ToPrice = 1000000 });
                prices.Add(new PriceModel() { FromPrice = 1000000, ToPrice = 2000000 });
                prices.Add(new PriceModel() { FromPrice = 2000000, ToPrice = 5000000 });
                prices.Add(new PriceModel() { FromPrice = 5000000, ToPrice = 10000000 });
                prices.Add(new PriceModel() { FromPrice = 10000000, ToPrice = 20000000 });
                prices.Add(new PriceModel() { FromPrice = 20000000, ToPrice = 30000000 });
                prices.Add(new PriceModel() { FromPrice = 30000000, ToPrice = 50000000 });
                prices.Add(new PriceModel() { FromPrice = 50000000, ToPrice = 100000000 });
                prices.Add(new PriceModel() { FromPrice = 100000000, ToPrice = -1 });
            }
        }

        ObservableCollection<PriceModel> prices;
        public ObservableCollection<PriceModel> Prices
        {
            get { return prices; }
        }

        private bool isOther;
        public bool IsOther
        {
            get
            {
                return isOther;
            }
            set
            {
                SetProperty(ref isOther, value);
            }
        }

        public Command OtherCommand
        {
            get;
        }

        private void DoOtherCommand()
        {
            IsOther = !IsOther;
        }
    }
}
