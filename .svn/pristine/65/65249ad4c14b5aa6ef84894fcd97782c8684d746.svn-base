using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace APIRPA.Utility
{
    public class AllowedIdUnedAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var idUnedSolicitante = value as string;
            Regex regexInvi = new Regex("^col[0-9]{5}$", RegexOptions.None, TimeSpan.FromMilliseconds(500));
            Regex regexCol = new Regex ("^invi[0-9]{4}$", RegexOptions.None, TimeSpan.FromMilliseconds(500));
            //AppDomain.CurrentDomain.SetData("REGEX_DEFAULT_MATCH_TIMEOUT", TimeSpan.FromMilliseconds(500));

            if (!regexCol.IsMatch(idUnedSolicitante.ToLower()))
            {
                if(!regexInvi.IsMatch(idUnedSolicitante.ToLower()))
                {
                    return new ValidationResult($"El Identificador proporcionado no es INVI ni COL");
                }
                else // ! COINCIDE ID INVI
                {
                    return ValidationResult.Success;
                }
            }
            else // ! COINCIDE ID COL
            {
                return ValidationResult.Success;
            }
        }
    }
}