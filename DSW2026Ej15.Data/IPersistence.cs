using DSW2026Ej15.Domain.Entidades;

namespace DSW2026Ej15.Data;

public interface IPersistence
{
    IEnumerable<Speciality> GetSpecialities();
    Speciality? GetSpecialityById(Guid id);
    void SaveDoctor(Doctor doctor);
    IEnumerable<Doctor> GetActiveDoctors();
    Doctor? GetActiveDoctorById(Guid id);
    Doctor? GetDoctorById(Guid id);
    void AddDoctor(Doctor doctor);
    void DeleteDoctorById(Guid id);
}
