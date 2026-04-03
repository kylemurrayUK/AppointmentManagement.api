using System.Text.Json;

namespace AppointmentManagementAPI
{
    class FileStorage
    {
        const string filePath = @"data\Appointments.json";
        public List<Appointment> LoadFile()
        {
            List<Appointment> loadedAppointments = new List<Appointment>();
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
                loadedAppointments = JsonSerializer.Deserialize<List<Appointment>>(File.ReadAllText(filePath));
            }
            catch (JsonException)
            {
                Console.WriteLine("Json exception thrown - invalid JSON in file. Empty list returned");
                return new List<Appointment>();
            }
            if (loadedAppointments == null)
            {
                Console.WriteLine("null file. Empty list returned");
                return new List<Appointment>();
            }
            return loadedAppointments;

        }
        public void SaveFile(List<Appointment> tasks)
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize<List<Appointment>>(tasks));   
        }
    }
}