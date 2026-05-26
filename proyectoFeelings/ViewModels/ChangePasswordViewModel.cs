using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace proyectoFeelings.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "El campo Email es obligatorio.")]
        [EmailAddress]

        public string Email { get; set; }

        [Required(ErrorMessage = "El campo Contraseña actual es obligatorio.")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 40 caracteres.")]
        [DataType(DataType.Password)] // Indica que una propiedad debe tratarse como una contraseña.
        [Display(Name = "Nueva Contraseña ")]
        [Compare("ConfirmedNewPassword", ErrorMessage = "Las contraseñas no coinciden")]


        public string NewPassword { get; set; }

        [Required(ErrorMessage = "El campo Confirmar Contraseña es obligatorio.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Nueva Contraseña")]

        public string ConfirmedNewPassword { get; set; }

    }
}
