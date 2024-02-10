using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebResume.Model;

public class SessionModel{
    [Key]
    public Guid SessionId{ get; set; }
    
    public string? SessionData{ get; set; }

    [Required]
    public DateTime CreatedDateTime{ get; set; }

    [Required]
    public DateTime LastAccessedDateTime{ get; set; }

    [Required] 
    public int UserId{ get; set; }
}