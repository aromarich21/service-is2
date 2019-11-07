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
        public string CountResElements {get;set;}
        public string CountResLinks { get; set; }
        public string CountIntElements { get; set; }
        public string CountIntLinks { get; set; }
        public string LevelChoiceElement { get; set; }
        public string NumberChoiceElement { get; set; }
        public static Elements choiceElement { get; set; }

        public Integrator(List<Elements> curElements, List<Elements>intElements, 
                          List<Links> curLinks, List<Links> intLinks,
                          string curCountElements,string intCountElements,
                          string curCountLinks, string intCountLinks, 
                          string levelElement, string numberElement)
        {
            ResElements = new List<Elements>();
            ResLinks = new List<Links>();
            ResElements = curElements;
            ResLinks = curLinks;
            IntElements = intElements;
            IntLinks = intLinks;
            CountResElements = curCountElements;
            CountIntElements = intCountElements;
            CountResLinks = curCountLinks;
            CountIntLinks = intCountLinks;
            LevelChoiceElement = levelElement;
            NumberChoiceElement = numberElement;
            var index = SearchIndexElement(LevelChoiceElement, NumberChoiceElement, ResElements);
            SearchElementWithIndex(index);
            ConnectInEmptyElement(index);
            //ConsoleLog(choiceElement.Name);
        }
        public Integrator(List<Elements> curElements, List<Elements> intElements, 
                          List<Links> curLinks, List<Links> intLinks,
                          string curCountElements, string intCountElements,
                          string curCountLinks, string intCountLinks)
        {
            ResElements = new List<Elements>();
            ResLinks = new List<Links>();
            ResElements = curElements;
            ResLinks = curLinks;
            IntElements = intElements;
            IntLinks = intLinks;
            CountResElements = curCountElements;
            CountIntElements = intCountElements;
            CountResLinks = curCountLinks;
            CountIntLinks = intCountLinks;
        }
        public Integrator()
        {

        }
        public void ConsoleLogElements(List<Elements> listElements)
        {
            JavaScript js = new JavaScript();
            if (listElements != null)
            {
                //sourceElements.Sort();
                foreach (Elements elements in listElements)
                {
                    js.ConsoleLog(elements.Level + "." + elements.Number + "." + elements.Name);
                }
            }
        }
        public void ConsoleLog(string message)
        {
            JavaScript js = new JavaScript();
            js.ConsoleLog(message);
        }
        public string SearchIndexElement(string level, string number, List<Elements> elementsForSearch)
        {
            var count = 0;
            string index = "";
            foreach (Elements elements in elementsForSearch)
            {
                if (elements.Level == level && elements.Number == number)
                {
                    index = count.ToString();
                    JavaScript js = new JavaScript();
                    js.ConsoleLog("Ваш выбранный итем = " + index);
                    break;
                }
                count++;
            }
            return index;
        }
        public void SearchElementWithIndex(string index)
        {
            choiceElement = new Elements();
            var i = Int32.Parse(index);
            choiceElement = ResElements[i];
        }
        public void ConnectInEmptyElement(string index)
        {
            //var countResultDiagramm = Int32.Parse(CountIntElements) + Int32.Parse(CountResElements) - 1;
            var count = Int32.Parse(CountResElements);
            var indexChange = Int32.Parse(index);
            var iteration = 1;          
            foreach (Elements elements in IntElements)
            {
                if (iteration == 1)
                    ResElements[indexChange] = elements;
                ResElements.Add(elements);

                count++;
                iteration++;
            }
            CountResElements = count.ToString();
            ConsoleLogElements(ResElements);
        }
    }
}