# Appointment Management API

## Overview
An ASP.NET web api for managing appointments which is RESTful compliant. 


## Features
Users are able to 
 - Create an appointment
 - View all appointments
 - View a specific appointment via the appointment ID
 - View a list of appointments filtered by either the patient name, clinician name, or department name.
 - Change status of an appointment to Pending, Completed, Cancelled, EnteredInError

All the above are implemented using HTTP requests. Please see How to Run for specifics.

## Technical Highlights
 - Implementation of an ASP.NET controller based api
 - Routing implemented in Controller class
 - Clear separation of concerns between Controller handling HTTP requests, Service class handling business logic, and Filestorage class handling file storage
 - Dependency injection used, specifically a singleton lifetime for the AppointmentService and FileStorage objects.
 - Implementation of an interface for the file storage class.
 - Use of enum for appointment status 
 - Appropriate status codes, including feedback in the form of error messages or success messsages
 - Appropriate attribute tagging to fully utilize model binding and error handling features of ASP.NET

## Design Decisions
Below are some design decisions that are worth explaining
 - The use of singletons in the dependency injection container as deemed appropriate as there is an in-memory list loaded for the appointments that needs to persist for the entire run time of the application. When a database is implemented in a later project then either a scoped or transient lifetime will be more appropriate.
 - A delete request is noticeably absent from the http methods implemented. This is because in a medical context you wouldn't want data being deleted entirely for auditing purposes. I decided instead have an EnteredInError status code to keep the implementation domain appropriate.
 - A separate patient and clinician object that contains ID's would be implemented in a real-world NHS appointment system. However, the focus of this project was to implement my first ASP.NET api based on the previous project, the ToDoManager. In order to keep this project focused this implementation was deemed out of scope and will be implemented in a later project.
 
A brief discussion on my thought process behind my choice of status codes
 - All successful get requests return a 200 Ok request. When these requests failed I generally returned a 400 Bad Request except the single appointment lookup returns a 404 not found. These seem the most appropriate and industry standard.
 - In my post request I return a 201 Created response when successful using the CreatedAtAction method that returns the location of the new resource along with its value in the response body. This returns a 400 Bad Request if the model state is invalid
 - For the patch request, I return a 404 not found if the appointment id isn't found and I return a 200 response if appointment status is successfully edited. This is so I can catch the case where the user has tried to change the status to the same status. The body of the 200 response will contain this message. 

## How to Run
Currently not deployed as a standalone application - so once you have pulled the repo run dotnet build then dotnet run.

## Endpoints

### List All Appointments
```
GET /api/appointment/listappointments
```
Response `200 OK`:
```json
[
    {
        "Id": 1,
        "Patient": "John Smith",
        "Clinician": "Dr Jones",
        "Department": "Cardiology",
        "AppointmentTime": "2026-04-15T09:00:00",
        "Status": "Pending"
    }
]
```

---

### Get Appointment by ID
```
GET /api/appointment/getappointment/{id}
```
Response `200 OK`:
```json
{
    "Id": 1,
    "Patient": "John Smith",
    "Clinician": "Dr Jones",
    "Department": "Cardiology",
    "AppointmentTime": "2026-04-15T09:00:00",
    "Status": "Pending"
}
```
Response `404 Not Found`

---

### Get Appointments by Parameter
```
GET /api/appointment/getappointments?patient=John Smith
GET /api/appointment/getappointments?clinician=Dr Jones
GET /api/appointment/getappointments?department=Cardiology
```
Response `200 OK`:
```json
[
    {
        "Id": 1,
        "Patient": "John Smith",
        "Clinician": "Dr Jones",
        "Department": "Cardiology",
        "AppointmentTime": "2026-04-15T09:00:00",
        "Status": "Pending"
    }
]
```
Response `400 Bad Request` - `"No query included"`  
Response `400 Bad Request` - `"More than one query parameter not allowed"`

---

### Create Appointment
```
POST /api/appointment/createappointment
```
Request body:
```json
{
    "Patient": "John Smith",
    "Clinician": "Dr Jones",
    "Department": "Cardiology",
    "AppointmentTime": "2026-04-15T09:00:00"
}
```
Response `201 Created`:
```json
{
    "Id": 1,
    "Patient": "John Smith",
    "Clinician": "Dr Jones",
    "Department": "Cardiology",
    "AppointmentTime": "2026-04-15T09:00:00",
    "Status": "Pending"
}
```
Response `400 Bad Request` - Invalid or missing fields

---

### Change Appointment Status
```
PATCH /api/appointment/changeappointmentstatus
```
Request body:
```json
{
    "Id": 1,
    "Status": "Completed"
}
```
Response `200 OK` - `"Appointment status successfully changed to Completed"`  
Response `200 OK` - `"Appointment was already Completed. Appointment Status: Completed"`  
Response `404 Not Found` - `"Appointment not found"`

## Project Structure
```
├── Controllers/
│   └── AppointmentController.cs       # Handles HTTP requests
├── DTOs/
│   ├── ChangeAppointmentStatusDTO.cs  # DTO for changing appointment status,  just includes ID and status
│   └── CreateAppointmentDTO.cs        # DTO for creating appointment, excludes Id and appointment status
├── Models/
│   ├── Appointment.cs                 # Defines structure of appointments
│   └── AppointmentStatus.cs           # Enum for appointment status codes  
├── AppointmentService.cs              # Service layer object that handles business logic
├── FileStorage.cs                     # Handles Saving and loading data
├── IFileStorage.cs                    # Interface for Filestorage
└── Program.cs                         # Program entry point
```