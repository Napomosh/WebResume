using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebResume.BL.Auth;
using WebResume.BL.Exception;
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
        try{
            await _auth.Register(new UserModel{
                Email = Email!,
                Password = Password!
            });
        }
        catch (DuplicateEmailException e){
            ModelState.AddModelError("Duplicate email", "This email is already exist");
            return Page();
        }
        catch{
            ModelState.AddModelError("Unknown error with duplicate email", "Unknown error with duplicate email");
            return Page();
        }
        
        return RedirectToPage("/Index");
    }
}