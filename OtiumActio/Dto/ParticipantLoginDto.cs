using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OtiumActio.Dto
{
    public class ParticipantLoginDto
    {
        public int PrtcId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ange din e-post")]
        public string PrtcUserName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ange ditt lösenord")]
        [DataType(DataType.Password)]
        public string PrtcPassword { get; set; }
    }
}
