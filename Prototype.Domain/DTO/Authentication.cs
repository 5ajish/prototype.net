namespace Prototype.Domain
{
    public class SigninDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class SignupDTO : SigninDTO
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class SigninResponseDTO
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string ExpireIn { get; set; }
        public string Token { get; set; }
    }
}
