using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace PetShop.Validations
{
    public class PhoneRule : ValidationRule
    {
        double min;
        double max;
        public double Min { get => min; set => min = value; }
        public double Max { get => max; set => max = value; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double numVal = 0.0;
            if (!double.TryParse(value.ToString(), out numVal))
            {
                return new ValidationResult(false, "Phone Number is not a number");
            }
            if (numVal < min || numVal > max)
            {
                return new ValidationResult(false, string.Format("Phone Number should be 10 digits from {0} to {1}.", min, max));
            }
            return ValidationResult.ValidResult;
        }
    }
}
