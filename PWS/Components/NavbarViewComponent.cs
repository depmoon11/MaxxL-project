using Microsoft.AspNetCore.Mvc;

namespace MaxL.Components
{

    public class NavbarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }

}
