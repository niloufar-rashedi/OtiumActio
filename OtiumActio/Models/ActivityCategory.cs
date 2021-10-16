using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OtiumActio.Models
{
    public class ActivityCategory
    {
        public int Id { get; set; }
        public Category CategoryId { get; set; }
        public ICollection<Activity> Activities { get; set; }

    }
}
