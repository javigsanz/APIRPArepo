using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APIRPA.Utility
{
    public class AllowEmailUnedAluAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var emailUnedAluSolicitante = value as string;
            string extEmailAlu = "alumno.uned.es";

            if(!emailUnedAluSolicitante.Contains(extEmailAlu))
            {
                return new ValidationResult($"El correo proporcionado no pertenece a la organización");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}