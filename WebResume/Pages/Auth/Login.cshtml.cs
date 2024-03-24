using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebResume.BL.Auth;
using WebResume.BL.Exception;

namespace WebResume.Pages.Auth;

public class Login(IAuth auth, ICurrentUser currentUser) : PageModel{
    private readonly IAuth _auth = auth;
    
    [BindProperty]
    [Required(ErrorMessage = "Email mustn't be empty")] 
    [EmailAddress(ErrorMessage = "Email is incorrect")]
    public string Email{ get; set; }
    
    [BindProperty]
    [Required(ErrorMessage = "Password mustn't be empty")]
    public string Password{ get; set; }
    
    [BindProperty]
    [Display(Name = "Запомни меня")]
    public bool RememberMe{ get; set; }
    
    
    public async Task<IActionResult> OnGet(){
        if(await currentUser.IsLoggedIn())
            return RedirectToPage("/Index");
        return Page();
    }

    public async Task<IActionResult> OnPost(){
        if(!ModelState.IsValid)
            return Page();

        try{
            await _auth.Login(Email, Password, RememberMe);
            
        }
        catch (AuthorizationException e){
            ModelState.AddModelError("Incorrect auth data", "Login or password is incorrect");
            return Page();
        }
        catch{
            ModelState.AddModelError("Unknown auth error", "");
            return Page();
        }
        
        return RedirectToPage("/Index");
    }
}