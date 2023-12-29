using Microsoft.AspNetCore.Mvc;

namespace PWS.Controllers
{
	public class PostController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
