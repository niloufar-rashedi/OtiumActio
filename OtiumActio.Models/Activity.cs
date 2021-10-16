using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OtiumActio.Models
{
    public class Activity
    {
        [Key]
        public int Id { get; set; }
        public int Category { get; set; }
        public string CategoryName { get; set; }
        public Category Categories { get; set; }
        public string Description { get; set; }
        public long Participants { get; set; }
        public DateTime Date { get; set; }
        public DateTime Created { get; set; }


    }
}
