using AutoMapper;
using ToDoList.Endpoint.Models.Requests;
using Db = ToDoList.Persistence.Models;
using Domain = ToDoList.Endpoint.Models.Domain;

namespace ToDoList.Endpoint.Profiles;

public class ToDoItemProfile : Profile
{
    public ToDoItemProfile()
    {
        CreateMap<AddItemRequest, Db.ToDoItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        CreateMap<Db.ToDoItem, Domain.ToDoItem>().ReverseMap();
    }
}