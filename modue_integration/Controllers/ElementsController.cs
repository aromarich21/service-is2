using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using modue_integration.Models;

namespace modue_integration.Controllers
{
    public class 
        ElementsController : ApiController
    {
        List<Elements> curElements = new List<Elements>();
        List<Elements> intElements = new List<Elements>();
        
        public void CreateElement()
        {
            Elements newElement = new Elements("newElement","1","1","1","1","1","0");
            curElements.Add(newElement);
        }
        public IHttpActionResult GetCurElement(int id)
        {
            var element = curElements.FirstOrDefault((p) => Int32.Parse(p.Id) == id);
            if (element == null)
            {
                return NotFound();
            }
            return Ok(element);
        }

        public IEnumerable<Elements> GetAllCurElements()
        {
            CreateElement();
            return curElements;
        }

        public IEnumerable<Elements> GetAllIntElements()
        {
            return intElements;
        }

    }
}
