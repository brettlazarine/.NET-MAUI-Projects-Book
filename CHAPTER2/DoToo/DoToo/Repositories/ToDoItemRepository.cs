using DoToo.Models;
using SQLite;

namespace DoToo.Repositories
{
    public class ToDoItemRepository : IToDoItemRepository
    {
        private SQLiteAsyncConnection _connection;

        public event EventHandler<ToDoItem> OnItemAdded;
        public event EventHandler<ToDoItem> OnItemUpdated;

        private async Task CreateConnectionAsync()
        {
            if (_connection is not null)
            {
                return;
            }

            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var databasePath = Path.Combine(documentPath, "ToDoItems.db");

            _connection = new SQLiteAsyncConnection(databasePath);
            await _connection.CreateTableAsync<ToDoItem>();

            if (await _connection.Table<ToDoItem>().CountAsync() is 0)
            {
                await _connection.InsertAsync(new ToDoItem()
                {
                    Title = "Welcome to DoToo",
                    Due = DateTime.Now
                });
            }
        }

        public async Task AddItemAsync(ToDoItem item)
        {
            await CreateConnectionAsync();
            await _connection.InsertAsync(item);
            OnItemAdded?.Invoke(this, item);
        }

        public async Task AddOrUpdateAync(ToDoItem item)
        {
            if (item.Id is 0)
            {
                await AddItemAsync(item);
            }
            else
            {
                await UpdateItemAsync(item);
            }
        }

        public async Task<List<ToDoItem>> GetItemsAsync()
        {
            await CreateConnectionAsync();
            return await _connection.Table<ToDoItem>().ToListAsync();
        }

        public async Task UpdateItemAsync(ToDoItem item)
        {
            await CreateConnectionAsync();
            await _connection.UpdateAsync(item);
            OnItemUpdated?.Invoke(this, item);
        }
    }
}
