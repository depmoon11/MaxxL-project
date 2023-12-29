using MaxL.Models;
using Microsoft.AspNetCore.Mvc;

namespace MaxL.Controllers
{
    public class ValidatorController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new MyModel());
        }

        [HttpPost]
        public IActionResult Index(MyModel model)
        {
            var validator = new MyModelValidator();
            var result = validator.Validate(model);

            if (result.IsValid)
            {
                TempData["SuccessMessage"] = "Валідація пройшла успішно!";
        return RedirectToAction("Succes");
                // Дії, які слід виконати при валідних даних
                return View();
            }

            // Передача помилок валідації на сторінку
            ModelState.AddModelError(string.Empty, result.Errors.First().ErrorMessage);
            return View(model);
        }

        public IActionResult Succes()
        {
            // Дії, які слід виконати при успішній валідації
            return View();
        }

    }
}
