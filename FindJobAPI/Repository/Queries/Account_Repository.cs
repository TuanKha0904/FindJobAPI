using FindJobAPI.Data;
using FindJobAPI.Model.Domain;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using System.Net.WebSockets;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FindJobAPI.Repository.Queries
{
    public class Account_Repository : IAccount_Repository
    {
        private readonly AppDbContext _context;
        private readonly FirebaseAuth _firebaseAuth;
        private const string Api = "https://identitytoolkit.googleapis.com/v1/";
        private const string ApiKey = "AIzaSyD_mccfU36uTeIcExGaWxxPre3MIiWDuic";
        private readonly HttpClient _httpClient;
        public Account_Repository(AppDbContext context, FirebaseApp firebaseApp)
        {
            _context = context;
            _firebaseAuth = FirebaseAuth.GetAuth(firebaseApp);
            _httpClient = new HttpClient();

        }

        public async Task<List<AllAccountDTO>> GetAll(bool isDescending, int pageNumber, int pageSize)
        {
            var listUsersOptions = new ListUsersOptions
            {
                PageSize = pageSize,
                PageToken = pageNumber > 1 ? (pageNumber - 1).ToString() : null
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
                        DateCreate = user.UserMetaData.CreationTimestamp!.Value.ToString("dd-MM-yyyy"),
                    });
                }
            }
    
            if (isDescending)
            {
                listAccount = listAccount.OrderByDescending(account => account.DateCreate).ToList();
            }
            else
            {
                listAccount = listAccount.OrderBy(account => account.DateCreate).ToList();
            }
    
            return listAccount.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
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


        public async Task<Login> Post(string userId)
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
                var Seeker = new seeker
                {
                    UID = Account.UID
                };
                var employer = new employer
                {
                    UID = Account.UID
                };
                _context.Seeker.Add(Seeker);
                _context.Employer.Add(employer);
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

        public async Task<GetUser> Login(string email, string password)
        {
            var data = new
            {
                email,
                password,
                returnSecureToken = true,
            };
            var content = new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{Api}accounts:signInWithPassword?key={ApiKey}", content);
            var user = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<GetUser>(user);
            var dataResponse = new GetUser()
            {
                idToken = token!.idToken
            };
            return dataResponse!;
        }

        public async Task<UserRecord> Info(string userId, Infor info)
        {
            var accountDomain = await _firebaseAuth.GetUserAsync(userId);
            if (accountDomain == null)
                return null!;
            var updateArgs = new UserRecordArgs
            {
                Uid = accountDomain.Uid,
            };
            if (!string.IsNullOrEmpty(info.Name)) updateArgs.DisplayName = info.Name;
            if (!string.IsNullOrEmpty(info.PhoneNumber))
            {
                if(info.PhoneNumber.StartsWith("0")) 
                {
                    updateArgs.PhoneNumber = "+84" + info.PhoneNumber.Substring(1);
                }
                else
                {
                    updateArgs.PhoneNumber = info.PhoneNumber;
                }
            }
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

        public async Task<UserRecord> Password(string userId, Password password)
        {
            var accountDomain = await _firebaseAuth.GetUserAsync(userId);
            if (accountDomain == null) { return null!; }
            var updateArgs = new UserRecordArgs
            {
                Uid = accountDomain.Uid,
                Password = password.password
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
