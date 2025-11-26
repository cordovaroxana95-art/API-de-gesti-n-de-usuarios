// DTOs/UserCreateDto.cs
using System.ComponentModel.DataAnnotations;

namespace UserManagementApi.DTOs
{
    public class UserCreateDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El email debe tener un formato válido.")]
        public string Email { get; set; }

        [Range(18, 150, ErrorMessage = "La edad debe ser un número entre 18 y 150.")]
        public int Age { get; set; }
    }
}