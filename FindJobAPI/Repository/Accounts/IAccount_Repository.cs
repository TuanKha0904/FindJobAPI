using FindJobAPI.Model.Accounts;
using FindJobAPI.Model.Domain;

namespace FindJobAPI.Repository.Accounts
{
    public interface IAccount_Repository
    {
        Task<List<AccountDTO>> GetAll();
        Task<CreateAccount> CreateAccount(CreateAccount createAccount);
        Task<UpdateAccount> UpdateAccount(int id, UpdateAccount updateAccount);
        Task<account> DeleteAccount(int id);
    }
}
