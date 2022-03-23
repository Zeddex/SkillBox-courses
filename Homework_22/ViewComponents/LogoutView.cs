using Microsoft.AspNetCore.Mvc;

namespace Homework_22.ViewComponents
{
    public class LogoutView : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
