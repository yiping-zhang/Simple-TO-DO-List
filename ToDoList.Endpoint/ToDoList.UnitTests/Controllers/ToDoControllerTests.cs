using FluentValidation;
using ToDoList.Endpoint.Controllers;
using ToDoList.Endpoint.Models.Requests;
using ToDoList.Endpoint.Services;
using NSubstitute;
using TestStack.BDDfy;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using ToDoList.Endpoint.Validators;
using ToDoList.Endpoint.Models.Responses;

namespace ToDoList.UnitTests.Controllers;

[TestFixture]
public class ToDoControllerTests
{
    private IToDoListService _toDoListService;
    private IValidator<AddItemRequest> _validator;

    private ToDoController _subject;

    private AddItemRequest _request;
    private IActionResult _result;

    [SetUp]
    public void SetUp()
    {
        _request = new AddItemRequest();

        _toDoListService = Substitute.For<IToDoListService>();
        _validator = new ToDoItemValidator();

        _subject = new ToDoController(_toDoListService, _validator);
    }

    [Test]
    public void ItShouldAddItem()
    {
        this.Given(x => x.GivenANewItemWithName("Utilities Restoration"))
            .When(x => x.WhenAddTheNewItem())
            .Then(x => x.ThenItemShouldBeAddedSuccessful())
            .BDDfy();
    }

    [Test]
    public void ItShouldThrowExceptionWhenAddItemWithoutAName()
    {
        this.Given(x => x.GivenANewItemWithName(""))
            .When(x => x.WhenAddTheNewItem())
            .Then(x => x.ThenBadRequestShouldBeReturned())
            .And(x => x.ThenItemShouldNotBeAdded())
            .BDDfy();
    }

    private void GivenANewItemWithName(string itemName)
    {
        _request.Name = itemName;
    }

    private async Task WhenAddTheNewItem()
    {
        _result = await _subject.AddItem(_request);
    }

    public void ThenBadRequestShouldBeReturned()
    {
        var badRequestResult = _result as BadRequestObjectResult;
        badRequestResult.ShouldNotBeNull();
        badRequestResult.StatusCode.ShouldBe(400);
    }

    public void ThenItemShouldNotBeAdded()
    {
        _toDoListService.DidNotReceive().AddItem(Arg.Any<AddItemRequest>());
    }

    public void ThenItemShouldBeAddedSuccessful()
    {
        var successfulResponse = _result as CreatedResult;
        successfulResponse.ShouldNotBeNull();
        successfulResponse.StatusCode.ShouldBe(201);
        _toDoListService.Received(1).AddItem(Arg.Any<AddItemRequest>());
    }
}