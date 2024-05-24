namespace APIassignment.Models
{
    public class LoginModel
    {   public string Username { get; set; }
        public string Password { get; set; }
        

        LoginModel User = new LoginModel
        {
            Username = "User1234",
            Password = "User1234",

        };
        
        LoginModel Admin = new LoginModel
        {
            Username = "Admin1234",
            Password = "Admin1234",

        };
    }
}
