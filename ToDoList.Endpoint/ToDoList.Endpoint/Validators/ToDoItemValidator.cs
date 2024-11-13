using FluentValidation;
using ToDoList.Endpoint.Models.Requests;

namespace ToDoList.Endpoint.Validators;
public class ToDoItemValidator: AbstractValidator<AddItemRequest>
{
    public ToDoItemValidator()
    {
        RuleFor(item => item.Name)
            .NotEmpty().WithMessage("A task list item must have a name");
    }
}