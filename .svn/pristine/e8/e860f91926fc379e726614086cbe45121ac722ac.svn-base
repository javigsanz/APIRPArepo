using APIRPA.DalService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIRPA.core
{
    public class DalHelper
    {
        public static ConnectionString MiConnectionString { get; }

        static DalHelper()
        {
            MiConnectionString = new ConnectionString
            {
                Servicio = Config.DBService,
                Login = Config.DBLogin,
                Password = Config.DBPassword
            };
        }

        public static TipoParametroOracle PonerParametros(string nombre,
            TipoOracle tipo,
            ParameterDirection direccion,
            object valor = null,
            int tamano = 0)
        {
            TipoParametroOracle parametro = new TipoParametroOracle();
            parametro.Nombre = nombre;
            parametro.Valor = valor;
            parametro.Tipo = tipo;
            parametro.Size = tamano;
            parametro.Direccion = direccion;
            return parametro;
        }     
    }       
}
