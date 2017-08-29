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
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<FrontendNode>));
            MemoryStream stream = new MemoryStream(data);
            var obj = (List<FrontendNode>)ser.ReadObject(stream);

            // set the canvas to be large enough to fit all of the nodes.
            // scroll bars will appear if the canvas is too large for the screen
            ViewBag.CanvasWidth = obj.Select(n => n.PosX).Max() + 100;
            ViewBag.Canvasheight = obj.Select(n => n.PosY).Max() + 100;

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