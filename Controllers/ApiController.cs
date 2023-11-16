using Microsoft.AspNetCore.Mvc;
using MSIT153Site.Models;
using MSIT153Site.Models.ViewModel;
using System.IO;

namespace MSIT153Site.Controllers
{
    public class ApiController : Controller
    {
        private readonly IWebHostEnvironment _host;
        private readonly DemoContext _context;
        public ApiController(IWebHostEnvironment host, DemoContext context)
        {
            _host = host;
            _context = context;
        }
        //  public IActionResult Index(string name, int age=30)
        public IActionResult Index(UserViewModel user)
        {
           // System.Threading.Thread.Sleep(1000);
            if (string.IsNullOrEmpty(user.name))
            {
                user.name = "guest";
            }
            //return Content("<h2>Ajax 你好 !!</h2>","text/html", System.Text.Encoding.UTF8);
            return Content($"Hello {user.name}， You are {user.age} years old.");
        }

        public IActionResult Register(Members member, IFormFile formFile)
        {
            //實際路徑
            //C:\Users\User\Documents\workspace\MSIT153Site\wwwroot\uploads\abc.jpg
            //專案根目錄的實際路徑
            //string strPath = _host.ContentRootPath; //C:\Users\User\Documents\workspace\MSIT153Site
            //wwwroot的實際路徑
            //tring strPath = _host.WebRootPath; //C:\Users\User\Documents\workspace\MSIT153Site\wwwroot
            string strPath = Path.Combine(_host.WebRootPath, "uploads", formFile.FileName);
            //將檔案存到uploads資料夾中
            using (var fileStream = new FileStream(strPath, FileMode.Create))
            {
                formFile.CopyTo(fileStream);
            }

            member.FileName = formFile.FileName;
            //將上傳的圖轉成二進位
            byte[]? imgByte = null;
            using (var memoryStream = new MemoryStream())
            {
                formFile.CopyTo(memoryStream);
                imgByte = memoryStream.ToArray();
            }
            member.FileData = imgByte;

            //將資料寫進資料庫中
            _context.Members.Add(member);
            _context.SaveChanges();

            return Content("新增成功");
            //檔案名稱、檔案大小、檔案類型
            //string fileInfo = $"{formFile?.FileName} - {formFile?.Length} - {formFile?.ContentType}";
            //return Content(fileInfo);
            //return Content("<h2>Ajax 你好 !!</h2>","text/html", System.Text.Encoding.UTF8);
            //return Content($"Hello {member.name}，{member.email},  You are {member.age} years old.");
        }
   
        //讀取不會重複的城市名稱
      public IActionResult Cities()
        {
            var cities = _context.Address.Select(c => c.City).Distinct();
            return Json(cities);
        }

        //根據城市名稱讀取不會重複的鄉鎮區名稱
        public IActionResult Districts(string city)
        {
            var districts = _context.Address.Where(a => a.City == city).Select(a => a.SiteId).Distinct();
            return Json(districts);
        }

        //根據鄉鎮區讀取路的名稱
        public IActionResult Roads(string siteId)
        {
            var roads = _context.Address.Where(a => a.SiteId == siteId).Select(a => a.Road);
            return Json(roads);
        }


    }
}
