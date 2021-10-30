using OtiumActio.Domain.Categories;
using OtiumActio.Domain.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required(AllowEmptyStrings = false, ErrorMessage = "Beskriv kort aktiviteten, max 50 karaktär")]
        public string AcDescription { get; set; }
        [Range(1, 50, ErrorMessage = "Ange ett tal mellan 1 och 50")]
        public byte? AcParticipants { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy H:mm:ss zzz}", ApplyFormatInEditMode = true)]
        public DateTime? AcDate { get; set; }
        public int? AcCategoryId { get; set; }
        public DateTime? AcCreated { get; set; }
        public DateTime? AcModified { get; set; }

        public virtual Category AcCategory { get; set; }
        public virtual ICollection<Participant> Participants { get; set; }

    }
}
