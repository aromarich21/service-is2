using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace modue_integration.Models
{
    public class FileIS2
    {
        public XDocument xDoc { get; set; }
        public bool pointCheckFileScheme { get; set; }
        public bool pointCheckFormatFile { get; set; }
        public bool pointCheckHasFile { get; set; }
        public bool pointError { get; set; }
        public FileIS2()
        {
        }
        public FileIS2(System.Web.UI.WebControls.FileUpload fileUpload)
        {
            CheckHasFile(fileUpload);
            if (pointCheckHasFile == true)
            {
                CheckFileFormat(fileUpload);
                if (pointCheckFormatFile == true)
                {
                    CheckFileScheme(fileUpload);
                    if (pointCheckFileScheme == true)
                        ParseFile(xDoc);
                }
            }
        }
        public string GetFileFormat(System.Web.UI.WebControls.FileUpload fileUpload)
        {
            var format = Path.GetExtension(fileUpload.FileName);
            return format;
        }
        public void CheckFileFormat(System.Web.UI.WebControls.FileUpload fileUpload)
        {
           if (GetFileFormat(fileUpload) == ".is2")
            {
                pointCheckFormatFile = true;
            }
            else
            {
                pointCheckFormatFile = false;
            }
        }
        public void CheckHasFile(System.Web.UI.WebControls.FileUpload fileUpload)
        {
            if (fileUpload.HasFile)
            {
                pointCheckHasFile = true;
            }
            else
            {
                pointCheckHasFile = false;
            }
        }
        public void CheckFileScheme(System.Web.UI.WebControls.FileUpload fileUpload)
        {
            try
            {
                xDoc = XDocument.Load(fileUpload.FileContent);
                var nodeElements = 0;
                var nodeLinks = 0;
                foreach (XElement elements in xDoc.Element("project").Element("models").Elements("primaryModel").Elements("spd").Elements("actions").Elements("pd"))
                {
                    nodeElements++;
                }
                if (nodeElements == 0)
                    pointCheckFileScheme = false;
                else
                    pointCheckFileScheme = true;
                foreach (XElement elements in xDoc.Element("project").Element("models").Elements("primaryModel").Elements("spd").Elements("links").Elements("link"))
                {
                    nodeLinks++;
                }
                if (nodeLinks == 0)
                    pointCheckFileScheme = false;
                else
                    pointCheckFileScheme = true;
            }
            catch 
            {
                pointError = true;
            }
        }
        public bool CheckAllPoints()
        {
            if (pointCheckFileScheme == true && pointCheckFormatFile == true && pointCheckHasFile == true)
                return true;
            else
                return false;
        }
        public void ParseFile(XDocument xDoc)
        {
            var i = 0;
            foreach (XElement elements in xDoc.Element("project").Element("models").Elements("primaryModel").Elements("spd").Elements("actions").Elements("pd"))
            {
                i++;
                XAttribute nameAttribute = elements.Attribute("name");
                XAttribute idAttribute = elements.Attribute("id");
                XAttribute levelAttribute = elements.Attribute("level");
                XAttribute numberAttribute = elements.Attribute("number");
                XAttribute statusAttribute = elements.Attribute("status");
                XAttribute typeAttribute = elements.Attribute("type");
                XAttribute formalizationAttribute = elements.Attribute("formalization");

                if (nameAttribute != null && idAttribute != null)
                {
                    var nameCookie = "Element:" + idAttribute.Value;
                    // Создать объект cookie-набора
                    HttpCookie cookie = new HttpCookie(nameCookie);
                    // Установить значения в нем
                    //cookie["name"] = nameAttribute.Value;
                    //var bytes = Encoding.GetEncoding(1251).GetBytes(nameAttribute.Value);
                    cookie["name"] = nameAttribute.Value;
                    cookie["id"] = idAttribute.Value;
                    cookie["level"] = levelAttribute.Value;
                    cookie["number"] = numberAttribute.Value;
                    cookie["status"] = statusAttribute.Value;
                    cookie["type"] = typeAttribute.Value;
                    cookie["formalization"] = formalizationAttribute.Value;
                    //кодирование
                    var bytes = Encoding.GetEncoding(1251).GetBytes(cookie.Value);
                    cookie.Value = Convert.ToBase64String(bytes);
                    //время куки
                    //cookie.Expires = DateTime.Now.AddSeconds(settingsTimeSecondsCookies);
                    // Добавить куки в ответ
                    //Response.Cookies.Add(cookie);
                }

            }
        }
    } 
}

