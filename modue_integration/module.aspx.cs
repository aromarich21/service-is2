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

namespace modue_integration
{
    public partial class module : System.Web.UI.Page
    {

        public string auth_; //авторизация.состояние для бета теста
        public int settingsTimeSecondsCookies = 1;  ///  cookies_config. время куки
        public bool goodSchemaFile = false; // метка проверки схемы XML
        public List<object> sourceDropDown = new List<object>(); //датасурс для dropdown 
        static public List<Elements> sourceCurElements = new List<Elements>(); //датасурс элементов cur диаграммы
        static public List<Elements> sourceIntElements = new List<Elements>(); //датасурс элементов int диаграммы
        static public List<Links> sourceIntLinks = new List<Links>(); //датасурс связей cur диаграммы
        static public List<Links> sourceCurLinks = new List<Links>(); //датасурс связей int диаграммы
        static public string userChoice; // метка выбора итема dropdownа пользователем
        static public string itemChoiceLevel; // уровень выбранного элемента
        static public string itemChoiceNumber; //номер выбранного элемента
        static public List<Elements> sourceResElements = new List<Elements>(); //датасурс элементов res диаграммы
        static public string defaultNameElement = "defaultName";
        /// function back
        public void RedirectToAuth() //редирект на страницу авторизации
        {
            ReadCookieAuth();
            if (auth_ != "access")
                //Server.Transfer("module.aspx");
                Response.Redirect("Login.aspx");
        }
        public void TestElementsON()
        {
            TextBox4.Visible = true;
            Button3.Visible = true;
        } //отображение на фронте тестовых элементов
        public string GetFormat()
        {
            var format = Path.GetExtension(FileUpload1.FileName);
            return format;
        } //определить формат файла в элементе FileUpload1
        public void Step4ElementON()
        {
            DropDownList1.Visible = true;
            Button4.Visible = true;
        } //vision у dropdown 
        public void SetCookie(string nameCookie, string content) //Создание куки типа [name][value=content]
        {
            var name = nameCookie;             
            HttpCookie cookie = new HttpCookie(name); // Создать объект cookie-набора
            cookie.Value=content;  // Установить значения в нем
            var bytes = Encoding.GetEncoding(1251).GetBytes(cookie.Value);
            cookie.Value = Convert.ToBase64String(bytes);
            Response.Cookies.Add(cookie); // Добавить куки в ответ
        }
        public string ReadCookie(string nameCookie)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[nameCookie];
            var inputBytes = Convert.FromBase64String(cookie.Value);
            string content = Encoding.GetEncoding("Windows-1251").GetString(inputBytes);
            return content;
        }  //считывание куки
        public void ParseFileIs2(string nameDiagramm) //парсинг файла формата .is2 и помещение элементов и связей в cookie
        {        
            try
            {
                XDocument xdoc = XDocument.Load(FileUpload1.FileContent);
                string nameCookie = "";
                var i = 0;
                var nodeElements = 0;
                var nodeLinks = 0;
                foreach (XElement elements in xdoc.Element("project").Element("models").Elements("primaryModel").Elements("spd").Elements("actions").Elements("pd"))
                { nodeElements++; }
                if (nodeElements != 0 )
                {
                    foreach (XElement elements in xdoc.Element("project").Element("models").Elements("primaryModel").Elements("spd").Elements("actions").Elements("pd"))
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
                            nameCookie = nameDiagramm + "Element:" + idAttribute.Value;
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
                            cookie.Expires = DateTime.Now.AddSeconds(settingsTimeSecondsCookies);
                            // Добавить куки в ответ
                            Response.Cookies.Add(cookie);
                        }

                    }       
                    HttpCookie cookie2 = new HttpCookie(nameDiagramm + "CountElement");
                    cookie2.Value = i.ToString();
                    cookie2.Expires = DateTime.Now.AddSeconds(settingsTimeSecondsCookies);
                    Response.Cookies.Add(cookie2);
                    goodSchemaFile = true;
                }
                else
                    goodSchemaFile = false;

                foreach (XElement elements in xdoc.Element("project").Element("models").Elements("primaryModel").Elements("spd").Elements("links").Elements("link"))
                { nodeLinks++; }

