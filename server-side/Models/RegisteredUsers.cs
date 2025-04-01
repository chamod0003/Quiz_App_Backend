using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class RegisteredUser
{
    

    [Key]
    public int UserId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string Email { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string Password { get; set; }
}