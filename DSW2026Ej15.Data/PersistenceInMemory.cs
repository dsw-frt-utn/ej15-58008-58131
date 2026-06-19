using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Text.Json;
using DSW2026Ej15.Data.Dto;

namespace DSW2026Ej15.Data;
    internal class PersistenceInMemory : IPersistence
    {
        private readonly List<Speciality> _specialities = [];
        private readonly List<Doctor> _doctors = [];
        private readonly object _lock = new();

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

                _specialities = [.. specialities.Select(s => new Speciality(s.Name, s.Description, s.Id))];
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
    }