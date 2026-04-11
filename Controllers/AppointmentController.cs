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

        [HttpGet("{Id}")]
        public ActionResult<Appointment> GetAppointment(int iD)
        {
            var appointment = _appointmentService.GetAppointment(iD);

            if (appointment == null)
            {
                return NotFound();
            }
            return Ok(appointment);
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