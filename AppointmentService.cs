namespace AppointmentManagementAPI
{
    public class  AppointmentService
    {
        private List<Appointment> _appointments;
        private IFileStorage _fileStorage;

        public AppointmentService(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
            _appointments = fileStorage.LoadFile();
        }

        public List<Appointment> ListAppointments()
        {
            return _appointments;
        }
        
        public Appointment? GetAppointment(int iD)
        {
            Appointment? appointmentToReturn = null;

            foreach (Appointment appointment in _appointments)
            {
                if (appointment.Id == iD)
                    appointmentToReturn = appointment;
            }

            return appointmentToReturn;
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
        public bool CancelAppointment(int inputtedID)
        {
            bool wasSuccessful = false;
            Appointment? appointmentToDelete = null;
            if (DoesAppointmentExist(inputtedID))
            {
                foreach (Appointment appointment in _appointments)
                {
                    if (appointment.Id == inputtedID)
                    {
                        appointmentToDelete = appointment;
                    }
                }
                _appointments.Remove(appointmentToDelete!);
                _fileStorage.SaveFile(_appointments);    
                wasSuccessful = true;            
            }
            return wasSuccessful; 

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

        public bool DoesAppointmentExist(int inputtedID)
        {
            bool doesAppointmentExist = false;
            foreach(Appointment appointment in _appointments)
            {
                if(appointment.Id == inputtedID)
                {
                    doesAppointmentExist = true;
                }
            }

            return doesAppointmentExist;
        }
    }
}