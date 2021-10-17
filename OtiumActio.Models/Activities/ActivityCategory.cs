using OtiumActio.Domain.Activities;
using System;
using System.Collections.Generic;

#nullable disable

namespace OtiumActio.Domain.Categories
{
    public partial class ActivityCategory
    {
        public int AcatCategoryId { get; set; }
        public int? AcatActivityId { get; set; }
        public DateTime? AcatCreated { get; set; }

        public virtual Activity AcatActivity { get; set; }
        public virtual Category AcatCategory { get; set; }
    }
}
