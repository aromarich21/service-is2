using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace modue_integration.Models
{
    public class JavaScript
    {
        static string scriptTag = "<script type=\"\" language=\"\">{0}</script>";
        public void ConsoleLog(string message)
        {
            string function = "console.log('{0}');";
            string log = string.Format(GenerateCodeFromFunction(function), message);
            HttpContext.Current.Response.Write(log);
        }
        public void Alert(string message)
        {
            string function = "alert('{0}');";
            string log = string.Format(GenerateCodeFromFunction(function), message);
            HttpContext.Current.Response.Write(log);
        }
        string GenerateCodeFromFunction(string function)
        {
            return string.Format(scriptTag, function);
        }
    }
}