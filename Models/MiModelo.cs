using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIRPA.models
{
    public class MiModelo
    {
        public string idSolicitud { get; set; }
        public string nombrePDF { get; set; }
        public string Firmante { get; set; }
        public byte[] bytesPDF { get; set; }
    }
}