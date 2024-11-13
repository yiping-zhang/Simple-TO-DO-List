using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using ToDoList.Endpoint.Models.Domain;
using ToDoList.Endpoint.Models.Requests;
using ToDoList.Endpoint.Services;

namespace ToDoList.Endpoint.Controllers;

[ApiController]
[Route("to-do-items")]
public class ToDoController: ControllerBase
{
    private readonly IToDoListService _toDoListService;
    private readonly IValidator<AddItemRequest> _validator;

    public ToDoController( IToDoListService service, IValidator<AddItemRequest> validator)
    {
        _validator = validator;
        _toDoListService = service;
    }

    [HttpGet()]
    public async Task<List<ToDoItem>> GetItems()
    {
        return await _toDoListService.GetItems();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveItem(int id)
    {
        await _toDoListService.RemoveItem(id);
        return NoContent();
    }

    [HttpPost()]
    public async Task<IActionResult> AddItem(AddItemRequest newItem)
    {
        var validationResult = await _validator.ValidateAsync(newItem);
        if (!validationResult.IsValid)
        {
            foreach (var failure in validationResult.Errors)
            {
                ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
            }

            return BadRequest(ModelState);
        }

        var newId = await _toDoListService.AddItem(newItem);
        return Created(string.Empty, new { Id = newId });
    }
}