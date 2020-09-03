using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace PetShop.Validations
{
    public class PetIdRule : ValidationRule
    {
        double min;
        double max;
        public double Min { get => min; set => min = value; }
        public double Max { get => max; set => max = value; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int numVal;// = null;
            if (!Int32.TryParse(value.ToString(), out numVal))
            {
                return new ValidationResult(false, "Pet ID is not a number");
            }
            if (numVal < min || numVal > max)
            {
                return new ValidationResult(false, string.Format("Pet ID is between {0} and {1}.", min, max));
            }
            return ValidationResult.ValidResult;
        }
    }
}
