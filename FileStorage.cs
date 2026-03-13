using System.Text.Json;

namespace ToDoManagerAPI
{
    class FileStorage
    {
        const string filePath = @"data\Tasks.json";
        public List<ToDoItem> LoadFile()
        {
            List<ToDoItem> loadedTasks = new List<ToDoItem>();
            if (!Directory.Exists(@"data"))
            {
                Directory.CreateDirectory(@"data");
            }
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]");
            }
            try
            {
                loadedTasks = JsonSerializer.Deserialize<List<ToDoItem>>(File.ReadAllText(filePath));
            }
            catch (JsonException)
            {
                Console.WriteLine("Json exception thrown - invalid JSON in file. Empty list returned");
                return new List<ToDoItem>();
            }
            if (loadedTasks == null)
            {
                Console.WriteLine("null file. Empty list returned");
                return new List<ToDoItem>();
            }
            return loadedTasks;

        }
        public void SaveFile(List<ToDoItem> tasks)
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize<List<ToDoItem>>(tasks));   
        }
    }
}