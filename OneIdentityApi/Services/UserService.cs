using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using OneIdentityApi.Models;

namespace OneIdentityApi.Services
{
    public class UserService:IUserService
    {
        private readonly AppDbContext _db;
        public UserService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _db.Users.Find(a => a.id == int.Parse(id)).SingleOrDefaultAsync();
        }
        public async Task<List<User>> GetUsersAsync()
        {
            return await _db.Users.Find(a => true).ToListAsync();
        }
        public async Task AddUserAsync(User user)
        {
            await _db.Users.InsertOneAsync(user);
        }
        public async void UpdateUserAsync(User user)
        {
            var filter = Builders<User>.Filter.Eq("id", user.id);

            var update = Builders<User>.Update.Set("name",user.name)
                .Set("username",user.username)
                .Set("email",user.email)
                .Set("address",user.address)
                .Set("phone",user.phone)
                .Set("website",user.website)
                .Set("company",user.company);

            await _db.Users.UpdateOneAsync(filter, update);
        }

        public async void DeleteUserAsync(int userId)
        {
            var deletefilter = Builders<User>.Filter.Eq("id", userId);
            await _db.Users.DeleteOneAsync(deletefilter);
        }
    }
}
