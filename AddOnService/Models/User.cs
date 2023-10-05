using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AddOnService.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public required string Nic { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string MobileNumber { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string[]? PastPassword { get; set; }
}