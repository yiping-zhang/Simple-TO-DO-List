using ToDoList.Persistence.Exceptions;
using ToDoList.Persistence.Models;

namespace ToDoList.Persistence.Repositories;
public class ToDoRepository: IToDoRepository
{
    //TODO Hook up with a database and replace with DbContext
    private static List<ToDoItem> _defaultItems = new List<ToDoItem> { 
        new ToDoItem
        {
            Id = 1,
            Name = "Ensure safety first",
            CreatedAt = DateTimeOffset.UtcNow
        },
        new ToDoItem
        {
            Id = 2,
            Name = "Prevent further damage",
            CreatedAt = DateTimeOffset.UtcNow
        },
        new ToDoItem
        {
            Id = 3,
            Name = "Initial clean-up and debris removal",
            CreatedAt = DateTimeOffset.UtcNow
        },
        new ToDoItem
        {
            Id = 4,
            Name = "Access and repair structural damage",
            CreatedAt = DateTimeOffset.UtcNow
        }
    };

    public async Task<List<ToDoItem>> GetItems()
    {
        return _defaultItems;
    }

    public async Task RemoveItem(int id)
    {
        var elementToBeRemoved = _defaultItems.Find(x => x.Id == id);
        if (elementToBeRemoved == null)
        {
            throw new NotFoundException($"Item with id: {id} cannot be found");
        }
        _defaultItems.Remove(elementToBeRemoved);
    }

    public async Task<int> AddItem(ToDoItem newItem)
    {
        var newId = _defaultItems.Last().Id + 1;
        newItem.Id = newId;
        _defaultItems.Add(newItem);
        return newId;
    }
}