using Microsoft.AspNetCore.Mvc;

namespace AppointmentManagementAPI
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AppointmentManagementController : ControllerBase
    {
        private AppointmentService _appointmentService;
        public AppointmentManagementController(AppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public ActionResult<List<Appointment>> ListAppointments()
        {
            
            return _appointmentService.ListAppointments();
        }

        [HttpPost]
        public IActionResult CreateAppointment([FromBody] CreateAppointmentDTO createAppointmentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Appointment appointment =_appointmentService.AddAppointment(createAppointmentDTO);

            return Created("", appointment);
        }
    }
}