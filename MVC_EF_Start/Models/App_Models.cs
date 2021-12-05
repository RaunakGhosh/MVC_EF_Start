using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace MVC_EF_Start.Models
{
    public class App_Models
    {
        public List<Exam> Data { get; set; }
    }
    
    public class Exam
    {
        
        [Key]
        public int examID { get; set; }
        public string mean_scale_score { get; set; }
        public string number_tested { get; set; }
        public string level_1_1 { get; set; }
        public string level_1_2 { get; set; }
        public string level_2_1 { get; set; }
        public string level_2_2 { get; set; }
        public string level_3_1 { get; set; }
        public string level_3_2 { get; set; }
        public string level_4_1 { get; set; }
        public string level_4_2 { get; set; }
        public string level_3_4_1 { get; set; }
        public string level_3_4_2 { get; set; }
        [Required]
        public Category cat_id { get; set; }
        
    }
    public class Participants
    {
        [Key]
        public int participantId { get; set; }
        public string grade { get; set; }
        public string year { get; set; }
        [Required]
        public Category cat_id { get; set; }
    }

    public class Category
    {
        [Key]
        public int categoryId { get; set; }
        public string category { get; set; }

    }

    public class ChartModel
    {
        public string ChartType { get; set; }
        public string Labels { get; set; }

        public string Colors { get; set; }
        public string Data { get; set; }
        public string Title { get; set; }
    }




}
