using System.ComponentModel.DataAnnotations;

namespace WebResume.Model;

public class User{
    [Key]
    public int? UserId{ get; set; }

    [Required] 
    [EmailAddress] 
    public string Email{ get; set; } = null!;
    
    [Required]
    public string Password{ get; set; } = null!;
    
    public string Salt{ get; set; } = null!;
    
    public int Status{ get; set; } = 0;
}