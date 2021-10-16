using OtiumActio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OtiumActio.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public int Category { get; set; }
        public Category Categories { get; set; }
        public string Description { get; set; }
        public long Participants { get; set; }
        public DateTime Date { get; set; }
    }
}
