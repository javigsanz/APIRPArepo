using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIRPA.bl
{
    public class EncryptPass
    {
        private string makeEncrypt(string pass)
        {
            if (pass.IsNullOrWhiteSpace())
            {
                return null;
            }
            else
            {

                return "";
            }
        }
    }
}