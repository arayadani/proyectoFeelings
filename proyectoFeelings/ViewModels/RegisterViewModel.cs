using System.ComponentModel.DataAnnotations;

namespace proyectoFeelings.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "El nombre es requerido.")]

        public string Name { get; set; }
        [Required(ErrorMessage = "El correo electrónico es requerido.")]
        [EmailAddress]

        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida.")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 40 caracteres.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "Las contraseñas no coinciden.")]

        public string Password { get; set; }
        [Required(ErrorMessage = "La confirmación de contraseña es requerida.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        public string ConfirmPassword { get; set; }
    }
}
