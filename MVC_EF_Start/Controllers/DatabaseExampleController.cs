using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_EF_Start.DataAccess;
using MVC_EF_Start.Models;

namespace MVC_EF_Start.Controllers
{
    public class DatabaseExampleController : Controller
    {
        public ApplicationDbContext dbContext;

        public DatabaseExampleController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ViewResult> DatabaseOperations()
        {
            // CREATE operation
            PatientsRecord patient1 = new PatientsRecord();
            //  patient1.patientID = 101;
            patient1.firstName = "Tim";
            patient1.lastName = "Howard";
            patient1.age = 40;
            patient1.email = "timhoward@gmail.com";
            patient1.mobile = 987654321;
            dbContext.Patients.Add(patient1);

            PatientsRecord patient2 = new PatientsRecord();
            // patient2.patientID = 102;
            patient2.firstName = "Eric";
            patient2.lastName = "Dyer";
            patient2.age = 30;
            patient2.email = "ed@gmail.com";
            patient2.mobile = 456123789;
            dbContext.Patients.Add(patient2);

            PatientsRecord patient3 = new PatientsRecord();
            // patient3.patientID = 103;
            patient3.firstName = "Sid";
            patient3.lastName = "Malone";
            patient3.age = 34;
            patient3.email = "sidmalone@gmail.com";
            patient3.mobile = 654321987;
            dbContext.Patients.Add(patient3);

            PatientsRecord patient4 = new PatientsRecord();
            //  patient4.patientID = 104;
            patient4.firstName = "Trevor";
            patient4.lastName = "Baker";
            patient4.age = 31;
            patient4.email = "TrevBaker@gmail.com";
            patient4.mobile = 123314;
            dbContext.Patients.Add(patient4);

            doctorVisit doc1 = new doctorVisit();
            //  doc1.doctorID = 101;
            doc1.doctorFirstName = "john";
            doc1.doctorLastName = "mallon";
            doc1.patientCount = 130;
            dbContext.Doctors.Add(doc1);

            doctorVisit doc2 = new doctorVisit();
            //  doc2.doctorID = 102;
            doc2.doctorFirstName = "Steven";
            doc2.doctorLastName = "Strange";
            doc2.patientCount = 300;
            dbContext.Doctors.Add(doc2);

            doctorVisit doc3 = new doctorVisit();
            //  doc3.doctorID = 103;
            doc3.doctorFirstName = "Raymond";
            doc3.doctorLastName = "James";
            doc3.patientCount = 100;
            dbContext.Doctors.Add(doc3);

            doctorVisit doc4 = new doctorVisit();
            //   doc4.doctorID = 104;
            doc4.doctorFirstName = "Quinton";
            doc4.doctorLastName = "Tarantino";
            doc4.patientCount = 20;
            dbContext.Doctors.Add(doc4);

            doctorVisit doc5 = new doctorVisit();
            //  doc5.doctorID = 105;
            doc5.doctorFirstName = "Bert";
            doc5.doctorLastName = "Reynolds";
            doc5.patientCount = 450;
            dbContext.Doctors.Add(doc5);

            doctorVisit doc6 = new doctorVisit();
            //   doc6.doctorID = 106;
            doc6.doctorFirstName = "Hannibal";
            doc6.doctorLastName = "Lector";
            doc6.patientCount = 600;
            dbContext.Doctors.Add(doc6);

            BrandRecord brand1 = new BrandRecord();
            //brand1.brandId = 301;
            brand1.brandName = "Pfizer";
            dbContext.Brands.Add(brand1);

            BrandRecord brand2 = new BrandRecord();
            //brand2.brandId = 302;
            brand2.brandName = "Moderna";
            dbContext.Brands.Add(brand2);

            BrandRecord brand3 = new BrandRecord();
            //b//rand3.brandId = 303;
            brand3.brandName = "ellipsis";
            dbContext.Brands.Add(brand3);

            AppointmentRecord appoint1 = new AppointmentRecord();
            // appoint1.appointmentID = 501;
            appoint1.patient = patient1;
            appoint1.doc = doc1;
            appoint1.dateVisited = new DateTime(2015, 12, 25);
            dbContext.Appointments.Add(appoint1);

            AppointmentRecord appoint2 = new AppointmentRecord();
            //    appoint2.appointmentID = 502;
            appoint2.patient = patient2;
            appoint2.doc = doc2;
            appoint2.dateVisited = new DateTime(2016, 10, 10);
            dbContext.Appointments.Add(appoint2);

            AppointmentRecord appoint3 = new AppointmentRecord();
            //   appoint3.appointmentID = 503;
            appoint3.patient = patient3;
            appoint3.doc = doc3;
            appoint3.dateVisited = new DateTime(2015, 07, 1);
            dbContext.Appointments.Add(appoint3);

            MedicineRecord med2 = new MedicineRecord();
            //   med2.medicineID = 701;
            med2.brand = brand1;
            med2.medicineName = "xanax";
            med2.price = 1000;
            med2.isRegulated = true;
            dbContext.Medicines.Add(med2);

            MedicineRecord med1 = new MedicineRecord();
            //  med1.medicineID = 702;
            med1.brand = brand2;
            med1.medicineName = "medzone";
            med1.price = 900;
            med1.isRegulated = true;
            dbContext.Medicines.Add(med1);


            MedicineRecord med3 = new MedicineRecord();
            //  med3.medicineID = 703;
            med3.brand = brand2;
            med3.medicineName = "profolac";
            med3.price = 600;
            med3.isRegulated = false;
            dbContext.Medicines.Add(med3);

            PrescriptionRecord prescription1 = new PrescriptionRecord();
            //  prescription1.prescriptionID = 999;
            prescription1.patient = patient1;
            prescription1.doc = doc1;
            prescription1.appoint = appoint1;
            dbContext.Prescriptions.Add(prescription1);


            PrescriptionRecord prescription2 = new PrescriptionRecord();
            //   prescription2.prescriptionID = 997;
            prescription2.patient = patient1;
            prescription2.doc = doc1;
            prescription2.appoint = appoint1;
            dbContext.Prescriptions.Add(prescription2);


            PrescriptionRecord prescription3 = new PrescriptionRecord();
            //    prescription3.prescriptionID = 199;
            prescription3.patient = patient1;
            prescription3.doc = doc1;
            prescription3.appoint = appoint1;
            dbContext.Prescriptions.Add(prescription3);



            await dbContext.SaveChangesAsync();

            // READ operation
            //Company CompanyRead1 = dbContext.Companies
            //                        .Where(c => c.Id == "MCOB")
            //                        .First();

            //Company CompanyRead2 = dbContext.Companies
            //                        .Include(c => c.Quotes)
            //                        .Where(c => c.Id == "MCOB")
            //                        .First();

            //// UPDATE operation
            //CompanyRead1.iexId = "MCOB";
            //dbContext.Companies.Update(CompanyRead1);
            ////dbContext.SaveChanges();
            //await dbContext.SaveChangesAsync();

            //// DELETE operation
            //dbContext.Companies.Remove(CompanyRead1);
            //await dbContext.SaveChangesAsync();

            return View();
        }

