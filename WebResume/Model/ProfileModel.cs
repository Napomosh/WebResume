using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebResume.Model;

public class ProfileModel{
    [Key] 
    public int ProfileId{ get; set; }
    
    [ForeignKey("UserId")]
    public int UserId{ get; set; }

    [Required]
    public string ProfileName{ get; set; } = null!;
    
    [Required]
    public string FirstName{ get; set; } = null!;
    
    [Required]
    public string LastName{ get; set; } = null!;
    
    [Required]
    public string ProfileImage{ get; set; } = null!;
}