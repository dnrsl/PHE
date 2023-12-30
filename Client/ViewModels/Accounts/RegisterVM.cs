namespace Client.ViewModels.Accounts
{
    public class RegisterVM
    {
        //Account
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        //User
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public IFormFile PhotoFile { get; set; }
        public string Photo { get; set; }

        public Guid RoleGuid { get; set; }
    }
}
