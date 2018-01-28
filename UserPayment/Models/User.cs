using System.ComponentModel.DataAnnotations;

namespace UserPayment.Models
{
    public class User : BaseEntity
    {
        public User() { }
        //
        public User(string aLogin, string aPassword) : this()
        {
            Login = aLogin;
            Password = aPassword;
        }                
        // phone number
		[Required]
        public string Login { get; set; }
		// password
		[Required]
		public string Password { get; set; }
    }
}
