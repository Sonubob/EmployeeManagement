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
    public class FileController : Controller
    {
        private readonly IMockEmployeeRepository _dataRepo;
        public FileController(IMockEmployeeRepository dataRepo)
        {
            _dataRepo = dataRepo;
        }

        // GET: FileController
        public ActionResult Index()
        {
            return View();
        }

      /// <summary>
      /// Get Details as XML Soap Packet
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
        [HttpGet]
        public ActionResult DetailsAsXML()
        {
            //Get the user data from json file
            var data = JsonConvert.SerializeObject( _dataRepo.GetAllEmployee().ToList());
            var model = new FileDetails();
            model.Message = ConvertJsonToSoap(data);
            
            return View("Index",model);
        }

        [HttpGet]
        public ActionResult DetailsAsJson()
        {
            //Get the user data from json file
            var data = JsonConvert.SerializeObject(_dataRepo.GetAllEmployee().ToList());
            var model = new FileDetails();
            model.Message =data;

            return View("Index",model);
        }

        private string ConvertJsonToSoap(object jsonData)
        {
            // Convert JSON to XML
            string xml = JsonConvert.DeserializeXmlNode(JsonConvert.SerializeObject(jsonData), "Data").OuterXml;

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
