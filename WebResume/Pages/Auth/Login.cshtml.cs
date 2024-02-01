﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebResume.BL.Auth;

namespace WebResume.Pages.Auth;

public class Login(IAuth auth) : PageModel{
    private readonly IAuth _auth = auth;
    
    [BindProperty]
    [Required(ErrorMessage = "Email mustn't be empty")] 
    [EmailAddress(ErrorMessage = "Email is incorrect")]
    public string Email{ get; set; }
    
    [BindProperty]
    [Required(ErrorMessage = "Password mustn't be empty")]
    public string Password{ get; set; }
    
    public void OnGet(){
        
    }

    public async Task<IActionResult> OnPost(){
        if(!ModelState.IsValid)
            return Page();
        
        bool loginResult = await _auth.Login(Email, Password);
        if(!loginResult){
            ModelState.AddModelError("Incorrect auth data", "Login or password is incorrect");
            return Page();
        }
        return RedirectToPage("/Index");
    }
}