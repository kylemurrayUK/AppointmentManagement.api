namespace AppointmentManagementAPI
{
    public class CreateAppointmentDTO
    {
        public string? Patient {get; set;}
        public string? Department {get; set;}
        public string? Clinician {get; set;}
        public DateTime AppointmentTime{get; set;}
    }
}