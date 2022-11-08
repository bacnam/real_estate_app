using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Models
{
    public class CalculatorModel : BaseModel
    {
        double[] materialPrices = { 3000000, 4800000, 5100000, 5500000, 5700000 };
        double[] roofPrices = { 0, 0.3, 0.5, 0.7 };
        double[] substructurePrices = { 0, 0, 0.5, 0.7 };

        public CalculatorModel()
        {
            Roof = new SelectionModel() { Id = 0, Name = "Loại chọn loại mái" };
            Substructure = new SelectionModel() { Id = 0, Name = "Loại chọn loại móng" };
            Material = new SelectionModel() { Id = 0, Name = "Loại chọn loại vật tư" };
        }

        //Mái nhà
        public double RoofPrice
        {
            get { return roofPrices[Roof.Id] * materialPrices[0] * Width * Height; }
        }

        //Móng nhà
        public double SubstructureFee
        {
            get { return substructurePrices[Substructure.Id] * materialPrices[0] * Width * Height; }
        }

        public double MaterialPrice
        {
            get { return materialPrices[Material.Id]; }
        }

        //Mái nhà
        private SelectionModel roof;
        public SelectionModel Roof
        {
            get
            {
                return roof;
            }
            set
            {
                SetProperty(ref roof, value);
            }
        }

        //Móng nhà
        private SelectionModel substructure;
        public SelectionModel Substructure
        {
            get
            {
                return substructure;
            }
            set
            {
                SetProperty(ref substructure, value);
            }
        }

        //Vật tư
        private SelectionModel material;
        public SelectionModel Material
        {
            get
            {
                return material;
            }
            set
            {
                SetProperty(ref material, value);
            }
        }

        double width;
        public double Width
        {
            get
            {
                return width;
            }
            set
            {
                SetProperty(ref width, value);
            }
        }

        double height;
        public double Height
        {
            get
            {
                return height;
            }
            set
            {
                SetProperty(ref height, value);
            }
        }

        int floor;
        public int Floor
        {
            get
            {
                return floor;
            }
            set
            {
                SetProperty(ref floor, value);
            }
        }

        public double Acreage
        {
            get { return Height * Width * Floor; }
        }

        public double Fee
        {
            get { return Acreage * MaterialPrice + RoofPrice; }
        }

        public double TotalFee
        {
            get { return SubstructureFee + Fee; }
        }
    }
}
