using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dto
{
    public class LoginDto
    {   

        [Required(ErrorMessage = "El usuario es requerido")]
        [StringLength(50, ErrorMessage = "El usuario no puede exceder los 50 caracteres")]
        [RegularExpression("^[a-zA-Z0-9._@]*$", ErrorMessage = "El usuario solo puede contener letras y números")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(50, ErrorMessage = "La contraseña no puede exceder los 50 caracteres")]
        [RegularExpression("^[a-zA-Z0-9.*$%?_!@#]*$", ErrorMessage = "La contraseña solo puede contener letras y números")]

        public string Password { get; set; } = string.Empty;
    }
}