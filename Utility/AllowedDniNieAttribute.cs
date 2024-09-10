using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace APIRPA.Utility
{
    public class AllowedDniNieAttribute : ValidationAttribute
    {        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var numDniNie = value as string;
            Regex regexNumDni = new Regex("^[0-9]{8}-[a-zA-Z]{1}$", RegexOptions.None, TimeSpan.FromMilliseconds(500)); // ! 12345678-X
            Regex regexNumNie = new Regex("^[a-zA-Z]{1}-[0-9]{7}-[a-zA-Z]{1}$", RegexOptions.None, TimeSpan.FromMilliseconds(500)); // ! X-1234567-X
            
            if (!regexNumDni.IsMatch(numDniNie))
            {
                if(!regexNumNie.IsMatch(numDniNie))
                {
                    return new ValidationResult($"tanto en el dni como en el nie las letras deben ir" +
                        $"separadas por un guión (-) 12345678-X");
                }
                else // ! COINCIDE NIE
                {
                    return ValidationResult.Success;
                }                
            }
            else // ! COINCIDE DNI
            {
                return ValidationResult.Success;
            }
            

        }

    }
}