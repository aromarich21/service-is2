using modue_integration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Timer = System.Web.UI.Timer;

namespace modue_integration
{
    public partial class Error : System.Web.UI.Page
    {
        public string prevPage = "Default.aspx";
        public void RedirectToMain() //редирект на home
        {
            Response.Redirect("Default.aspx");
        }
        public void RedirectToPrevPage(string page) //редирект на prevPage
        {
            Response.Redirect(page);
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
            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[nameCookie];
                var inputBytes = Convert.FromBase64String(cookie.Value);
                string content = Encoding.GetEncoding("Windows-1251").GetString(inputBytes);
                return content;
            }
            catch
            {
                return prevPage;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            prevPage = ReadCookie("CurrentPage");
            Timer1.Interval = 5000;
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //RedirectToMain();
            RedirectToPrevPage(prevPage);
        }
    }
}