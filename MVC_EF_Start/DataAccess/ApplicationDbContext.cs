using Microsoft.EntityFrameworkCore;
using MVC_EF_Start.Models;

namespace MVC_EF_Start.DataAccess
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        //public DbSet<doctorVisit> Doctors { get; set; }
        //public DbSet<PatientsRecord> Patients { get; set; }
        //public DbSet<AppointmentRecord> Appointments { get; set; }
        //public DbSet<PrescriptionRecord> Prescriptions { get; set; }
        //public DbSet<MedicineRecord> Medicines { get; set; }
        //public DbSet<BrandRecord> Brands { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Participants> Participants { get; set; }
        public DbSet<Category> Category { get; set; }


    }
}