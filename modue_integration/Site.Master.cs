using modue_integration.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace modue_integration
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(asm.Location);
            JavaScript js = new JavaScript();
            js.ConsoleLog("serviceVersion = " + fvi.ProductVersion + " loaded");
            Label1.Text = fvi.ProductVersion;
        }
    }
}