using DSW2026Ej15.Domain.Interface;
using DSW2026Ej15.Domain.Source;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DSW2026Ej15.Data;

public class PersistenceEf : IPersistence
{
    private readonly DSW2026Ej15DbContext _context;
    public PersistenceEf(DSW2026Ej15DbContext context)
    {
        _context = context;
    }
    public async Task DeleteDoctorAsync(Doctor doctor)
    {
        doctor.IsActive = false;
        await _context.SaveChangesAsync();
    }
    public async Task<Doctor?> GetDoctorByIdAsync(Guid Id) => await _context.Doctors.Include(d => d.Speciality).FirstOrDefaultAsync(d => d.Id == Id && d.IsActive);
    public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync() => await _context.Doctors.Include(d => d.Speciality).Where(d => d.IsActive).ToListAsync();
    public async Task<List<Speciality>> GetSpecialitiesAsync() => await _context.Specialities.ToListAsync();
    public async Task<Speciality?> GetSpecialityByIdAsync(Guid Id) => await _context.Specialities.FirstOrDefaultAsync(s => s.Id == Id);
    public async Task SaveDoctorAsync(Doctor doctor)
    {
        await _context.AddAsync(doctor);
        await _context.SaveChangesAsync();
    }
}
