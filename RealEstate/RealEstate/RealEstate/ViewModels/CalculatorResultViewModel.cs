using System;
using System.Collections.Generic;
using System.Text;
using RealEstate.Models;

namespace RealEstate.ViewModels
{
    public class CalculatorResultViewModel : BaseViewModel
    {
        public CalculatorResultViewModel(CalculatorModel calculatorModel)
        {
            CalculatorModel = calculatorModel;
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
    }
}
