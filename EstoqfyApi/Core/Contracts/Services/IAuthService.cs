namespace EstoqFy.Core.Contracts.Services
{
    public interface IAuthService
    {
        public string CreateHash(string password);
        public bool VerifyHash(string password, string hash);
    }
}
