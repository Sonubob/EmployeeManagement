using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    /// <summary>
    /// Objective: To create action methods that return data as XML and Json
    /// </summary>
    public class ContactController : Controller
    {
        public ContactController()
        {
       
        }

        // GET: ContactController
        public ActionResult Index()
        {
            return View(new ContactDetails());
        }
      
      /// <summary>
      /// Get Details as XML Soap Packet
      /// </summary>
      /// <param name="details"></param>
      /// <returns></returns>
        [HttpPost]
        public IActionResult DetailsAsXML(ContactDetails details)
        {
            //Get the user data from json file
            var data = JsonConvert.SerializeObject(details);
            
            var result = ConvertJsonToSoap(data);

            return Content(result, "text/xml");
        }

        /// <summary>
        /// Send the response as an XML file
        /// </summary>
        /// <param name="contactDetails"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DetailsAsJson(ContactDetails contactDetails)
        {

            var detailsJson = JsonConvert.SerializeObject(contactDetails); 

            return Json(detailsJson);
        }

        private string ConvertJsonToSoap(string jsonData)
        {
            // Convert JSON to XML
            string xml = JsonConvert.DeserializeXmlNode(jsonData,"root").OuterXml;

            // Construct SOAP envelope
            string soapXml = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                                <soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                                    <soap:Body>
                                        {xml}
                                    </soap:Body>
                                </soap:Envelope>";

            return soapXml;
        }
    }
}
