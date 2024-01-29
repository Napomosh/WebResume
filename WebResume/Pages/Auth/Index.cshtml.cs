using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebResume.BL.Auth;
using WebResume.Model;

namespace WebResume.Pages.Auth;

public class Index(IAuth auth) : PageModel{
    private readonly  IAuth _auth = auth;
    
    [BindProperty]
    [Required(ErrorMessage = "Email mustn't be empty")] 
    [EmailAddress(ErrorMessage = "Email is incorrect")]
    public string? Email{ get; set; }
    
    [BindProperty]
    [Required(ErrorMessage = "Password mustn't be empty")]
    public string? Password{ get; set; }
    
    public void OnGet(){
        
    }

    public async Task<IActionResult> OnPost(){
        if(!ModelState.IsValid)
            return Page();

        var isExistUser = await _auth.IsExistUser(Email);
        if (!isExistUser){
            ModelState.AddModelError(AuthConstants.AUTH_ERROR_USER_EXSIST, "User already exist");
        }
        
        await _auth.CreateUser(new UserModel{
            Email = Email!,
            Password = Password!
        });
        
        return RedirectToPage("/Index");
    }
}