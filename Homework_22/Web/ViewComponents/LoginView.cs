using Microsoft.AspNetCore.Mvc;

namespace Homework_22.ViewComponents
{
    public class LoginView : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
