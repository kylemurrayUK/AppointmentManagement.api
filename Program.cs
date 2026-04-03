
using AppointmentManagementAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<FileStorage>();
builder.Services.AddSingleton<AppointmentService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}



app.MapGet("/todoapp", (AppointmentService appointmentService) =>
{
    return appointmentService.ListTasks();
})
.WithName("todoappView");

app.MapPost("/todoapp/CreateTask", (AppointmentService appointmentService, CreateAppointmentDTO createTaskDTO) =>
{
    appointmentService.AddTask(createTaskDTO);
});

app.Run();

