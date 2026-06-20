using DSW2026Ej15.Api.Models;
using DSW2026Ej15.Domain.Interface;
using DSW2026Ej15.Domain.Source;
using Microsoft.AspNetCore.Mvc;
using DSW2026Ej15.Domain.Exceptions;

namespace DSW2026Ej15.Api.Controllers;

public class DoctorsController : AppController
{
    private readonly IPersistence _persistence;

    public DoctorsController(IPersistence persistence)
    {
        _persistence = persistence;
    }

    [HttpPost("doctors")]
    public async Task<IActionResult> CreateDoctor(DoctorModel.Request request) 
    {
        if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.LicenseNumber))
        {
            throw new ValidationException("Nombre y matricula son requeridos");
        }

        var speciality = _persistence.GetSpecialityById(request.SpecialityId);
        if (speciality is null)
        {
            throw new ValidationException("No existe Especialidad");
        }

        var doctor = new Doctor(request.Name, request.LicenseNumber, speciality);
        _persistence.AddDoctor(doctor);

        return Created();
    }

    [HttpGet("doctors")]
    public async Task<IActionResult> GetAllActiveDoctor()
    {
        var activeDoctor = _persistence.GetActiveDoctors();
        return Ok(activeDoctor);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetDoctor(Guid id)
    {
        var doctor = _persistence.GetDoctorById(id);
        if (doctor is null || !doctor.IsActive) return NotFound("Doctor no se encuentra");
        return Ok(doctor);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctor(Guid id)
    {
        var doctor = _persistence.GetDoctorById(id);
        if (doctor is null || !doctor.IsActive) return NotFound("Doctor no se encuentra");
        _persistence.DeleteDoctorById(id);
        return NoContent();
    }
}
