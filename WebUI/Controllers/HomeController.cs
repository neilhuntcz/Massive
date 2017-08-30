using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Mvc;
using GraphService;
using System.Configuration;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(FormCollection collection)
        {
            HttpClient proxy = new HttpClient();
            byte[] data = proxy.GetByteArrayAsync($"{ConfigurationManager.AppSettings["ServiceURI"]}/Frontend.svc/GetAllNodes").Result;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<FrontendNode>));
            MemoryStream stream = new MemoryStream(data);
            var obj = (List<FrontendNode>)ser.ReadObject(stream);

            // set the canvas to be large enough to fit all of the nodes.
            // scroll bars will appear if the canvas is too large for the screen
            ViewBag.CanvasWidth = obj.Select(n => n.PosX).Max() + 100;
            ViewBag.Canvasheight = obj.Select(n => n.PosY).Max() + 100;

            if (collection.HasKeys())
            {
                ViewBag.ShortestRoute = GetRoute(collection["txtNodeStart"], collection["txtNodeEnd"]);
            }

            return View(obj);
        }

        public ShortestRoute GetRoute(string start, string end)
        {
            try
            {
                HttpClient proxy = new HttpClient();
                byte[] data = proxy.GetByteArrayAsync($"{ConfigurationManager.AppSettings["ServiceURI"]}/DomainLogic.svc/GetShortestRoute?StartNode={start}&EndNode={end}").Result;
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ShortestRoute));
                MemoryStream stream = new MemoryStream(data);
                return (ShortestRoute)ser.ReadObject(stream);
            }
            catch
            {
                ViewBag.Message = "No route found";
                return null;
            }
        }
    }
}