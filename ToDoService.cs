namespace ToDoManagerAPI
{
    class ToDoService
    {
        private List<ToDoItem> _tasks;
        private FileStorage _fileStorage;

        public ToDoService(FileStorage fileStorage)
        {
            _fileStorage = fileStorage;
            _tasks = fileStorage.LoadFile();
        }

        public void ListTasks()
        {
            foreach (ToDoItem task in _tasks)
            {
                Console.WriteLine($"ID: {task.Id}\n" +
                                  $"Title: {task.Title}\n" +
                                  $"Description: {task.Description}\n" +
                                  $"Completed?: {task.IsCompleted}\n" +
                                  $"Created on: {task.CreatedAt}");
                if (task.IsCompleted)
                {
                    Console.WriteLine($"Tasks completed at: {task.CompletedAt}\n");
                }
                else
                {
                    Console.WriteLine("\n");
                }
            }
        }
        public void AddTask(string title, string description)
        {
            int id = FindNextID(_tasks);
            ToDoItem newTask = new ToDoItem(id, title, description);
            _tasks.Add(newTask);
            _fileStorage.SaveFile(_tasks);
        }
        public void CompleteTask(int inputtedID)
        {
            int matchCounter = 0;
            foreach(ToDoItem task in _tasks)
            {
                if (task.Id == inputtedID && task.IsCompleted == true)
                {
                    Console.WriteLine("Task is already completed!");
                    matchCounter++;
                    continue;
                }
                else if(task.Id == inputtedID && matchCounter == 0)
                {
                    task.IsCompleted = true;
                    task.CompletedAt = DateTime.Now;
                    matchCounter++;
                    Console.WriteLine($"Task marked as complete with time of {task.CompletedAt}");
                }
            }
            _fileStorage.SaveFile(_tasks);
            if (matchCounter > 1)
            {
                Console.WriteLine("More than one match was found with that id. Only one has been updated.");
            }
            else if (matchCounter < 1)
            {
                Console.WriteLine("No match found");
            }
        }
        public void DeleteTask(int inputtedID)
        {
            int matchCounter = 0;
            int indexCounter = 0;
            int itemForDeletionIndex = 0;
            foreach(ToDoItem task in _tasks)
            {
                if(task.Id == inputtedID)
                {
                    if (matchCounter == 0)
                        {itemForDeletionIndex = indexCounter;}
                    matchCounter++;
                }
                indexCounter++;
            }
            if (matchCounter > 1)
            {
                Console.WriteLine("More than one item exists with this ID - no action taken");
            }
            else if (matchCounter == 1)
            {
                _tasks.Remove(_tasks[itemForDeletionIndex]);
                _fileStorage.SaveFile(_tasks);
            }
            else
            {
                Console.WriteLine("No match found");
            }
        }


        private int FindNextID(List<ToDoItem> Tasks)
        {
            int id;
            int highestID = 0;
            if (Tasks.Count() == 0)
            {
                id = 1;
            }
            else
            {
                foreach (ToDoItem task in Tasks)
                {
                    if (task.Id > highestID)
                    {
                        highestID = task.Id;
                    }
                } 
                id = highestID + 1;
            }
            return id;
        }
    }
}