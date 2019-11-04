using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace modue_integration
{
    public partial class Cookies : System.Web.UI.Page
    {
        string nameFile = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            nameFile= FileUpload1.FileName;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            string company="";
            string age= "";

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(FileUpload1.FileContent);
            // получим корневой элемент
            XmlElement xRoot = xDoc.DocumentElement;
            // обход всех узлов в корневом элементе
            foreach (XmlNode xnode in xRoot)
            {
                // получаем атрибут name
                if (xnode.Attributes.Count > 0)
                {
                    XmlNode attr = xnode.Attributes.GetNamedItem("name");
                    if (attr != null)
                        Console.WriteLine(attr.Value);
                }
                // обходим все дочерние узлы элемента user
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    // если узел - company
                    if (childnode.Name == "company")
                    {
                        company = childnode.InnerText;
                    }
                    // если узел age
                    if (childnode.Name == "age")
                    {
                        age = childnode.InnerText;
                    }
                }      
            }

            // Создать объект cookie-набора
            HttpCookie cookie = new HttpCookie("attributes");

            // Установить значения в нем
            cookie["Company"] = company;
            cookie["Age"] = age;

            // Добавить куки в ответ
            Response.Cookies.Add(cookie);
        }
    

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}