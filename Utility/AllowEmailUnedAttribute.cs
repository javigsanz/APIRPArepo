using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APIRPA.Utility
{
    public class AllowEmailUnedAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var emailUnedSolicitante = value as string;
            string extEmailInvi = "invi.uned.es";
            string extEmailCol = "col.uned.es";

            if(!emailUnedSolicitante.Contains(extEmailCol))
            {
                if(!emailUnedSolicitante.Contains(extEmailInvi))
                {
                    return new ValidationResult($"El correo proporcionado no pretenece a la organización");
                }
                else // ! COINCIDE CON EXTENSION CUENTA INVI
                {
                    return ValidationResult.Success;
                }

            }
            else // ! COINCIDE CON EXTENSION CUENTA COL
            {
                return ValidationResult.Success;
            }
        }
    }
}