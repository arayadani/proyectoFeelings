using System.ComponentModel.DataAnnotations;

namespace proyectoFeelings.ViewModels
{
    public class VerifyEmailViewModel
    {
        [Required(ErrorMessage = "El correo electrónico es requerido.")]
        [EmailAddress]  
        public string Email { get; set; }   
    }
}
