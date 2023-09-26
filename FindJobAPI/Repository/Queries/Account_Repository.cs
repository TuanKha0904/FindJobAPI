using FindJobAPI.Data;
using FindJobAPI.Model.Domain;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using FirebaseAdmin;
using FirebaseAdmin.Auth;

namespace FindJobAPI.Repository.Queries
{
    public class Account_Repository : IAccount_Repository
    {
        private readonly AppDbContext _context;
        private readonly FirebaseAuth _firebaseAuth;
        public Account_Repository(AppDbContext context, FirebaseApp firebaseApp)
        {
            _context = context;
            _firebaseAuth = FirebaseAuth.GetAuth(firebaseApp);
        }

        public async Task<List<AllAccountDTO>> GetAll()
        {
            var listUsersOptions = new ListUsersOptions
            {
                PageSize = 1000
            };
            var allAccount = _firebaseAuth.ListUsersAsync(listUsersOptions);
            var listAccount = new List<AllAccountDTO>();
            await foreach (var user in allAccount)
            {
                var userData = await GetUserDataFromFirebase(user.Uid);
                if (userData != null)
                {
                    listAccount.Add(new AllAccountDTO
                    {
                        UID = user.Uid,
                        Email = user.Email,
                        Password = user.PasswordHash,
                        DateCreate = user.UserMetaData.CreationTimestamp!.Value.ToShortDateString(),
                    }) ; 
                }
            }
            return listAccount;
        }
        private async Task<UserRecord> GetUserDataFromFirebase(string uid)
        {
            try
            {
                var userRecord = await _firebaseAuth.GetUserAsync(uid);
                return userRecord;
            }
            catch
            {
                return null!;
            }
        }


        public async Task<Login> Login(string userId)
        {
            var accountFB = await _firebaseAuth.GetUserAsync(userId);
            var accountDomain = await _context.Account.FirstOrDefaultAsync(a => a.UID == userId);
            if (accountDomain == null) 
            {
                var Account = new account
                {
                    UID = userId
                };
                _context.Account.Add(Account);
                await _context.SaveChangesAsync();
            }
            var account = new Login
            {
                UID = accountFB.Uid,
                Name = accountFB.DisplayName,
                Email = accountFB.Email,
                Photo = accountFB.PhotoUrl,
                PhoneNumber = accountFB.PhoneNumber,
            };
            return account;
        }

/*        public async Task<account> CreateAccount(string userId)
        {
            var accessToken = await _firebaseAuth.GetUserAsync(userId);
            return account;
        }
*/
        public async Task<UserRecord> Infor(string userId, Infor infor)
        {
            var accountDomain = await _firebaseAuth.GetUserAsync(userId);
            if (accountDomain == null)
                return null!;
            var updateArgs = new UserRecordArgs
            {
                Uid = accountDomain.Uid,
                DisplayName = infor.Name,
                PhoneNumber = infor.PhoneNumber,
            };
            UserRecord userRecord = await _firebaseAuth.UpdateUserAsync(updateArgs);
            return userRecord;
        }

        public async Task<UserRecord> Photo(string userId, Photo photo)
        {
            var accountDomain = await _firebaseAuth.GetUserAsync(userId);
            if(accountDomain == null) { return null!; }
            var updateArgs = new UserRecordArgs
            {
                Uid = accountDomain.Uid,
                PhotoUrl = photo.PhotoUrl,
            };
            UserRecord userRecord = await _firebaseAuth.UpdateUserAsync(updateArgs);
            return userRecord;
        }


        public async Task<account> DeleteAccount(string userId)
        {
            var accountFB =  _firebaseAuth.GetUserAsync(userId);
            if (accountFB == null) { return null!; }
            var accountDomain = await _context.Account.FirstOrDefaultAsync(a => a.UID == userId);
            if (accountDomain == null)
            {
                return null!;
            }
            _context.Account.Remove(accountDomain);
            await _context.SaveChangesAsync();
            await _firebaseAuth.DeleteUserAsync(userId);
            return accountDomain;
        }
    }
}
