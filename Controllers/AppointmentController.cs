using Microsoft.AspNetCore.Mvc;

namespace AppointmentManagementAPI
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private AppointmentService _appointmentService;
        public AppointmentController(AppointmentService appointmentService)
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

            Appointment newAppointment =_appointmentService.AddAppointment(createAppointmentDTO);

            return CreatedAtAction(nameof(GetAppointment), new {iD = newAppointment.Id}, newAppointment);
        }
    }
}