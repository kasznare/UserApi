using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace OneIdentityApi.Models
{
    //Later on, you should have this context class implement an interface so you can inject mock class instances for testing.
    public class AppDbContext
    {
        private readonly IMongoDatabase _db;
        //constructor will inject an instance
        public AppDbContext(IMongoClient client, string dbName)
        {
            _db = client.GetDatabase(dbName);
        }

        public IMongoCollection<User> Users => _db.GetCollection<User>("users");
        public IMongoCollection<Article> Articles => _db.GetCollection<Article>("articles");

    }
}
