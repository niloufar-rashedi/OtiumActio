using OtiumActio.Domain.Activities;
using OtiumActio.Domain.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OtiumActio.Dto
{
    public class ParticipantDto
    {
        public int PrtcId { get; set; }
        public int? PrtcActivityId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ange ditt förnamn")]
        [StringLength(50, ErrorMessage = "Namnet är för kort/långt")]
        public string PrtcFirstName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ange ditt familjnamn")]
        [StringLength(50, ErrorMessage = "familjnamn är för kort/långt")]
        public string PrtcLastName { get; set; }
        public int? PrtcAge { get; set; }
        public int? PrtcFavouritCategory { get; set; }
        public string PrtcUserName { get; set; }
        [Required]
        public string PrtcPassword { get; set; }
        [Compare("PrtcPassword", ErrorMessage = "Lösenord matchar ej!")]
        public string PrtcConfirmPassword { get; set; }

        public virtual Activity PrtcActivity { get; set; }
        public virtual Category PrtcFavouritCategoryNavigation { get; set; }

    }
}
