using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebResume.BL.Auth;

namespace WebResume.Pages;

public class IndexModel(ILogger<IndexModel> logger, ICurrentUser currentUser) : PageModel{
    private readonly ILogger<IndexModel> _logger = logger;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<bool> IsLogged(){
        return await _currentUser.IsLoggedIn();
    }
    public void OnGet(){
        
    }
}