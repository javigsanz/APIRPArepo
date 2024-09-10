using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;


namespace APIRPA.Controllers
{
    public class Config
    {
        public enum EnumEntorno
        {
            DESARROLLO,
            PREPRODUCCION,
            STAGE,
            PRODUCCION
        }
        public static EnumEntorno Entorno { get; }

        // ! INICIALIZAMOS FIRMAENDPOINT QUE MAS TARDE ACCEDEREMOS DESDE VALUES CONTROLLER CON Config.FirmaEndpoint
        public static string FirmaEndPoint { get; }
        static Config()
        {

            // ?  NameValueCollection globalSettings = (NameValueCollection)ConfigurationManager.GetSection("ConfigSettings/GlobalSettings");
            // ?  NameValueCollection globalSettings = new NameValueCollection();

            // ? globalSettings = (NameValueCollection)ConfigurationManager.GetSection("GlobalSettings");


            NameValueCollection entornoSettings = new NameValueCollection();

            // ! Si estamos en local configuramos como desarrollo, si no cogemos la configuración del IIS
            if (HttpContext.Current.Request.Url.AbsoluteUri.Contains("localhost"))
            {
                Entorno = EnumEntorno.DESARROLLO;
                //Entorno = EnumEntorno.PRODUCCION;
            }
            else
            {
                if (ConfigurationManager.AppSettings["ENTORNO"] == null)
                    // TODO CAMBIAR A ENTORNO DE PRODUCCION
                    Entorno = EnumEntorno.PRODUCCION;
                else
                {
                    //Esta variable de entorno se lee del application settings del IIS

                    switch (ConfigurationManager.AppSettings["ENTORNO"].ToString())
                    {
                        case "DESARROLLO":
                            Entorno = EnumEntorno.DESARROLLO;
                            break;
                        case "PREPRODUCCION":
                            Entorno = EnumEntorno.PREPRODUCCION;
                            break;
                        case "STAGE":
                            Entorno = EnumEntorno.STAGE;
                            break;
                        default:
                            Entorno = EnumEntorno.PRODUCCION;
                            break;
                    }
                }
            }
            // switch (Entorno)
            switch (Entorno)
            {
                case EnumEntorno.DESARROLLO:
                    entornoSettings = (NameValueCollection)ConfigurationManager.GetSection("ConfigSettings/DesarrolloSettings");
                    break;
                case EnumEntorno.PREPRODUCCION:
                    entornoSettings = (NameValueCollection)ConfigurationManager.GetSection("ConfigSettings/PreProduccionSettings");
                    break;
                case EnumEntorno.STAGE:
                    entornoSettings = (NameValueCollection)ConfigurationManager.GetSection("ConfigSettings/StageSettings");
                    break;
                case EnumEntorno.PRODUCCION:
                    entornoSettings = (NameValueCollection)ConfigurationManager.GetSection("ConfigSettings/ProduccionSettings");
                    break;
                default:
                    entornoSettings = (NameValueCollection)ConfigurationManager.GetSection("ConfigSettings/ProduccionSettings");
                    break;
            }

            FirmaEndPoint = entornoSettings["FirmaEndPoint"].ToString();       
            
            /*
            SeguridadActiva = bool.Parse(globalSettings["SeguridadActiva"].ToString());
            IDapp = globalSettings["IDapp"].ToString();            
            DBService = (DalService.ServicioEnum)Enum.Parse(typeof(DalService.ServicioEnum), entornoSettings["DBService"].ToString());
            DBLogin = entornoSettings["DBLogin"].ToString();
            DBPassword = entornoSettings["DBPassword"].ToString();
            DalEndPoint = entornoSettings["DalEndPoint"].ToString();
            AuthAppEndPoint = entornoSettings["AuthAppEndPoint"].ToString();
            */

        }

        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;

        }
    }

}

