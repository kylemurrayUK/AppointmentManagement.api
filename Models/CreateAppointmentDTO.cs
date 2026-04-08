namespace AppointmentManagementAPI
{
    /// <summary>
    /// Represents data needed to create an appointment
    /// </summary>
    public class CreateAppointmentDTO
    {
        /// <summary>
        /// Patient name
        /// </summary>
        public string? Patient {get; set;}
        /// <summary>
        /// Department appoint sits with
        /// </summary>
        public string? Department {get; set;}
        /// <summary>
        /// Clinician patient will be seeing
        /// </summary>
        public string? Clinician {get; set;}
        public DateTime AppointmentTime{get; set;}
    }
}