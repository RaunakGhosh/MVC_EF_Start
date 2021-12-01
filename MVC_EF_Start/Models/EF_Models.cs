using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC_EF_Start.Models
{
    public class PatientsRecord
    {
        [Key]
        public int patientID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int age { get; set; }
        public string email { get; set; }
        public int mobile { get; set; }
        public List<PrescriptionRecord> Prescriptions { get; set; }
        public List<AppointmentRecord> Appointments { get; set; }
    }

    public class AppointmentRecord
    {
        [Key]
        public int appointmentID { get; set; }
        public PatientsRecord patient { get; set; }
        public doctorVisit doc { get; set; }
        public DateTime dateVisited { get; set; }
    }
    public class PrescriptionRecord
    {
        [Key]
        public int prescriptionID { get; set; }
        public PatientsRecord patient { get; set; }
        public doctorVisit doc { get; set; }
        public AppointmentRecord appoint { get; set; }
        public List<MedicineRecord> Medicines { get; set; }

    }
    public class doctorVisit
    {
        [Key]
        public int doctorID { get; set; }
        public string doctorFirstName { get; set; }
        public string doctorLastName { get; set; }
        public int patientCount { get; set; }
        public List<PrescriptionRecord> Prescriptions { get; set; }
        public List<AppointmentRecord> Appointments { get; set; }
    }
    public class MedicineRecord
    {
        [Key]
        public int medicineID { get; set; }
        public BrandRecord brand { get; set; }
        public PrescriptionRecord prescription { get; set; }
        public string medicineName { get; set; }
        public int price { get; set; }
        public Boolean isRegulated { get; set; }
    }

    public class BrandRecord
    {
        [Key]
        public int brandId { get; set; }
        public string brandName { get; set; }
        public List<MedicineRecord> medicines { get; set; }

    }

   
}