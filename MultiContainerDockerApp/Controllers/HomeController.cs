using Microsoft.AspNetCore.Mvc;

namespace MultiContainerDockerApp.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        public IActionResult Ping()
        {
            return Ok($"Service is running. \r\nTimestamp: {DateTime.Now}");
        }
    }
}