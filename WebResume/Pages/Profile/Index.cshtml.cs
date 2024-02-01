using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebResume.BL.FileManagers;

namespace WebResume.Pages.Profile;

public class Index(IImageIOManager imgManager) : PageModel{
    [BindProperty]
    [Required(ErrorMessage = "Profile name cant be empty")]
    [Display(Name = "Profile Name")]
    public string ProfileName{ get; set; } = null!;
    
    [BindProperty]
    [Required(ErrorMessage = "Your first name cant be empty")]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed")]
    [Display(Name = "First Name")]
    public string FirstName{ get; set; } = null!;
    
    [BindProperty]
    [Required(ErrorMessage = "Your last name cant be empty")]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed")]
    [Display(Name = "Last Name")]
    public string LastName{ get; set; } = null!;

    public void OnGet(){
        
    }

    public async Task<IActionResult> OnPost(){
        if (!ModelState.IsValid)
            return Page();
        
        var imgData = Request?.Form.Files["file"];
        if (imgData == null){
            ModelState.AddModelError("Null image", "Your image is incorrect");
            return Page();
        }
        await imgManager.UploadImage(imgData);
        
        return RedirectToPage("../Index");
    }
}