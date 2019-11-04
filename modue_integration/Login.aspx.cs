using modue_integration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace modue_integration
{
    public partial class Login : System.Web.UI.Page
    {
        public string login = "root";
        public string password = "toor";
        public string auth_ = "none";

        public void CreateCookieAuth(string value)
        {
            var nameCookie = "auth_";
            // Создать объект cookie-набора
            HttpCookie cookie = new HttpCookie(nameCookie);
            // Установить значения в нем
            cookie.Value = value;
            var bytes = Encoding.GetEncoding(1251).GetBytes(cookie.Value);
            cookie.Value = Convert.ToBase64String(bytes);
            cookie.Expires = DateTime.Now.AddMinutes(30);
            // Добавить куки в ответ
            Response.Cookies.Add(cookie);
        }
        public void Authorization()
        {

            if (TextBox1.Text == login)
                if (TextBox2.Text == password)
                {
                    var pointLogIn = "access";
                    Label1.Visible = false;
                    CreateCookieAuth(pointLogIn);
                }
                else
                {
                    Label1.Visible = true;
                    JavaScript js = new JavaScript();
                    js.ConsoleLog(Label1.Text);
                    auth_ = "none";
                }
            else
            {
                Label1.Visible = true;
                JavaScript js2 = new JavaScript();
                js2.ConsoleLog(Label1.Text);
                auth_ = "none";
            }
        }
        public void ReadCookieAuth()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["auth_"];
            if (cookie != null)
            {
                var inputBytes = Convert.FromBase64String(cookie.Value);
                auth_ = Encoding.GetEncoding("Windows-1251").GetString(inputBytes);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ReadCookieAuth();
            if (auth_ == "access")
                //Server.Transfer("module.aspx");
                Response.Redirect("module.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Authorization();
            ReadCookieAuth();
            if (auth_ == "access")
                //Server.Transfer("module.aspx");
                Response.Redirect("module.aspx");
        }
       
    }
}