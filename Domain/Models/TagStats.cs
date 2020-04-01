using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TagStats
    {  
        public int ID { get; set; }
        public string Tag { get; set; }
        public double WeekRate { get; set; }
        public double DayRate { get; set; }
        public double AllTimeRate { get; set; }
    }
}

