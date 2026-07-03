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
    public async Task<IActionResult> CreateDoctor([FromBody] DoctorModel.Request request)
    {
        if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.LicenseNumber))
        {
            throw new ValidationException("Nombre y Matricula son requeridos");
        }
        var speciality = await _persistence.GetSpecialityByIdAsync(request.SpecialityId);
        if (speciality == null)
        {
            throw new ValidationException("Especialidad no Existe");
        }
        await _persistence.SaveDoctorAsync(new Doctor(request.Name, request.LicenseNumber, speciality));
        return Created();
    }

    [HttpGet("doctors")]
    public async Task<IActionResult> GetDoctors()
    {
        var doctors = await _persistence.GetAllDoctorsAsync();
        return Ok(doctors);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetDoctorById([FromRoute] Guid id)
    {
        var doctor = await _persistence.GetDoctorByIdAsync(id);
        if (doctor == null || doctor.IsActive == false)
        {
            throw new NotFoundException("Médico no encontrado o inactivo.");
        }
        return Ok(new { doctor.Name, doctor.LicenseNumber, SpecialityName = doctor.Speciality.Name });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctorById([FromRoute] Guid id)
    {
        var doctor = await _persistence.GetDoctorByIdAsync(id);
        if (doctor == null || doctor.IsActive == false)
        {
            throw new NotFoundException("Médico no encontrado o inactivo.");
        }
        await _persistence.DeleteDoctorAsync(doctor);
        return NoContent();
    }
}
