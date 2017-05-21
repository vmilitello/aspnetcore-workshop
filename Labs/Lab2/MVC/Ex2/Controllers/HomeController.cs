using Microsoft.AspNetCore.Mvc;
namespace Ex2.Controllers
{   

    public class HomeController
    {
        [HttpGet("/")]
        public string Index() => "Hello from MVC!";
    }
}
