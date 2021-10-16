using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OtiumActio.Models
{
    public class Participant
    {
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Category FavouritCategory { get; set; }
        //public ICollection<Activity> Activities { get; set; }
    }
}
