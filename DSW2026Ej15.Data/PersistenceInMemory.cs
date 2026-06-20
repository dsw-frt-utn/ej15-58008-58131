using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Text.Json;
using DSW2026Ej15.Data.Dto;
using DSW2026Ej15.Domain.Entidades;

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

        public IEnumerable<Speciality> GetSpecialities()
        {
            lock (_lock) return _specialities.ToList();
        }

        public Speciality? GetSpecialityById(Guid id)
        {
            lock (_lock) return _specialities.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Doctor> GetActiveDoctors()
        {
            lock (_lock) return _doctors.Where(d => d.IsActive).ToList();
        }

        public Doctor? GetActiveDoctorById(Guid id)
        {
            lock (_lock) return _doctors.FirstOrDefault(d => d.Id == id && d.IsActive);
        }

        public void AddDoctor(Doctor doctor)
        {
            lock (_lock) _doctors.Add(doctor);
        }

        public Doctor? GetDoctorById(Guid id)
        {
            lock (_lock) return _doctors.FirstOrDefault(d => d.Id == id);
        }

        public void DeleteDoctorById(Guid id)
        {
            lock (_lock) 
            {
                var doctor = _doctors.FirstOrDefault(d => d.Id == id);
                doctor.IsActive = false;
            }
        }

    public void SaveDoctor(Doctor doctor)
    {
        _doctors.Add(doctor);
    }
}