using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Text.Json;

namespace DSW2026Ej15.Data
{
    internal class PersistenceInMemory : IPersistence
    {
        private readonly List<Speciality> _specialities = new();
        private readonly List<Doctor> _doctors = new();
        private readonly object _lock = new();

        public PersistenceInMemory()
        {
            LoadSpecialitiesAsync().GetAwaiter().GetResult();
        }

        private async Task LoadSpecialitiesAsync()
        {
            try
            {
                string filePath = Path.Combine(AppContext.BaseDirectory, "specialities.json");

                if (File.Exists(filePath))
                {
                    string json = await File.ReadAllTextAsync(filePath);

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var list = JsonSerializer.Deserialize<List<Speciality>>(json, options);

                    if (list != null)
                    {
                        // Bloqueamos la lista sólo el instante en que agregamos los elementos
                        lock (_lock)
                        {
                            _specialities.AddRange(list);
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"[Advertencia] No se encontró el archivo JSON en: {filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Al cargar especialidades de forma asíncrona: {ex.Message}");
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
}
