namespace ToDoManagerAPI
{
    public class ToDoItem
    {
        public int Id {get; set;}
        public string Title {get; set;} = string.Empty;
        public string Description {get; set;} = string.Empty;
        public bool IsCompleted {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime? CompletedAt {get; set;}

        public ToDoItem(int id,string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
            IsCompleted = false;
            CreatedAt = DateTime.Now;
        }

    }
}