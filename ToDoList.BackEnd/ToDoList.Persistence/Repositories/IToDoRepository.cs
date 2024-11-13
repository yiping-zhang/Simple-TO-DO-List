using ToDoList.Persistence.Models;

namespace ToDoList.Persistence.Repositories;

public interface IToDoRepository
{
    Task<List<ToDoItem>> GetItems();
    Task RemoveItem(int id);
    Task<int> AddItem(ToDoItem newItem);
}