namespace AppointmentManagementAPI
{
    public class  AppointmentService
    {
        private List<Appointment> _appointments;
        private FileStorage _fileStorage;

        public AppointmentService(FileStorage fileStorage)
        {
            _fileStorage = fileStorage;
            _appointments = fileStorage.LoadFile();
        }

        public List<Appointment> ListAppointments()
        {
            return _appointments;
        }
        
        public Appointment AddAppointment(CreateAppointmentDTO createAppointmentDTO)
        {
            int id = FindNextID(_appointments);
            Appointment newAppointment = new Appointment(id, createAppointmentDTO.Patient, createAppointmentDTO.Department, createAppointmentDTO.Clinician, AppointmentStatus.Pending, createAppointmentDTO.AppointmentTime);
            _appointments.Add(newAppointment);
            _fileStorage.SaveFile(_appointments);
            return newAppointment;
        }
        public void ChangeAppointmentStatus(int inputtedID)
        {
            int matchCounter = 0;
            foreach(Appointment appointment in _appointments)
            {
                if (appointment.Id == inputtedID && appointment.Status == AppointmentStatus.Completed)
                {
                    Console.WriteLine("Appointment is already completed!");
                    matchCounter++;
                    continue;
                }
                else if(appointment.Id == inputtedID && matchCounter == 0)
                {
                    appointment.Status = AppointmentStatus.Completed;
                    matchCounter++;
                    Console.WriteLine($"Appointment marked as {appointment.Status}");
                }
            }
            _fileStorage.SaveFile(_appointments);
            if (matchCounter > 1)
            {
                Console.WriteLine("More than one match was found with that id. Only one has been updated.");
            }
            else if (matchCounter < 1)
            {
                Console.WriteLine("No match found");
            }
        }
        public void CancelAppointment(int inputtedID)
        {
            int matchCounter = 0;
            int indexCounter = 0;
            int itemForDeletionIndex = 0;
            foreach(Appointment appointment in _appointments)
            {
                if(appointment.Id == inputtedID)
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
                _appointments.Remove(_appointments[itemForDeletionIndex]);
                _fileStorage.SaveFile(_appointments);
            }
            else
            {
                Console.WriteLine("No match found");
            }
        }


        private int FindNextID(List<Appointment> Appointments)
        {
            int id;
            int highestID = 0;
            if (Appointments.Count() == 0)
            {
                id = 1;
            }
            else
            {
                foreach (Appointment appointment in Appointments)
                {
                    if (appointment.Id > highestID)
                    {
                        highestID = appointment.Id;
                    }
                } 
                id = highestID + 1;
            }
            return id;
        }
    }
}