                if (nodeLinks != 0)
                {
                    i = 0;
                    foreach (XElement elements in xdoc.Element("project").Element("models").Elements("primaryModel").Elements("spd").Elements("links").Elements("link"))
                    {
                        i++;
                        XAttribute afe1Attribute = elements.Attribute("afe1");
                        XAttribute afe2Attribute = elements.Attribute("afe2");
                        XAttribute afe3Attribute = elements.Attribute("afe3");
                        XAttribute typeLinkAttribute = elements.Attribute("type");

                        if (afe1Attribute != null && afe2Attribute != null && afe3Attribute != null && typeLinkAttribute != null)
                        {
                            nameCookie = nameDiagramm + "Link:" + i.ToString();
                            // Создать объект cookie-набора
                            HttpCookie cookie3 = new HttpCookie(nameCookie);
                            // Установить значения в нем
                            //cookie["name"] = nameAttribute.Value;
                            //var bytes = Encoding.GetEncoding(1251).GetBytes(nameAttribute.Value);
                            cookie3["afe1"] = afe1Attribute.Value;
                            cookie3["afe2"] = afe2Attribute.Value;
                            cookie3["afe3"] = afe3Attribute.Value;
                            cookie3["type"] = typeLinkAttribute.Value;
                            //кодирование
                            var bytes = Encoding.GetEncoding(1251).GetBytes(cookie3.Value);
                            cookie3.Value = Convert.ToBase64String(bytes);
                            //время куки
                            cookie3.Expires = DateTime.Now.AddSeconds(settingsTimeSecondsCookies);
                            // Добавить куки в ответ
                            Response.Cookies.Add(cookie3);
                        }
                    }
                    HttpCookie cookie4 = new HttpCookie(nameDiagramm + "CountLinks");
                    cookie4.Value = i.ToString();
                    cookie4.Expires = DateTime.Now.AddSeconds(settingsTimeSecondsCookies);
                    Response.Cookies.Add(cookie4);
                    goodSchemaFile = true;
                }
                else
                    goodSchemaFile = false;
            }
            catch 
            {
                Response.Redirect("Error.aspx");
            }
        }
        public void SetCssErrorTextBox3(string message) //визуализация ошибки на фронте во время загрузки файла - TextBox3
        {
            TextBox3.CssClass = "btn btn-danger";
            TextBox3.Text = message;
        }
        public void SetCssErrorTextBox2() //визуализация ошибки на фронте во время загрузки файла - TextBox2
        {
            TextBox2.Text = "Ошибка";
            TextBox2.CssClass = "btn btn-danger";
            pointIntegrationError.Value = "true";
        }
        public void SetCssErrorTextBox1() //визуализация ошибки на фронте во время загрузки файла - TextBox1
        {
            TextBox1.Text = "Ошибка";
            TextBox1.CssClass = "btn btn-danger";
            pointCurrentError.Value = "true";
        }
        public void ReadCookieIntgrElements() //прочтение cookie элементов интегрируемой диаграммы, инициализация массива класса Elements
        {
            string elementCookie, cookieName;
            Regex pattern = new Regex("name=(.*)[&]id=([\\d]*)[&]level=([\\d]*)[&]number=([\\d]*)[&]status=([\\d]*)[&]type=([\\d]*)[&]formalization=([\\d]*)");
            HttpCookie cookieCount = Request.Cookies["Integration" + "CountElement"];
            if (cookieCount != null)
            {
                var countElements = Int32.Parse(cookieCount.Value);
                countIntgrElement.Value = cookieCount.Value;
                Elements[] intgrElements = new Elements[countElements];
                var countGoodElements = 0;
                for (var j = 1; countGoodElements < countElements; j++)
                {
                    cookieName = "Integration" + "Element:" + j;
                    HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
                    if (cookie != null)
                    {
                        var inputBytes = Convert.FromBase64String(cookie.Value);
                        elementCookie = Encoding.GetEncoding("Windows-1251").GetString(inputBytes);
                        Match match = pattern.Match(elementCookie);
                        if (match.Success)
                        {
                            intgrElements[countGoodElements] = new Elements(match.Groups[1].Value, match.Groups[2].Value, match.Groups[3].Value, match.Groups[4].Value, match.Groups[5].Value, match.Groups[6].Value, match.Groups[7].Value);                      
                        }
                        countGoodElements++;
                    }
                }
                if (sourceIntElements.Count > 0)
                {
                    sourceIntElements.Clear();
                }
                for (int i = 0; i < countElements; i++)
                {
                    if (intgrElements[i] != null)
                    {
                        sourceIntElements.Add(intgrElements[i]);
                    }
                }
            }
        }
        public void ReadCookieIntgrLinks() //прочтение cookie связей интегрируемой диаграммы, инициализация массива класса Links
        {
            string linkCookie, cookieName2;
            Regex pattern2 = new Regex("afe1=(.*)[&]afe2=([\\d]*)[&]afe3=([\\d]*)[&]type=([\\d]*)");
            HttpCookie cookieCount2 = Request.Cookies["Integration" + "CountLinks"];
            if (cookieCount2 != null)
            {
                var countLinks = Int32.Parse(cookieCount2.Value);
                countIntgrLinks.Value = cookieCount2.Value;
                Links[] intgrLinks = new Links[countLinks];
                var countGoodElements = 0;
                for (var j = 1; countGoodElements < countLinks; j++)
                {
                    cookieName2 = "IntegrationLink:" + j;
                    HttpCookie cookie7 = HttpContext.Current.Request.Cookies[cookieName2];
                    if (cookie7 != null)
                    {
                        var inputBytes = Convert.FromBase64String(cookie7.Value);
                        linkCookie = Encoding.GetEncoding("Windows-1251").GetString(inputBytes);
                        Match match = pattern2.Match(linkCookie);
                        if (match.Success)
                        {

                            intgrLinks[countGoodElements] = new Links(match.Groups[1].Value, match.Groups[2].Value, match.Groups[3].Value, match.Groups[4].Value);
                        }
                        countGoodElements++;
                    }
                }
                if (sourceIntLinks.Count > 0)
                {
                    sourceIntLinks.Clear();
                }
                for (int i = 0; i < countLinks; i++)
                {
                    if (intgrLinks[i] != null)
                    {
                        sourceIntLinks.Add(intgrLinks[i]);
                    }
                }
            }
        }
        public void ReadCookieCurLinks() //прочтение cookie связей интегрируемой диаграммы, инициализация массива класса Links
        {
            string linkCookie, cookieName2;
            Regex pattern2 = new Regex("afe1=(.*)[&]afe2=([\\d]*)[&]afe3=([\\d]*)[&]type=([\\d]*)");
            HttpCookie cookieCount2 = Request.Cookies["Current" + "CountLinks"];
            if (cookieCount2 != null)
            {
                var countLinks = Int32.Parse(cookieCount2.Value);
                countCurrentLinks.Value = cookieCount2.Value;
                Links[] curLinks = new Links[countLinks];
                var countGoodElements = 0;
                for (var j = 1; countGoodElements < countLinks; j++)
                {
                    cookieName2 = "CurrentLink:" + j;
                    HttpCookie cookie7 = HttpContext.Current.Request.Cookies[cookieName2];
                    if (cookie7 != null)
                    {
                        var inputBytes = Convert.FromBase64String(cookie7.Value);
                        linkCookie = Encoding.GetEncoding("Windows-1251").GetString(inputBytes);
                        Match match = pattern2.Match(linkCookie);
                        if (match.Success)
                        {
                            curLinks[countGoodElements] = new Links(match.Groups[1].Value, match.Groups[2].Value, match.Groups[3].Value, match.Groups[4].Value);
                        }
                        countGoodElements++;
                    }
                }
                if (sourceCurLinks.Count > 0)
                {
                    sourceCurLinks.Clear();
                }
                for (int i = 0; i < countLinks; i++)
                {
                    if (curLinks[i] != null)
                    {
                        sourceCurLinks.Add(curLinks[i]);
                    }
                }
            }
        }
        public void ReadCookieCurElements() //прочтение cookie элементов текущей диаграммы, инициализация массива класса Elements, наполнение DropDownList1
        {
            string elementCookie, cookieName;
            Regex pattern = new Regex("name=(.*)[&]id=([\\d]*)[&]level=([\\d]*)[&]number=([\\d]*)[&]status=([\\d]*)[&]type=([\\d]*)[&]formalization=([\\d]*)");
            HttpCookie cookieCount = Request.Cookies["Current" + "CountElement"];
            if (cookieCount != null)
            {
                var countElements = Int32.Parse(cookieCount.Value);
                countCurElement.Value = cookieCount.Value;
                Elements[] curElements = new Elements[countElements];
                var countGoodElements = 0;
                for (var j = 1; countGoodElements < countElements; j++)
                {
                    cookieName = "Current" + "Element:" + j;
                    HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
                    if (cookie != null)
                    {
                        var inputBytes = Convert.FromBase64String(cookie.Value);
                        elementCookie = Encoding.GetEncoding("Windows-1251").GetString(inputBytes);
                        Match match = pattern.Match(elementCookie); //с декодированием
                        //Match match = pattern.Match(cookie.Value); // если не нужно декодирование
                        if (match.Success)
                        {
                                curElements[countGoodElements] = new Elements(match.Groups[1].Value, match.Groups[2].Value, match.Groups[3].Value, match.Groups[4].Value, match.Groups[5].Value, match.Groups[6].Value, match.Groups[7].Value);
                        }
                        countGoodElements++;
                    }
                }
                if (DropDownList1.Items.Count > 0)
                {
                    DropDownList1.Items.Clear();
                }
                if (sourceDropDown.Count > 0)
                {
                    sourceDropDown.Clear();
                }
                if (sourceCurElements.Count > 0)
                {
                    sourceCurElements.Clear();
                }

                for (int i = 0; i < countElements; i++)
                {
                    if (curElements[i] != null)
                    { 
                        if (curElements[i].Name != "")
                        sourceDropDown.Add(curElements[i].Level + "." + curElements[i].Number + "." + curElements[i].Name);
                        else
                        sourceDropDown.Add(curElements[i].Level + "." + curElements[i].Number + "." + defaultNameElement);

                        sourceCurElements.Add(curElements[i]);
                    }
                }
                if (sourceDropDown != null)
                    sourceDropDown.Sort();

                DropDownList1.DataSource = sourceDropDown;
                DropDownList1.DataBind();
                if (DropDownList1.Items.Count != 0)
                {
                    Step4ElementON();
                }
            }
        }
        public void SetCssSuccessTextBox1()
        {
            this.hfNameCurrent.Value = FileUpload1.FileName;
            TextBox1.Text = this.hfNameCurrent.Value;
            TextBox1.CssClass = "btn btn-success";
            pointCurrentError.Value = "false";
        } //визуализация успешности на фронте во время загрузки файла - TextBox1
        public void SetCssSuccessTextBox2()
        {
            this.hfNameIntegration.Value = FileUpload1.FileName;
            TextBox2.Text = this.hfNameIntegration.Value;
            TextBox2.CssClass = "btn btn-success";
            pointIntegrationError.Value = "false";
        } //визуализация успешности на фронте во время загрузки файла - TextBox2
        public void SetCssSuccessTextBox3()
        {
            TextBox3.Text = "Успешно";
            TextBox3.CssClass = "btn btn-success";
        } //визуализация успешности на фронте во время загрузки файла - TextBox3
        public void SetCssDisabledTextBox1()
        {
            TextBox1.CssClass = "btn btn-default.disabled";
            TextBox1.Text = "";
        } //сброс визуализации с TextBox1
        public void SetCssDisabledTextBox2()
        {
            TextBox2.CssClass = "btn btn-default.disabled";
            TextBox2.Text = "";
        } //сброс визуализации с TextBox2
        public void ReadCookieAuth()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["auth_"];
            if (cookie != null)
            {
                var inputBytes = Convert.FromBase64String(cookie.Value);
                auth_ = Encoding.GetEncoding("Windows-1251").GetString(inputBytes);
            }
        } //считывание куки авторизации
        public void ConsoleLog(string message)
        {
            JavaScript js = new JavaScript();
            js.ConsoleLog(message);
        } //test func для просмотра значений из консоли
        public void Alert(string message)
        {
            JavaScript js = new JavaScript();
            js.Alert(message);
        }
        public void DetermineItem(string valueItem)
        {
            Regex pattern2 = new Regex("([\\d])[.]([\\d])[.](.*)");
            Match match = pattern2.Match(valueItem);
            if (match.Success)
            {
                itemChoiceLevel = match.Groups[1].Value;
                itemChoiceNumber= match.Groups[2].Value;
            }
        } //получение name выбранного итема 
        public void integrationIfElementIsEmpty()
        {
            try 
            {
                Integrator integrator = new Integrator(
                                                   sourceCurElements, sourceIntElements,
                                                   sourceCurLinks, sourceIntLinks,
                                                   countCurElement.Value, countIntgrElement.Value,
                                                   countCurrentLinks.Value, countIntgrLinks.Value,
                                                   itemChoiceLevel, itemChoiceNumber
                                                   );
                //integrator.ConsoleLogElements();
            }
            catch { Alert("error"); }
        }
        /////////////////////////////////back

        protected void Button1_Click(object sender, EventArgs e) //button "загрузить тек. диаграмму"
        {
            if (FileUpload1.HasFile)
            {
                if (GetFormat() == ".is2")
                {
                    ParseFileIs2("Current");

                    if (goodSchemaFile == true)
                    {
                        SetCssSuccessTextBox1();
                        SetCssSuccessTextBox3();
                        ReadCookieCurElements();
                        ReadCookieCurLinks();
                    }
                    else
                    {
                        SetCssErrorTextBox3("Неправильная структура файла!");
                        SetCssErrorTextBox1();
                        if (DropDownList1.Items.Count!=0)
                        {
                            DropDownList1.Items.Clear();
                            DropDownList1.Visible = false;
                            Button4.Visible = false;
                        }
                    }
                }
                else
                {
                    SetCssErrorTextBox3("Неправильный формат файла!");
                    SetCssErrorTextBox1();
                }
            }
            else
            {
                SetCssErrorTextBox3("Файл не выбран!");
                SetCssErrorTextBox1();
            }
            if (pointIntegrationError.Value == "true")
            {
                SetCssDisabledTextBox2();
            }
            if (pointIntegrationError.Value == "false" && pointCurrentError.Value == "false")
            {
                TextBox3.Text = "Файлы успешно загружены";
            }
        }
        protected void Button2_Click(object sender, EventArgs e) //button "загрузить интегр. диаграмму"
        {
            if (FileUpload1.HasFile)
            {              
                if (GetFormat() == ".is2")
                {
                    ParseFileIs2("Integration");
                    if (goodSchemaFile == true)
                    {
                        SetCssSuccessTextBox2();
                        SetCssSuccessTextBox3();
                        ReadCookieIntgrElements();
                        ReadCookieIntgrLinks();
                    }
                    else
                    {
                        SetCssErrorTextBox3("Неправильная структура файла!");
                        SetCssErrorTextBox2();
                    }
                }
                else
                {
                    SetCssErrorTextBox3("Неправильный формат файла!");
                    SetCssErrorTextBox2();
                }
            }
            else
            {
                SetCssErrorTextBox3("Файл не выбран!");
                SetCssErrorTextBox2();
            }
            if (pointCurrentError.Value == "true")
            {
                SetCssDisabledTextBox1();
            }
            if (pointIntegrationError.Value == "false" && pointCurrentError.Value == "false")
            {
                TextBox3.Text = "Файлы успешно загружены";
            }
        }
        protected void Button4_Click(object sender, EventArgs e) //баттон для выбора итема из дропдауна
        {
            userChoice = DropDownList1.SelectedValue;
            DetermineItem(userChoice);
            var type = FileUpload1.GetType();
            ConsoleLog(type.ToString());
            //ConsoleLog("Your choice = " + itemChoiceLevel + "." + itemChoiceNumber); //test
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            RedirectToAuth();
            SetCookie("CurrentPage","module.aspx");
        }
        protected void Button3_Click(object sender, EventArgs e) //тестовый баттон
        {
            integrationIfElementIsEmpty();
        }
        /// garbage
        private void OpenFileDialog()
        {

        }
        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {      
        }    
        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }
        protected void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }
        
    }
}

