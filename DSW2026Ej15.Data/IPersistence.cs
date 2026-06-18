using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace DSW2026Ej15.Data;

internal interface IPersistence
{
    IEnumerable<Speciality> GetSpecialities();
    Speciality? GetSpecialityById(Guid id);
    IEnumerable<Doctor> GetActiveDoctors();
    Doctor? GetActiveDoctorById(Guid id);
    Doctor? GetDoctorById(Guid id);
    void AddDoctor(Doctor doctor);
}
