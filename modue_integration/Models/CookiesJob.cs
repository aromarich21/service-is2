using modue_integration.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace modue_integration.Models
{
    public class CookiesJob
    {
        public CookiesJob()
        {

        }

        public void SetCookie(string nameCookie, string content) //Создание куки типа [name][value=content]
        {
            var name = nameCookie;
            HttpCookie cookie = new HttpCookie(name); // Создать объект cookie-набора
            cookie.Value = content;  // Установить значения в нем
            var bytes = Encoding.GetEncoding(1251).GetBytes(cookie.Value);
            cookie.Value = Convert.ToBase64String(bytes);
            //Response.Cookies.Add(cookie); // Добавить куки в ответ
        }
        public string ReadCookie(string nameCookie)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[nameCookie];
            var inputBytes = Convert.FromBase64String(cookie.Value);
            string content = Encoding.GetEncoding("Windows-1251").GetString(inputBytes);
            return content;
        }
    }
}