using modue_integration.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace modue_integration
{
    public partial class version : System.Web.UI.Page
    {
        public void ShowInfo()
        {
            var projectName = "serviceName = " + HttpContext.Current.ApplicationInstance.GetType().BaseType.Assembly.GetName().Name;
            var language = "language = c#; cs; csharp; vb; vbs; visualbasic; vbscript; javascript";
            JavaScript js = new JavaScript();
            Assembly asm = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(asm.Location);
            var assems = AppDomain.CurrentDomain.GetAssemblies();
            var filteredType = typeof(TargetFrameworkAttribute);
            var assemblyMatches = assems.Select(x => new { Assembly = x, TargetAttribute = (TargetFrameworkAttribute)x.GetCustomAttribute(filteredType) })
                                        .Where(x => x.TargetAttribute != null);
            foreach (var assem in assemblyMatches)
            {
                var framework = new System.Runtime.Versioning.FrameworkName(assem.TargetAttribute.FrameworkName);
                Label4.Text = "platform = " + framework.Identifier + " " + framework.Version.ToString();
            }

            var serviceVersion = "serviceVersion = " + fvi.ProductVersion;
            var build = "build = " + fvi.FileBuildPart;

            js.ConsoleLog(projectName + "; " + serviceVersion + "; " + build + "; " + language + "; ");
            Label1.Text = projectName;
            Label2.Text = serviceVersion;
            Label3.Text = build;
            Label5.Text = language;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ShowInfo();
        }
    }
}