using Microsoft.AspNetCore.Mvc;
using MSIT153Site.Models.ViewModel;

namespace MSIT153Site.Controllers
{
    public class ApiController : Controller
    {
        //  public IActionResult Index(string name, int age=30)
        public IActionResult Index(UserViewModel user)
        {
            if (string.IsNullOrEmpty(user.name))
            {
                user.name = "guest";
            }
            //return Content("<h2>Ajax 你好 !!</h2>","text/html", System.Text.Encoding.UTF8);
            return Content($"Hello {user.name}， You are {user.age} years old.");
        }
    }
}
