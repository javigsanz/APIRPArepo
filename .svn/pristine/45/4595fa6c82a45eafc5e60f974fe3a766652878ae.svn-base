using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace APIRPA.models
{
    public class FormularioDniModel
    {
        [DisplayName("Imagen reverso del DNI")] 
        [Required(ErrorMessage = "Es obligatorio adjuntar la imagen solicitada")]
        public HttpPostedFileBase bytesReversoDni { get; set; }

        public byte[] arrayBytesDni { get; set; }
    }

    public class FormularioDniEditModel
    {
        public HttpPostedFileBase bytesReversoDni { get; set; }
    }
}