using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace APIRPA.models
{
    public class MrzModelo
    {     
        public string tipoDocumento {  get; set; }
        public string paisEmisor {  get; set; }
        public string nSerieSopFisico { get; set; }
        public string digitoControl1 { get; set; }
        public string numeroDni { get; set; }
        public string fechaNacimiento { get; set; }
        public string fechaNacFormateada { get; set; }
        public string digitoControl2 { get; set; }
        public string sexo {  get; set; }
        public string fechaCaducidad { get; set; }
        public string fechaCadFormateada { get; set; }
        public string digitoControl3 { get; set; }
        public string nacionalidad { get; set; }
        public string digitoControl4 { get; set; }
        public string nombreCompleto {  get; set; }      

    }
}