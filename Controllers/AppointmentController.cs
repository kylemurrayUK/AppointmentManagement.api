using Microsoft.AspNetCore.Mvc;

namespace AppointmentManagementAPI
{
    [ApiController]
    [Route("appointments")]
    public class AppointmentManagementAPIController : ControllerBase
    {
        private AppointmentService _appointmentService;
        public AppointmentManagementAPIController(AppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        [HttpGet]
        public ActionResult<List<Appointment>> ListAppointments()
        {
            
            return Ok(_appointmentService.ListAppointments());
        }
    }
}