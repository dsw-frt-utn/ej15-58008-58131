using DSW2026Ej15.Data;
using Microsoft.AspNetCore.Mvc;

namespace DSW2026Ej15.Api.Controllers;

public class DoctorsController : AppController
{
    private readonly IPersistence _persistence;

    public DoctorsController(IPersistence persistence)
    {
        _persistence = persistence;
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
