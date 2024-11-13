namespace ToDoList.Endpoint.Models.Domain;
public class ToDoItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    //TODO add more properties
    //public int CreatedBy { get; set; }
}