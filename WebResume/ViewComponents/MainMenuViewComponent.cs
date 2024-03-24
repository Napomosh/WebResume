using Microsoft.AspNetCore.Mvc;
using WebResume.BL.Auth;

namespace WebResume.ViewComponents;

public class MainMenuViewComponent(ICurrentUser currentUser) : ViewComponent{
    public async Task<IViewComponentResult> InvokeAsync(){
        bool isLoggedIn = await currentUser.IsLoggedIn();
        return View("Index", isLoggedIn);
    }
}