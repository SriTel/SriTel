using System.ComponentModel.DataAnnotations;
using AuthService.Models;

namespace AuthService.DTO
{
    public class UserDTO
    {
        [Required] public long Id { get; set; }

        public string Nic { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string MobileNumber { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
        
        // user to userDTO
        public static UserDTO FromUser(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Nic = user.Nic,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MobileNumber = user.MobileNumber,
                Email = user.Email,
            };
        }

        // userDTO to user
        public User ToUser()
        {
            return new User
            {
                Nic = Nic,
                FirstName = FirstName,
                LastName = LastName,
                MobileNumber = MobileNumber,
                Email = Email,
                Password = Password
            };
        }
    }
}