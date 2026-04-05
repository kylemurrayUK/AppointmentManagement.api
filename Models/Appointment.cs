using System.ComponentModel.DataAnnotations;

namespace AppointmentManagementAPI{
     public enum AppointmentStatus
    {
        Completed,
        Pending,
        Cancelled,
        EnteredInError
    }    




    public class Appointment
    {
        public int Id {get; set;}
        [Required]
        public string Patient {get; set;} = string.Empty;
        [Required]
        public string Department {get; set;} = string.Empty;
        [Required]
        public string Clinician {get; set;} = string.Empty;
        public AppointmentStatus Status {get; set;}
        [Required]
        public DateTime AppointmentTime {get; set;}

        public Appointment(int id,string patient, string department,
                           string clinician, AppointmentStatus status, DateTime appointmentTime)
        {
            Id = id;
            Patient = patient;
            Department = department;
            Clinician = clinician;
            Status = status;
            AppointmentTime = appointmentTime;
        }

    }
}