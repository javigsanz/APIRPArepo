using APIRPA.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Windows.Forms.VisualStyles;

namespace APIRPA.bl
{
    public class DigitosControl
    {
        static Dictionary<char, int> valoresLetras = new Dictionary<char, int>
        {
            {'a', 10}, {'b', 11}, {'c', 12}, {'d', 13}, {'e', 14}, {'f', 15},
            {'g', 16}, {'h', 17}, {'i', 18}, {'j', 19}, {'k', 20}, {'l', 21},
            {'m', 22}, {'n', 23}, {'o', 24}, {'p', 25}, {'q', 26}, {'r', 27},
            {'s', 28}, {'t', 29}, {'u', 30}, {'v', 31}, {'w', 32}, {'x', 33},
            {'y', 34}, {'z', 35}
        };
        static Dictionary<char, int> valoresLetrasDni = new Dictionary<char, int>
        {
            {'T', 0}, {'R', 1}, {'W', 2}, {'A', 3}, {'G', 4}, {'M', 5}, {'Y', 6}, {'F',7}, {'P', 8}, {'D', 9},
            {'X', 10}, {'B', 11}, {'N',12}, {'J', 13}, {'Z', 14}, {'S', 15}, {'Q', 16}, {'V', 17}, {'H',18}, {'L', 19},
            {'C', 20}, {'K', 21 }, {'E',22}
        };
        
