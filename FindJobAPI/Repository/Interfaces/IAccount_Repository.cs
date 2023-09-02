using FindJobAPI.Model.Domain;
using FindJobAPI.Model.DTO;

namespace FindJobAPI.Repository.Interfaces
{
    public interface IAccount_Repository
    {
        Task<List<AccountDTO>> GetAll();
        Task<AccountDTO> GetOne(string email, string password);
        Task<CreateAccount> CreateAccount(CreateAccount createAccount);
        Task<UpdateAccount> UpdateAccount(int id, UpdateAccount updateAccount);
        Task<account> DeleteAccount(int id);
    }
}
