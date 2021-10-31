using OtiumActio.Models;
using OtiumActio.Domain.Categories;
using System;
using System.Collections.Generic;

#nullable disable

namespace OtiumActio.Models
{
    public partial class Participant
    {
        public int PrtcId { get; set; }
        public int? PrtcActivityId { get; set; }
        public string PrtcFirstName { get; set; }
        public string PrtcLastName { get; set; }
        public int? PrtcAge { get; set; }
        public int? PrtcFavouritCategory { get; set; }
        public string PrtcUserName { get; set; }
        public byte[] PrtcPasswordHash { get; set; }
        public byte[] PrtcPasswordSalt { get; set; }
        public DateTime? PrtcCreated { get; set; }
        public DateTime? PrtcModified { get; set; }
        public virtual Activity PrtcActivity { get; set; }
        public virtual Category PrtcFavouritCategoryNavigation { get; set; }
    }
}
