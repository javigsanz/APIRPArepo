using APIRPA.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APIRPA.models
{
    public class FormularioEmailModel
    {
        [DisplayName("Nombre completo del solicitante")]
        [Required(ErrorMessage = "El Nombre del Solicitante es necesario")]
        [StringLength(30, ErrorMessage = "El Nombre del Solicitante no puede superar los 100 caracteres")]
        public string nombreCompletoSolicitante {  get; set; }

        [DisplayName("Número del DNI/NIE del solicitante")]
        [Required(ErrorMessage = "El Número de DNI del Usuario es necesario")]
        [AllowedDniNie]
        public string nDniSolicitante { get; set; }

        [DisplayName("Dirección de Email UNED del solicitante")]
        [Required(ErrorMessage = "La dirección de Email de la Uned es necesaria")]
        [AllowEmailUnedAlu]
        public string emailUnedSolicitante { get; set; }

        [DisplayName("Contraseña de la cuenta de la UNED")]
        [Required(ErrorMessage = "Contraseña necesaria")]
        [DataType(DataType.Password)]
        public string passSolicitante { get; set; }

        [DisplayName("Dirección de Email personal de recuperación para la cuenta UNED")]
        [Required(ErrorMessage = "La dirección de Email personal es necesaria")]
        [DataType(DataType.EmailAddress)]
        public string emailPersSolicitante { get; set; }

        [DisplayName("Número de télefono personal para asociar a su cuenta de la UNED")]
        // ? [Required]
        // ? [DataType(DataType.PhoneNumber)]
        public string telfPersSolicitante { get; set; }

        [DisplayName("Imagen del reverso del DNI/NIE")]
        [Required(ErrorMessage = "Es obligatorio adjuntar la iamgen solicitada")]
        public HttpPostedFileBase bytesReversoDni {  get; set; }

        //public FormularioEmailModel(string nombre, string dni, string mailuned, string pass, string emailpers, string telfpers, byte[] bytesdni)
        //{
        //    nombreCompletoSolicitante = nombre;
        //    nDniSolicitante = dni;
        //    emailUnedSolicitante = mailuned;
        //    passSolicitante = pass;
        //    emailPersSolicitante = emailpers;
        //    telfPersSolicitante = telfpers;
        //    bytesReversoDni = bytesdni;
        //}
        //public string getPassSolicitante()
        //{
        //    return passSolicitante;
        //}
    }
}