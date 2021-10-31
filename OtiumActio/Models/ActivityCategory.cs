using OtiumActio.Domain.Activities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace OtiumActio.Models
{
    public partial class ActivityCategory
    {
        [Key]
        public int AcatCategoryId { get; set; }
        public int? AcatActivityId { get; set; }
        public DateTime? AcatCreated { get; set; }

        public virtual Activity AcatActivity { get; set; }
        public virtual Category AcatCategory { get; set; }
    }
}
