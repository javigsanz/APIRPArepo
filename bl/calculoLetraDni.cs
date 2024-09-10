using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;

namespace APIRPA.bl
{
    public class calculoLetraDni
    {
        public bool CalculoLetraDni(string numeroDni)
        {
            bool ok = false;
            if (numeroDni.Length == 9)
            {
                string numeroSinLetra = numeroDni.Remove(numeroDni.Length - 1, 1);
                string letraDni = numeroDni.Remove(0, 8);
                int dniSinLetra = Int32.Parse(numeroSinLetra);
                int calculoLetra = dniSinLetra % 23;
                string letraCalculada = "";

                switch (calculoLetra)
                {
                    case 0:
                        letraCalculada = "T";
                        break;
                    case 1:
                        letraCalculada = "R";
                        break;
                    case 2:
                        letraCalculada = "W";
                        break;
                    case 3:
                         letraCalculada = "A";
                        break;
                    case 4:
                        letraCalculada = "G";
                        break;
                    case 5:
                        letraCalculada = "M";
                        break;
                    case 6:
                        letraCalculada = "Y";
                        break;
                    case 7:
                        letraCalculada = "F";
                        break;
                    case 8:
                        letraCalculada = "P";
                        break;
                    case 9:
                        letraCalculada = "D";
                        break;
                    case 10:
                        letraCalculada = "X";
                        break;
                    case 11:
                        letraCalculada = "B";
                        break;
                    case 12:
                        letraCalculada = "N";
                        break;
                    case 13:
                        letraCalculada = "J";
                        break;
                    case 14:
                        letraCalculada = "Z";
                        break;
                    case 15:
                        letraCalculada = "S";
                        break;
                    case 16:
                        letraCalculada = "Q";
                        break;
                    case 17:
                        letraCalculada = "V";
                        break;
                    case 18:
                        letraCalculada = "H";
                        break;
                    case 19:
                        letraCalculada = "L";
                        break;
                    case 20:
                        letraCalculada = "C";
                        break;
                    case 21:
                        letraCalculada = "K";
                        break;
                    case 22:
                        letraCalculada = "E";
                        break;

                }

                if (letraCalculada == letraDni)
                {
                    ok = true;
                }
                else
                {
                    ok = false;
                }

            }
            else
            {
                // ! LOGITUD DEL NUMERO DE DNI INCORRECTA
            }
            return ok;
        }
    }
}