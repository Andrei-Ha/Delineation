using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Delineation.Models;
using GemBox.Document;
using GemBox.Document.Tables;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using System.IO;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sql;


namespace Delineation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost]
        public IActionResult SavePNG(string png, string svg)
        {
            var decodeURL_svg = WebUtility.UrlDecode(svg);
            var base64Data_svg = decodeURL_svg.Split(',');
            string path_svg = _webHostEnvironment.WebRootPath + "\\Temp\\mypict.svg";
            using (FileStream fs = new FileStream(path_svg, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(base64Data_svg[1]);
                    bw.Write(data);
                }
            }
            //---
            var decodeURL_png = WebUtility.UrlDecode(png);
            var base64Data_png = decodeURL_png.Split(',');
            string path_png = _webHostEnvironment.WebRootPath + "\\Temp\\mypict.png";
            using (FileStream fs = new FileStream(path_png, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(base64Data_png[1]);
                    bw.Write(data);
                }
            }
            //---
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Open()
        {
            return View();
        }
        public IActionResult Help() => View();
        public IActionResult Privacy()
        {
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
