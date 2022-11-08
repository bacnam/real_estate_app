using System;
using System.Collections.Generic;
using System.Text;
using RealEstate.Extensions;

namespace RealEstate.Models
{
    public class PriceModel : BaseModel
    {
        public PriceModel()
        {
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
    }
}
