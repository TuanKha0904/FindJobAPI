using FindJobAPI.Data;
using FindJobAPI.Model.Accounts;
using FindJobAPI.Model.Domain;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
    
namespace FindJobAPI.Repository.Accounts
{
    public class Account_Repository : IAccount_Repository
    {
        private readonly AppDbContext _context;
        public Account_Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AccountDTO>> GetAll()
        {
            var AllAccount =  _context.Account.AsQueryable();
            var listAccount = await AllAccount.Select(account => new AccountDTO
            {
                account_id = account.account_id,
                email = account.email,
                password = account.password,
                date_create = account.date_create,
            }).ToListAsync();
            return listAccount;
        }

        public async Task<CreateAccount> CreateAccount(CreateAccount createAccount)
        {
            var exitstingEmail = await _context.Account.FirstOrDefaultAsync(a => a.email == createAccount.email);
            if (exitstingEmail == null)
            {
                var AddAccount = new account
                {
                    email = createAccount.email,
                    password = createAccount.password,
                    date_create = DateTime.Now
                };
                await _context.Account.AddAsync(AddAccount);
                await _context.SaveChangesAsync();
                var AddSeeker = new seeker
                {
                    account_id = AddAccount.account_id,
                };
                var AddEmployer = new employer
                {
                    account_id = AddAccount.account_id,
                };
                await _context.Seeker.AddAsync(AddSeeker);
                await _context.Employer.AddAsync(AddEmployer);
                await _context.SaveChangesAsync();
                return createAccount;
            }
            return null!;
        }

        public async Task<UpdateAccount> UpdateAccount([Required] int id, UpdateAccount updateAccount)
        {
            var accountDomain = await _context.Account.FirstOrDefaultAsync(a => a.account_id == id);
            if (accountDomain == null)
                return null!;
            accountDomain.password = updateAccount.password;
            await _context.SaveChangesAsync();
            return updateAccount;
        }

        public async Task<account> DeleteAccount([Required] int id)
        {
            var accountDomain = await _context.Account.FirstOrDefaultAsync(a => a.account_id == id);
            var seekerDomain = await _context.Seeker.FirstOrDefaultAsync(s => s.account_id == id);
            var employerDomain = await _context.Employer.FirstOrDefaultAsync(e =>  e.account_id == id);
            if (accountDomain == null)
                return null!;
            _context.Seeker.Remove(seekerDomain!);
            _context.Employer.Remove(employerDomain!);
            await _context.SaveChangesAsync();
            _context.Account.Remove(accountDomain);
            await _context.SaveChangesAsync();
            return accountDomain;
        }
    }
}
