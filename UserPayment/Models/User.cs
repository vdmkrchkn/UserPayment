using System.ComponentModel.DataAnnotations;

namespace UserPayment.Models
{
    public class User
    {
        public User() { }
        //
        public User(string aLogin, string aPassword) : this()
        {
            Login = aLogin;
            Password = aPassword;
        }
        // entity primary key
        [Key]
        public int Id { get; set; }
        // phone number
		[Required]
        public string Login { get; set; }
		// password
		[Required]
		public string Password { get; set; }
    }
}
