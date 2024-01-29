using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebResume.BL.Auth;
using WebResume.Model;

namespace WebResume.Pages.Auth;

public class Index(IAuth auth) : PageModel{
    private readonly  IAuth _auth = auth;
    
    [BindProperty]
    public string? Email{ get; set; }
    
    [BindProperty]
    public string? Password{ get; set; }
    
    public void OnGet(){
        
    }

    public async Task<IActionResult> OnPost(){
        if(!ModelState.IsValid)
            return RedirectToPage("Index");
        
        await _auth.CreateUser(new UserModel{
            Email = Email!,
            Password = Password!
        });
        
        return RedirectToPage("/Index");
    }
}