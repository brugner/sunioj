using System.ComponentModel.DataAnnotations;

namespace Sunioj.Core.Resources.Auth
{
    public class UserForAuthDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public UserForAuthDTO()
        {

        }

        public UserForAuthDTO(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
