using System;
using System.Collections.Generic;
using System.Text;

namespace DSW2026Ej15.Domain
{
    internal class Doctor
    {
        public string Name { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public Speciality Speciality { get; set; } = null;

    }
}
