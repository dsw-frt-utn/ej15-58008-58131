using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Text.Json;
using DSW2026Ej15.Data.Dto;
using DSW2026Ej15.Domain.Source;
using DSW2026Ej15.Domain.Interface;

namespace DSW2026Ej15.Data;
    public class PersistenceInMemory : IPersistence
    {
        private List<Speciality> _specialities = [];
        private List<Doctor> _doctors = [];
        private object _lock = new();

        public PersistenceInMemory()
        {
            LoadSpecialities();
        }

        private void LoadSpecialities()
        {
            try
            {
                string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    "Sources", "specialities.json");

                var json = File.ReadAllText(jsonPath);

                var specialities = JsonSerializer.Deserialize<List<SpecialityDto>>(json,
                    new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? [];

                _specialities = [.. specialities.Select(s => new Speciality(s.name, s.description, s.id))];
            }
            catch (Exception)
            {
                
            }
        }

        public async Task<IEnumerable<Speciality>> GetSpecialitiesAsync()
        {
            lock (_lock) return _specialities.ToList();
        }

        public async Task<Speciality?> GetSpecialityByIdAsync(Guid id)
        {
            lock (_lock) return _specialities.FirstOrDefault(s => s.Id == id);
        }

        public async Task<IEnumerable<Doctor>> GetActiveDoctorsAsync()
        {
            lock (_lock) return _doctors.Where(d => d.IsActive).ToList();
        }

        public async Task<Doctor?> GetActiveDoctorByIdAsync(Guid id)
        {
            lock (_lock) return _doctors.FirstOrDefault(d => d.Id == id && d.IsActive);
        }

        public async Task AddDoctorAsync(Doctor doctor)
        {
            lock (_lock) _doctors.Add(doctor);
        }

        public async Task<Doctor?> GetDoctorByIdAsync(Guid id)
        {
            lock (_lock) return _doctors.FirstOrDefault(d => d.Id == id);
        }

        public async Task DeleteDoctorByIdAsync(Guid id)
        {
            lock (_lock) 
            {
                var doctor = _doctors.FirstOrDefault(d => d.Id == id);
                doctor.IsActive = false;
            }
        }

        public async Task SaveDoctorAsync(Doctor doctor)
        {
            lock (_lock) _doctors.Add(doctor);
        }

    public Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
    {
        throw new NotImplementedException();
    }

    public Task DeleteDoctorAsync(Doctor doctor)
    {
        throw new NotImplementedException();
    }
}