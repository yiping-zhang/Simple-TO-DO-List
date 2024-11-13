using ToDoList.Endpoint.Models.Domain;
using ToDoList.Endpoint.Models.Requests;

namespace ToDoList.Endpoint.Services;
public interface IToDoListService
{
    Task<List<ToDoItem>> GetItems();
    Task RemoveItem(int id);
    Task<int> AddItem(AddItemRequest newItem);
}