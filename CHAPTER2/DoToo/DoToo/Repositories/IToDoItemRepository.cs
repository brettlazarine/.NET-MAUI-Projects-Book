using DoToo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoToo.Repositories
{
    public interface IToDoItemRepository
    {
        event EventHandler<ToDoItem> OnItemAdded;
        event EventHandler<ToDoItem> OnItemUpdated;

        Task<List<ToDoItem>> GetItemsAsync();
        Task AddItemAsync(ToDoItem item);
        Task UpdateItemAsync(ToDoItem item);
        Task AddOrUpdateAync(ToDoItem item);
    }
}
