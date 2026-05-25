using ExpenseTracker.API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ExpenseTracker.API.Services
{
    public class ExpenseService
    {
        private readonly IMongoCollection<Expense> _expensesCollection;

        public ExpenseService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var mongoClient = new MongoClient(
                mongoDbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                mongoDbSettings.Value.DatabaseName);

            _expensesCollection = mongoDatabase.GetCollection<Expense>(
                mongoDbSettings.Value.CollectionName);
        }

        public async Task<List<Expense>> GetAsync() =>
            await _expensesCollection.Find(_ => true).ToListAsync();

        public async Task CreateAsync(Expense newExpense) =>
            await _expensesCollection.InsertOneAsync(newExpense);

        public async Task RemoveAsync(string id) =>
            await _expensesCollection.DeleteOneAsync(x => x.Id == id);
    }
}