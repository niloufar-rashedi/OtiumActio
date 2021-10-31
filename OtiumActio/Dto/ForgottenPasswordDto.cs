using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OtiumActio.Dto
{
    public class ForgottenPasswordDto
    {
            public int PrtcId { get; set; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "Ange din e-post")]
            [EmailAddress]
            public string PrtcUserName { get; set; }

    }
}
