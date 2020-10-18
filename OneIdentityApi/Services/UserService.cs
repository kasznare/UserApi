using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using OneIdentityApi.Models;

namespace OneIdentityApi.Services
{
    public class UserService
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
            return await _db.Users.Find(a =>true).ToListAsync();
        }
        public async Task AddUserAsync(User user)
        {
            await _db.Users.InsertOneAsync(user);
        }


        public async void DeleteEmployee(int userId)
        {
            var deletefilter = Builders<User>.Filter.Eq("id", userId);
            //var foundEmployee = _db.Users.Find(e => e.id == userId);

            await _db.Users.DeleteOneAsync(deletefilter);
        }

        //private readonly IMongoCollection<User> _users;
        //public UserService(IUserDatabaseSettings settings)
        //{
        //    //var client = new MongoClient(settings.ConnectionString);

        //    var client = new MongoClient("mongodb+srv://admin:79aeWlXTYUDjTHrL@cluster0.paiv8.azure.mongodb.net/<Users>?retryWrites=true&w=majority");
        //    var database = client.GetDatabase(settings.DatabaseName);

        //    _users = database.GetCollection<User>(settings.UsersCollectionName);

        //}

        //public List<User> GetAllUsers()
        //{
        //    List<User> users;
        //    users = _users.Find(emp => true).ToList();
        //    return users;
        //}

        //public User GetUserById(string id)
        //{
        //    return _users.Find<User>(emp => emp.id == id).FirstOrDefault();
        //}




    }
}
