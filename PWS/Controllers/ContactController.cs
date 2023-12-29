using Microsoft.AspNetCore.Mvc;
using PWS.Services;

namespace PWS.Controllers
{
	public class ContactController : Controller
	{
		private readonly IEmailService _emailService;

		public ContactController(IEmailService emailService)
		{
			_emailService = emailService;
		}

		[HttpPost]
		public async Task<IActionResult> SendMessage(string fname, string email, string message)
		{
			if (string.IsNullOrWhiteSpace(fname) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(message))
			{
				return View("Error");
			}
			await _emailService.SendEmailAsync(email, fname, message);
			return RedirectToAction("Index");
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