        bool control1; // CONTROLA EL NUMERO DE SOPORTE
        bool control2; // CONTROLA LA FECHA DE NACIMIENTO
        bool control3; // CONTROLA LA FECHA DE CADUCIDAD
        bool control4; // CONTROLA LOS 3 CAMPOS ANTERIORES CONTATENADOS
        bool controlNDni;
        public bool isControled(MrzModelo mrzNoControled)
        {
            // ! COMPROBACION LETRA DNI
            string dniSinLetra = mrzNoControled.numeroDni.Substring(0, 8); // ! COGEMOS LOS NUMEROS SIN LETRA
            string letraDni = mrzNoControled.numeroDni.Substring(8, 1);
            int dniInt = Int32.Parse(dniSinLetra); // ! STRING -> INT
            var restoDni = dniInt % 23;

            // TODO METER UN IF 
            controlNDni = ObtenerLetraDni(restoDni);

            // ! COMPROBACIÓN CONTROL1
            char[] nSopFisico = mrzNoControled.nSerieSopFisico.ToCharArray();
            List<int> nSopFisicointsList = new List<int>();
            int suma731D1 = 0;
            List<int> mult371D1 = new List<int>();
            foreach(char c in nSopFisico)
            {
                char letraMinuscula = char.ToLower(c);
                int letraEnNumero;
                if (valoresLetras.ContainsKey(letraMinuscula))
                {
                    letraEnNumero = valoresLetras[letraMinuscula];
                    nSopFisicointsList.Add(letraEnNumero);
                }
                else
                {
                    nSopFisicointsList.Add((int)char.GetNumericValue(c));
                }
            }
            for(int i = 0; i < nSopFisicointsList.Count; i++)
            {
                mult371D1.Add( nSopFisicointsList[i] * 7);
                i++;
                mult371D1.Add(nSopFisicointsList[i] * 3);
                i++;
                mult371D1.Add(nSopFisicointsList[i] * 1);                

            }
            for (int i = 0; i < mult371D1.Count; i++)
            {
                suma731D1 = suma731D1 + mult371D1[i];
            }
            var resto731D1 = suma731D1 % 10;
            if (resto731D1.ToString().Equals(mrzNoControled.digitoControl1))
            {
                control1 = true;
                suma731D1 = 0; // RESETEAMOS LA VARIABLE YA QUE LA VAMOS A REUTILIZAR PARA LOS SIGUIENTES DIGITOS DE CONTROL
                
            }
            // ! COMPROBACIÓN CONTROL2
            char[] fechaNacimiento = mrzNoControled.fechaNacimiento.ToCharArray();
            
            int suma731D2 = 0;
            List<int> mult731D2 = new List<int>();
            for(int i = 0; i < fechaNacimiento.Length; i++)
            {
                var aux = fechaNacimiento[i];
                var f = (int)char.GetNumericValue(fechaNacimiento[i]);

                mult731D2.Add((int)char.GetNumericValue(fechaNacimiento[i]) * 7);
                i++;
                mult731D2.Add((int)char.GetNumericValue(fechaNacimiento[i]) * 3);
                i++;
                mult731D2.Add((int)char.GetNumericValue(fechaNacimiento[i]) * 1);
            }
            for(int i = 0; i < mult731D2.Count; i++)
            {
                suma731D2 = suma731D2 + mult731D2[i];
            }
            var resto731D2 = suma731D2 % 10;
            if (resto731D2.ToString().Equals(mrzNoControled.digitoControl2))
            {
                control2 = true;
                suma731D2 = 0;
            }

            // ! COMPROBACIÓN CONTROL3
            char[] fechaCaducidad = mrzNoControled.fechaCaducidad.ToCharArray();
            int suma731D3 = 0;
            List<int> mult731D3 = new List<int>();
            for(int i = 0; i < fechaCaducidad.Length; i++ )
            {
                mult731D3.Add((int)char.GetNumericValue(fechaCaducidad[i]) * 7);
                i++;
                mult731D3.Add((int)char.GetNumericValue(fechaCaducidad[i]) * 3);
                i++;
                mult731D3.Add((int)char.GetNumericValue(fechaCaducidad[i]) * 1);
            }
            for(int i = 0; i < mult731D3.Count; i++)
            {
                suma731D3 = suma731D3 + mult731D3[i];
            }
            var resto731D3 = suma731D3 % 10;
            if(resto731D3.ToString().Equals(mrzNoControled.digitoControl3)) 
            {
                control3 = true;
            }

            // ! COMPROBACIÓN CONTROL 4
            // COMPROBACION CONJUNTA
            // CONCAT( nSOPORTE, DC1, DNI, FECNAC, D2, FECCAD, D3)
            //
            char[] strConcatFinal;
            List<int> strFinal = new List<int>();
            string nSopFisicoInts = "";
            // NUMERO SOPORTE
            for(int i = 0; i< nSopFisicointsList.Count; i++)
            {
                strFinal.Add(nSopFisicointsList[i]);
            }
            // DIGITO CONTROL 1
            strFinal.Add(Int32.Parse(mrzNoControled.digitoControl1));

            // NUMERO DNI
            //char[] dniArray = dniInt.ToString().ToCharArray();
            char[] dniArray = dniSinLetra.ToCharArray();
            for(int i = 0; i < dniArray.Length; i++)
            {
                strFinal.Add((int)char.GetNumericValue(dniArray[i]));
            }
            
            char letraDniChar = letraDni[0];
            var letraDniInt = ObtenerValorLetraDni(letraDniChar);
            if(letraDniInt == -1)
            {
                
            }
            else
            {
                strFinal.Add(letraDniInt);
            }
            // FECHA NACIMIENTO
            for(int i = 0; i < fechaNacimiento.Length; i++)
            {
                strFinal.Add((int)char.GetNumericValue(fechaNacimiento[i]));
            }
            // DIGITO CONTROL 2
            strFinal.Add(Int32.Parse(mrzNoControled.digitoControl2)); 
            // FECHA CADUCIDAD
            for(int i = 0; i < fechaCaducidad.Length; i++)
            {
                strFinal.Add((int)char.GetNumericValue(fechaCaducidad[i]));
            }
            // DIGITO CONTROL 3
            strFinal.Add(Int32.Parse(mrzNoControled.digitoControl3));

            // CALCULO DIGITO 4
            int suma731D4 = 0;
            List<int> mult731D4 = new List<int>();
            for (int i = 0; i < strFinal.Count; i++)
            {
                mult731D4.Add(strFinal[i] * 7); i++;
                mult731D4.Add(strFinal[i] * 3); i++;
                mult731D4.Add(strFinal[i] * 1);
                
            }
            for(int i = 0; i < mult731D4.Count; i++)
            {
                suma731D4 = suma731D4 + mult731D4[i];
            }
            var resto731D4 = suma731D4 % 10;
            if (resto731D4.ToString().Equals(mrzNoControled.digitoControl4))
            {
                control4 = true;
            }          
            
            if(control1 && control2 && control3 && control4 && controlNDni)
            {
                return true;
            }
            else
            {
                return false;
            } 
        }  
        static bool ObtenerLetraDni(int resto)
        {
            foreach(var kvp in valoresLetrasDni)
            {
                if (kvp.Value.Equals(resto))
                {
                    return true;
                }
            }
            return false;
        }

        static int ObtenerValorLetraDni(char letraDni)
        {
            foreach (var kvp in valoresLetras)
            {
                if (kvp.Key.Equals(char.ToLower(letraDni)))
                {
                    return kvp.Value;
                }                
            }
            return -1;
        }
    }
}