using AutoMapper;
using ToDoList.Endpoint.Models.Domain;
using Db = ToDoList.Persistence.Models;
using ToDoList.Endpoint.Models.Requests;
using ToDoList.Persistence.Repositories;

namespace ToDoList.Endpoint.Services;
public class ToDoListService : IToDoListService
{
    private readonly IMapper _mapper;
    private readonly IToDoRepository _toDoRepository;

    public ToDoListService(IMapper mapper, IToDoRepository toDoRepository)
    {
        _mapper = mapper;
        _toDoRepository = toDoRepository;
    }

    public async Task<int> AddItem(AddItemRequest newItem)
    {
        var dbItem = _mapper.Map<Db.ToDoItem>(newItem);
        dbItem.CreatedAt = DateTimeOffset.UtcNow;
        var newId = await _toDoRepository.AddItem(dbItem);
        return newId;
    }

    public async Task<List<ToDoItem>> GetItems()
    {
        var dbModels = await _toDoRepository.GetItems();
        return dbModels.Select(_mapper.Map<ToDoItem>).ToList();
    }

    public async Task RemoveItem(int id)
    {
        await _toDoRepository.RemoveItem(id);
    }
}