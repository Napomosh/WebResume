using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace WebResume.Model;

public class UserModel{
    [Key]
    public int UserId{ get; set; }

    [Required] 
    [EmailAddress] 
    public string Email{ get; set; } = null!;
    
    [Required]
    public string Password{ get; set; } = null!;

    public string Salt{ get; set; } = "";
    
    public int Status{ get; set; } = 0;
}