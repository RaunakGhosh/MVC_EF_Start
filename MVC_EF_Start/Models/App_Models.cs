using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MVC_EF_Start.Models
{


        public class Rootobject
        {
            public Class1[] Property1 { get; set; }
        }

        public class Class1
        {
            public string grade { get; set; }
            public string year { get; set; }
            public string category { get; set; }
            public string number_tested { get; set; }
            public string mean_scale_score { get; set; }
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
        }



    }