        public ViewResult LINQOperations()
        {



//query1
            var query = from patient in dbContext.Patients
                             select new { patient.firstName, patient.lastName };
                return View(query);


            public ViewResult Q2(string doctorName, DateTime visitDate)
            {
                int Did = dbContext.Doctors.SingleOrDefault(x => x.doctorFirstName == "John" && x.doctorLastName == "Smith").doctorID;

                List<int> patient_ids = dbContext.Appointments.Where(x => x.dateVisited == DateTime.Parse("10/22/2021") && x.doc.doctorID == Did)
                    .Select(x => x.patient.patientID).ToList();

                var patients = dbContext.Patients.Where(x => patient_ids.Contains(x.patientID)).Select(x => new { x.firstName, x.lastName });

                foreach (var x in patients)
                {
                    Debug.WriteLine(x.ToString());

                }

                return View(patients);
            }

            public ViewResult Ques5()
            {

                var doctors = dbContext.Appointments.Include(c => c.doc).Include(c => c.patient)
                    .AsEnumerable()
                    .GroupBy(c => c.doc.doctorID)
                    .OrderByDescending(c => c.Count())
                    .ToList().Take(5);

                foreach (var x in doctors)
                {
                    Debug.WriteLine("Doctor Details :" + x.Select(a => new { a.doc.doctorFirstName, a.doc.doctorLastName }).First().ToString() +
                        "'s, Patient Count is : " + x.Count()
                    );

                }

                Debug.WriteLine(doctors.ToString());

                return View(doctors);


            }

            public ViewResult Ques6()
            {
                int med_id = dbContext.Medicines.SingleOrDefault(x => x.medicineName == "xanax").medicineID;

                List<int> doctor_ids = dbContext.Prescriptions.Where(
                    x => x.Medicines.medid == med_id)
                    .Select(x => x.Doctor.DoctorId).ToList();

                var doctors = dbContext.Doctors.Where(x => doctor_ids.Contains(x.doctorID)).Select(x => new { x.doctorFirstName, x.doctorLastName });

                foreach (var x in doctors)
                {
                    Debug.WriteLine(x.ToString());


                }

                return View(doctors);

            }





        }

}
}
