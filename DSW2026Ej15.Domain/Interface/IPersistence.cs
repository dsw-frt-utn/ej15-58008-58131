using DSW2026Ej15.Domain.Source;

namespace DSW2026Ej15.Domain.Interface;

public interface IPersistence
{
    Task<Speciality?> GetSpecialityByIdAsync(Guid Id);
    Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
    Task<Doctor?> GetDoctorByIdAsync(Guid Id);
    Task SaveDoctorAsync(Doctor doctor);
    Task DeleteDoctorAsync(Doctor doctor);
}
