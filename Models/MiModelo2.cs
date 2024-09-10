using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace APIRPA.models
{
    public class MiModelo2
    {
        public string nombreCompletoSolicitante { get; set; }
        //public string idUnedSolicitante { get; set; }
        public string dniSolicitante { get; set; }
        public string emailSolicitante { get; set; }
        public string passNueva { get; set; }
        public string emailRecupSolicitante { get; set; }
        public string telfPersSolicitante { get; set; }
        public byte[] bytesDNI { get; set; }        
        public string linea1Mrz { get; set; }
        public string linea2Mrz { get; set; }
       
    }
}