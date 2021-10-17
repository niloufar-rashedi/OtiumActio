using OtiumActio.Domain.Categories;
using OtiumActio.Domain.Users;
using System;
using System.Collections.Generic;

#nullable disable

namespace OtiumActio.Domain.Activities
{
    public partial class Activity
    {
        public Activity()
        {
            Participants = new HashSet<Participant>();
        }

        public int AcId { get; set; }
        public string AcDescription { get; set; }
        public byte? AcParticipants { get; set; }
        public DateTime? AcDate { get; set; }
        public int? AcCategoryId { get; set; }
        public DateTime? AcCreated { get; set; }
        public DateTime? AcModified { get; set; }

        public virtual Category AcCategory { get; set; }
        public virtual ICollection<Participant> Participants { get; set; }
    }
}
