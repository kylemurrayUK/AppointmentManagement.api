using System.Text.Json.Serialization;

namespace AppointmentManagementAPI
{
    /// <summary>
    /// Enum for Appointment status
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AppointmentStatus
    {
        Completed,
        Pending,
        Cancelled,
        EnteredInError
    }    
}
