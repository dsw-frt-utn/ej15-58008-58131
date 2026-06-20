using DSW2026Ej15.Domain.Source;

namespace DSW2026Ej15.Domain.Interface;

public interface IPersistence
{
    IEnumerable<Speciality> GetSpecialities();
    Speciality? GetSpecialityById(Guid id);
    IEnumerable<Doctor> GetActiveDoctors();
    Doctor? GetActiveDoctorById(Guid id);
    Doctor? GetDoctorById(Guid id);
    void AddDoctor(Doctor doctor);
    void DeleteDoctorById(Guid id);
}
