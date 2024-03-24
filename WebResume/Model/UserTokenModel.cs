using System.ComponentModel.DataAnnotations;

namespace WebResume.Model;

public class UserTokenModel{
    [Key]
    public Guid TokenId{ get; set; }
    
    [Required]
    public int UserId{ get; set; }
    
    [Required]
    public DateTime CreatedTime{ get; set; }
}