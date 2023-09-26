using FindJobAPI.Model.Domain;
using FindJobAPI.Model.DTO;
using FirebaseAdmin.Auth;

namespace FindJobAPI.Repository.Interfaces
{
    public interface IAccount_Repository
    {
        Task<List<AllAccountDTO>> GetAll();
        Task<Login> Login(string userId);

/*        Task<account> CreateAccount(string userId);
*/        Task<UserRecord> Infor(string userId, Infor infor);
        Task<UserRecord> Photo(string userId, Photo photo);
        Task<account> DeleteAccount(string userId);


    }
}
