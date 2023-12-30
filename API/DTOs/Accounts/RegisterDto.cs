namespace API.DTOs.Accounts
{
    public class RegisterDto
    {
        //Account
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        //User
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Photo { get; set; }

        public Guid RoleGuid { get; set; }
    }
}
