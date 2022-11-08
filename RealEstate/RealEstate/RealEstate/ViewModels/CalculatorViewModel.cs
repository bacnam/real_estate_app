using System;
using System.Collections.Generic;
using System.Text;
using RealEstate.Models;

namespace RealEstate.ViewModels
{
    public class CalculatorViewModel: BaseViewModel
    {
        public CalculatorViewModel()
        {
            CalculatorModel = new CalculatorModel();
        }

        CalculatorModel calculatorModel;
        public CalculatorModel CalculatorModel
        {
            get
            {
                return calculatorModel;
            }
            set
            {
                SetProperty(ref calculatorModel, value);
            }
        }

        public bool CanDone()
        {
            return CalculatorModel.Floor > 0 && CalculatorModel.Width > 0 &&
                CalculatorModel.Height > 0 && CalculatorModel.Roof.Id > 0
                && CalculatorModel.Substructure.Id > 0
                && CalculatorModel.Material.Id > 0;
        }
    }
}
