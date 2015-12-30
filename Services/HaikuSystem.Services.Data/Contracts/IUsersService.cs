namespace HaikuSystem.Services.Data.Contracts
{
    public interface IUsersService
    {
        void Register(string username, string publishCode);
    }
}
