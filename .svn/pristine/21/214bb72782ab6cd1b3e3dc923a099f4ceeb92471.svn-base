using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Web;

namespace APIRPA.bl
{
    public class reseteoPassword
    {
        //public string genPassword()
        //{
        //    int length = 12;
        //    string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-+_¡!¿?&$()*";
        //    Random rdm = new Random();
         
        //    char[] chars = new char[length];
        //    for (int i = 0; i < length; i++)
        //    {
        //        chars[i] = validChars[rdm.Next(validChars.Length)];
        //    }
        //    return new string(chars);
        //}       
        public string genPass()
        {
            using (var cryptoProvider = new RNGCryptoServiceProvider())
            {
                byte[] bytes = new byte[12];
                cryptoProvider.GetBytes(bytes);

                string secureRandomString = Convert.ToBase64String(bytes);  
                return secureRandomString;

            }
        }
       
        
    }
}