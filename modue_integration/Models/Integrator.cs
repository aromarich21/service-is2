using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace modue_integration.Models
{
    public class Integrator
    {
        public List<Elements> ResElements { get; set; }
        public List<Links> ResLinks { get; set; }
        public List<Elements> IntElements { get; set; }
        public List<Links> IntLinks { get; set; }
        public int CountElements {get;set;}
        public int CountLinks { get; set; }

        public Integrator(List<Elements> curElements, List<Elements>intElements, List<Links> curLinks, List<Links> intLinks)
        {
            ResElements = new List<Elements>();
            ResLinks = new List<Links>();
            ResElements = curElements;
            ResLinks = curLinks;
            IntElements = intElements;
            IntLinks = intLinks;
        }
        public Integrator()
        {

        }

        public void ElementsConnect()
        {

        }
        public void ConsoleLogElements()
        {
            JavaScript js = new JavaScript();
            if (ResElements != null)
            {
                //sourceElements.Sort();
                foreach (Elements elements in ResElements)
                {
                    js.ConsoleLog(elements.Level + "." + elements.Number + "." + elements.Name);
                }
            }
        }
    }
}