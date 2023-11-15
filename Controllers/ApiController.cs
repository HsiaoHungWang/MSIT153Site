using Microsoft.AspNetCore.Mvc;
using MSIT153Site.Models.ViewModel;
using System.IO;

namespace MSIT153Site.Controllers
{
    public class ApiController : Controller
    {
        private readonly IWebHostEnvironment _host;
        public ApiController(IWebHostEnvironment host)
        {
            _host = host;
        }
        //  public IActionResult Index(string name, int age=30)
        public IActionResult Index(UserViewModel user)
        {
            System.Threading.Thread.Sleep(1000);
            if (string.IsNullOrEmpty(user.name))
            {
                user.name = "guest";
            }
            //return Content("<h2>Ajax 你好 !!</h2>","text/html", System.Text.Encoding.UTF8);
            return Content($"Hello {user.name}， You are {user.age} years old.");
        }

        public IActionResult register(MemberViewModel member, IFormFile formFile)
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
            return Content(strPath);
            //檔案名稱、檔案大小、檔案類型
            //string fileInfo = $"{formFile?.FileName} - {formFile?.Length} - {formFile?.ContentType}";
            //return Content(fileInfo);
            //return Content("<h2>Ajax 你好 !!</h2>","text/html", System.Text.Encoding.UTF8);
            //return Content($"Hello {member.name}，{member.email},  You are {member.age} years old.");
        }
    }
}
