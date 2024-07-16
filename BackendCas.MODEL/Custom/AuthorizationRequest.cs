using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendCas.MODEL.Custom
{
    public class AuthorizationRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Clave { get; set; }
    }
}
