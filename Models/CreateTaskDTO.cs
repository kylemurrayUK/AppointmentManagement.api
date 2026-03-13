using Microsoft.AspNetCore.Components.Web;

namespace ToDoManagerAPI
{
    class CreateTaskDTO
    {
        public string? Title {get; set;}
        public string? Description {get; set;}
    }
}