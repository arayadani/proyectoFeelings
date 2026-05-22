using System.ComponentModel.DataAnnotations;

namespace proyectoFeelings.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El correo es requerido.")]
        [EmailAddress]

        public string Email { get; set; }


        [Required(ErrorMessage = "La contraseña es requerida.")] //esto es parte de la vista  
        [DataType(DataType.Password)] 

        public string Password { get; set; } //esto es parte del modelo


        [Display(Name = "Recordar contraseña")] 

        public bool RememberMe { get; set; } //no necesario
    }
}
