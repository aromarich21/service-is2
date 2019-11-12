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
        public static Elements XElement { get; set; }
        public string Type { get; set; }

        public Integrator(List<Elements> curElements, List<Elements>intElements, 
                          List<Links> curLinks, List<Links> intLinks,
                          string curCountElements,string intCountElements,
                          string curCountLinks, string intCountLinks, 
                          string levelElement, string numberElement)
        {
            Type = "emptyElement";
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
            SearchElementWithIndexChoiceElement(index);
            ConnectInEmptyElement(index);
            RecodingElements(Int32.Parse(index));
            //ConsoleLog(choiceElement.Name);
        }
        public Integrator(List<Elements> curElements, List<Elements> intElements, 
                          List<Links> curLinks, List<Links> intLinks,
                          string curCountElements, string intCountElements,
                          string curCountLinks, string intCountLinks)
        {
            Type = "newElement";
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
                foreach (Elements elements in listElements)
                {
                    js.ConsoleLog(elements.Level + "." + elements.Number + "." + elements.Name);
                }
            }
        }
        public void ConsoleLogLinks(List<Links> listLinks)
        {
            JavaScript js = new JavaScript();
            if (listLinks != null)
            {
                foreach (Links elements in listLinks)
                {
                    js.ConsoleLog(elements.Afe1 + "." + elements.Afe2 + "." + elements.Afe3);
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
                    break;
                }
                count++;
            }
            return index;
        }
        public void SearchElementWithIndexChoiceElement(string index)
        {
            choiceElement = new Elements();
            var i = Int32.Parse(index);
            choiceElement = ResElements[i];
        }
        public Elements SearchElementWithIndex(int index) 
        {
            return ResElements[index]; 
        }
        public string SearchNumberLastElement(string id, List<Links> linksCurrent)
        {
            foreach (Links elements in linksCurrent)
            {
                if (elements.Afe1 == id && elements.Type == "3" && elements.Afe3 == "0")
                {
                    return elements.Afe2;
                }
            }
            return null;
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
                {
                    ResElements[indexChange] = elements;
                    ResElements[indexChange].Level = choiceElement.Level;
                    ResElements[indexChange].Number = choiceElement.Number;
                    XElement = ResElements[indexChange];
                }
                else
                {
                    ResElements.Add(elements);
                    count++;
                }
                iteration++;
            }
            CountResElements = count.ToString();
            ConsoleLogElements(ResElements); //test
            count = Int32.Parse(CountResLinks);
            iteration = 1;
            foreach (Links links in IntLinks)
            {
                if (iteration != 1)
                    ResLinks.Add(links);

                count++;
                iteration++;
            }
            CountResLinks = count.ToString();
            ConsoleLogLinks(ResLinks); //test
        }
        public void RecodingElements(int indexChange)
        {
            if (Type == "emptyElement")
            {
                var number = Int32.Parse(ResElements[indexChange].Number);
                var level = Int32.Parse(ResElements[indexChange].Level);
                number--;
                var index = SearchIndexElement(level.ToString(), number.ToString(), ResElements);
                var workElement = SearchElementWithIndex(Int32.Parse(index));
                var workLastNumber = SearchNumberLastElement(workElement.Id,ResLinks);
                ConsoleLog(workElement.Name);
                ConsoleLog(workLastNumber);
            }

            if (Type == "newElement")
            {

            }
        }
    }
}