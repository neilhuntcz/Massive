using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Mvc;
using GraphService;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HttpClient proxy = new HttpClient();
            byte[] data = proxy.GetByteArrayAsync("http://localhost:56481/Frontend.svc/GetAllNodes").Result;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<GraphNode>));
            MemoryStream stream = new MemoryStream(data);
            var obj = (List<GraphNode>)ser.ReadObject(stream);

            return View(obj);
        }

        public ActionResult CalcRoute(FormCollection collection)
        {
            // request node path from service
            if (ModelState.IsValid)
            {
                //do something with account
                return RedirectToAction("Index");
            }
            return View("SignUp");
        }
    }
}