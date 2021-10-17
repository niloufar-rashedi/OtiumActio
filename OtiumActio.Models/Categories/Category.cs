using OtiumActio.Domain.Activities;
using OtiumActio.Domain.Users;
using System;
using System.Collections.Generic;

#nullable disable

namespace OtiumActio.Domain.Categories
{
    public partial class Category
    {
        public Category()
        {
            Activities = new HashSet<Activity>();
            Participants = new HashSet<Participant>();
        }

        public int CatId { get; set; }
        public string CatName { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<Participant> Participants { get; set; }
    }
}
