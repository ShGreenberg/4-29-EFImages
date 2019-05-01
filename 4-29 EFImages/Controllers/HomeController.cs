using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using _4_29_EFImages.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using _4_29_EFImages.data2;
using Newtonsoft.Json;

namespace _4_29_EFImages.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _environment;
        private string _connString;

        public HomeController(IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _environment = hostingEnvironment;
            _connString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            ImagesRepository rep = new ImagesRepository(_connString);
            return View(rep.GetImages());
        }

        public IActionResult ShowAddImage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddImage(string title, IFormFile imageFile)
        {
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
            string fullPath = Path.Combine(_environment.WebRootPath, "uploads", fileName);
            using (FileStream stream = new FileStream(fullPath, FileMode.CreateNew))
            {
                imageFile.CopyTo(stream);
            }
            Image image = new Image
            {
                FileName = fileName,
                Title = title
            };
            ImagesRepository rep = new ImagesRepository(_connString);
            rep.AddImage(image);
            return Redirect("/");
        }

        public IActionResult ViewImage(int id)
        {
            ImagesRepository rep = new ImagesRepository(_connString);
            return View(rep.GetImage(id));
        }

        [HttpPost]
        public IActionResult AddLike(int id)
        {
            bool liked = HttpContext.Session.Get<bool>($"liked{id}");
            if (liked)
            {
                return Redirect("/");
            }
            ImagesRepository rep = new ImagesRepository(_connString);
            rep.AddLike(id);
            HttpContext.Session.Set<bool>($"liked{id}", true);
            //HttpContext.Session.Set<bool>("dis", true);
            return Redirect("/");
        }

        public IActionResult GetImages()
        {
            ImagesRepository rep = new ImagesRepository(_connString);
            IEnumerable<Image> images = rep.GetImages();
            //IEnumerable<ImageLiked> images = rep.GetImages();
            foreach (Image i in images)
            {
                i.Disabled = HttpContext.Session.Get<bool>($"liked{i.Id}");
                //i.Disabled = HttpContext.Session.Get<bool>("dis");
                //i.Disabled = true;
            }
            return Json(images);
        }
    }

    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            string value = session.GetString(key);

            return value == null ? default(T) :
                JsonConvert.DeserializeObject<T>(value);
        }
    }
}
