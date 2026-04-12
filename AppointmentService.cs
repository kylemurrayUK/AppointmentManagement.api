using System.Transactions;

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


        // below would be cleaner in one method using linq but for now I'm keeping it simple
        // - will refactor later.
        public List<Appointment> GetPatientAppointments(string patient)
        {
            List<Appointment> patientsAppointments = new List<Appointment>();

            foreach (Appointment appointment in _appointments)
            {
                if (appointment.Patient == patient)
                {
                    patientsAppointments.Add(appointment);
                }
            }

            return patientsAppointments;
        }
        public List<Appointment> GetClinicianAppointments(string clinician)
        {
            List<Appointment> cliniciansAppointments = new List<Appointment>();

            foreach (Appointment appointment in _appointments)
            {
                if (appointment.Clinician == clinician)
                {
                    cliniciansAppointments.Add(appointment);
                }
            }

            return cliniciansAppointments;
        }

        public List<Appointment> GetDepartmentAppointments(string department)
        {
            List<Appointment> departmentAppointments = new List<Appointment>();

            foreach (Appointment appointment in _appointments)
            {
                if (appointment.Department == department)
                {
                    departmentAppointments.Add(appointment);
                }
            }

            return departmentAppointments;
        }

        public Appointment AddAppointment(CreateAppointmentDTO createAppointmentDTO)
        {
            int id = FindNextID(_appointments);
            Appointment newAppointment = new Appointment(id, createAppointmentDTO.Patient, createAppointmentDTO.Department, createAppointmentDTO.Clinician, AppointmentStatus.Pending, createAppointmentDTO.AppointmentTime);
            _appointments.Add(newAppointment);
            _fileStorage.SaveFile(_appointments);
            return newAppointment;
        }

        // No delete method as in a medical conext you would want to keep all appointments
        // - even cancelled ones - for auditing purposes.
        public (bool wasSuccessful, string message) ChangeAppointmentStatus(ChangeAppointmentStatusDTO changeAppointmentStatusDTO)
        {
            bool wasSuccessful = false;
            string message = "No action taken";

            if (DoesAppointmentExist(changeAppointmentStatusDTO.Id))
            {
                foreach(Appointment appointment in _appointments)
                {
                    if(appointment.Id == changeAppointmentStatusDTO.Id)
                    {
                        if (appointment.Status == changeAppointmentStatusDTO.Status)
                        {
                            message = $"Appointment was already {changeAppointmentStatusDTO.Status}.\n" + 
                                        $"Appointment Status : {appointment.Status}";
                            wasSuccessful = true;
                        } 
                        else
                        {
                            appointment.Status = changeAppointmentStatusDTO.Status;
                            message = $"Appointment status successfully changed to {changeAppointmentStatusDTO.Status}";
                            wasSuccessful = true;
                        }

                    }
                }
                _fileStorage.SaveFile(_appointments);
            }
            return (wasSuccessful, message);
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