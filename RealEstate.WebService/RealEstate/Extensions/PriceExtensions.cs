using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Extensions
{
    public static class PriceExtension
    {
        public static string ToPrice(this double value)
        {
            string priceString = "";
            double ty = value / 1000000000;
            double tr = (value % 1000000000) / 1000000;
            double ngan = ((value % 1000000000) % 1000000) / 1000;
            double dong = ((value % 1000000000) % 1000000) % 1000;
            string unit = "";
            if (ty > 100)
            {
                priceString = string.Format("{0:F0}", ty);
                unit = "tỷ";
            }
            else if (ty >= 1)
            {
                if (tr > 0)
                {
                    priceString = string.Format("{0:F1}", ty);
                }
                else
                {
                    priceString = string.Format("{0:F0}", ty);
                }
                unit = "tỷ";
            }
            else if (tr > 100)
            {
                priceString = string.Format("{0:F0}", tr);
                unit = "triệu";
            }
            else if (tr >= 1)
            {
                if (ngan > 0)
                {
                    priceString = string.Format("{0:F1}", tr);
                }
                else
                {
                    priceString = string.Format("{0:F0}", tr);
                }
                unit = "triệu";
            }
            else if (ngan > 100)
            {
                priceString = string.Format("{0:F0}", ngan);
                unit = "ngàn";
            }
            else if (ngan >= 1)
            {
                if (dong > 0)
                {
                    priceString = string.Format("{0:F1}", ngan);
                }
                else
                {
                    priceString = string.Format("{0:F0}", ngan);
                }
                unit = "ngàn";
            }
            else
            {
                priceString = string.Format("{0:F0}", dong);
                unit = "đồng";
            }
            return priceString += " " + unit;
        }

        public static string GetFullPrice(this double price, string type)
        {
            string fullPrice = "";
            double ty = price / 1000000000;
            double tr = (price % 1000000000) / 1000000;
            double ngan = ((price % 1000000000) % 1000000) / 1000;
            double dong = ((price % 1000000000) % 1000000) % 1000;

            if (ty > 100)
            {
                fullPrice = string.Format("{0:F0} tỷ", ty);
            }
            else if (ty >= 1)
            {
                fullPrice = string.Format("{0:F1} tỷ", ty);
            }
            else if (tr > 100)
            {
                fullPrice = string.Format("{0:F0} triệu", tr);
            }
            else if (tr >= 1)
            {
                fullPrice = string.Format("{0:F1} triệu", tr);
            }
            else if (ngan > 100)
            {
                fullPrice = string.Format("{0:F0} ngàn", ngan);
            }
            else if (ngan >= 1)
            {
                fullPrice = string.Format("{0:F1} ngàn", ngan);
            }
            else if (dong > 100)
            {
                fullPrice = string.Format("{0:F0} đồng", dong);
            }
            else
            {
                fullPrice = string.Format("{0:F1} đồng", dong);
            }
            if (string.IsNullOrEmpty(type))
            {
                fullPrice = "";
            }
            else if (type.Equals("Liên hệ", StringComparison.OrdinalIgnoreCase))
            {
                fullPrice = "Liên hệ";
            }
            else if (type.Equals("tháng"))
            {
                fullPrice += "/th";
            }
            else if (type.Equals("m2"))
            {
                fullPrice += "/m2";
            }
            return fullPrice;
        }

        public static string GetPriceUnit(double price, string type)
        {
            string unit = "";
            double ty = price / 1000000000;
            double tr = (price % 1000000000) / 1000000;
            double ngan = ((price % 1000000000) % 1000000) / 1000;

            if (ty >= 1)
            {
                unit = "tỷ";
            }
            else if (tr >= 1)
            {
                unit = "triệu";
            }
            else if (ngan >= 1)
            {
                unit = "ngàn";
            }
            else
            {
                unit = "đồng";
            }
            if (type.Equals("tháng"))
            {
                unit += "/th";
            }
            else if (type.Equals("m2"))
            {
                unit += "/m2";
            }
            else
            {
                unit = "";
            }
            return unit;
        }

        public static string GetPriceString(double price, string type = "")
        {
            string priceString = "";
            double ty = price / 1000000000;
            double tr = (price % 1000000000) / 1000000;
            double ngan = ((price % 1000000000) % 1000000) / 1000;
            double dong = ((price % 1000000000) % 1000000) % 1000;
            string unit = "";
            if (ty > 100)
            {
                priceString = string.Format("{0:F0}", ty);
                unit = "tỷ";
            }
            else if (ty >= 1)
            {
                priceString = string.Format("{0:F1}", ty);
                unit = "tỷ";
            }
            else if (tr > 100)
            {
                priceString = string.Format("{0:F0}", tr);
                unit = "triệu";
            }
            else if (tr >= 1)
            {
                priceString = string.Format("{0:F1}", tr);
                unit = "triệu";
            }
            else if (ngan > 100)
            {
                priceString = string.Format("{0:F0}", ngan);
                unit = "ngàn";
            }
            else if (ngan >= 1)
            {
                priceString = string.Format("{0:F1}", ngan);
                unit = "ngàn";
            }
            else if (dong > 100)
            {
                priceString = string.Format("{0:F0}", dong);
                unit = "đồng";
            }
            else
            {
                priceString = string.Format("{0:F1}", dong);
                unit = "đồng";
            }
            if (type.Equals("Liên hệ", StringComparison.OrdinalIgnoreCase))
            {
                priceString = "Liên hệ";
            }
            else if (type.Equals("Tổng", StringComparison.OrdinalIgnoreCase))
            {
                priceString += " " + unit;
            }
            else
            {
                priceString += " " + unit;
            }
            return priceString;
        }

        public static double ConvertPrice(this double price, string unitPrice)
        {
            double _price = 0;

            if (unitPrice.Equals("Tỷ", StringComparison.OrdinalIgnoreCase))
            {
                _price = price * 1000000000;
            }
            else if (unitPrice.Equals("Triệu", StringComparison.OrdinalIgnoreCase))
            {
                _price = price * 1000000;
            }
            else if (unitPrice.Equals("Ngàn", StringComparison.OrdinalIgnoreCase))
            {
                _price = price * 1000;
            }
            else
            {
                _price = price;
            }

            return _price;
        }
    }
}
