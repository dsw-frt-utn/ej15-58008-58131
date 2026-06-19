using DSW2026Ej15.Domain;

namespace DSW2026Ej15.Data;

public interface IPersistence
{
    IEnumerable<Speciality> GetSpecialities();
    Speciality? GetSpecialityById(Guid id);
    IEnumerable<Doctor> GetActiveDoctors();
    Doctor? GetActiveDoctorById(Guid id);
    Doctor? GetDoctorById(Guid id);
    void AddDoctor(Doctor doctor);
}
