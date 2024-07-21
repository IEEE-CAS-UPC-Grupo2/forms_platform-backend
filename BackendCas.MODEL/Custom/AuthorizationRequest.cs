using System.ComponentModel.DataAnnotations;

namespace BackendCas.MODEL.Custom;

public class AuthorizationRequest
{
    [Required] public string Email { get; set; }

    [Required] public string Password { get; set; }
}