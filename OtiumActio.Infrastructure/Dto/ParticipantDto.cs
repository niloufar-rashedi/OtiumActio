using OtiumActio.Domain.Activities;
using OtiumActio.Domain.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace OtiumActio.Infrastructure.Dto
{
    public class ParticipantDto
    {
        public int PrtcId { get; set; }
        public int? PrtcActivityId { get; set; }
        public string PrtcFirstName { get; set; }
        public string PrtcLastName { get; set; }
        public int? PrtcAge { get; set; }
        public int? PrtcFavouritCategory { get; set; }
        public string PrtcUserName { get; set; }
        public string PrtcPassword { get; set; }
        public string PrtcConfirmPassword { get; set; }

        public virtual Activity PrtcActivity { get; set; }
        public virtual Category PrtcFavouritCategoryNavigation { get; set; }

    }
}
