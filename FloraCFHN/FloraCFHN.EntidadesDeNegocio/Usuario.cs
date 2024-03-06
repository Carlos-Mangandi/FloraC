using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FloraCFHN.EntidadesDeNegocio
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(100, ErrorMessage = "El largo maximo es de 100 caracteres")]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "El Apellido es requerido")]
        [MaxLength(50, ErrorMessage = "El largo máximo es de 50 caracteres")]
        public string Apellido { get; set; }


        [Required(ErrorMessage = "El teléfono es requerido")]
        [MaxLength(9, ErrorMessage = "El largo máximo es de 9 caracteres")]
        public string Telefono { get; set; }


        [Required(ErrorMessage = "El login es requerido")]
        [MaxLength(100, ErrorMessage = "El largo máximo es de 100 caracteres")]
        public string Login { get; set; }


        [Required(ErrorMessage = "El password es obligatorio")]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "Password debe estar entre 6 a 50 caracteres", MinimumLength = 6)]
        public string Password { get; set; }


        [Required(ErrorMessage = "el estatus es requerido")]
        public byte Estatus { get; set; }


        [Display(Name = "Fecha registro")]
        public DateTime FechaRegistro { get; set; }


        [Required(ErrorMessage = "El Id de rol es requerido")]
        [ForeignKey("Rol")]
        [Display(Name = "Id del rol")]
        public int RolId { get; set; }


        public Rol Rol { get; set; } //propiedad de navegacion
                                               // tipo de dato | tipo de propiedad


        [NotMapped]
        public int top_aux { get; set; }  //propiedad auxiliar para traer un numero en especifico de registros
                                          // en las consultas

        [NotMapped]
        [Required(ErrorMessage = "Password la contrasena")]
        [StringLength(50, ErrorMessage = "Password debe estar entre 6 a 50 caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password y confirmar Password deben de ser iguales")]
        [Display(Name = "Confirmar contraseña")]
        public string confirmPassword_aux { get; set; }

        public enum EnumRol
        {
            ADMINISTRADOR = 1,
            CLIENTE = 2,
        }
    }
    public enum Estatus_Usuario
    {
        ACTIVO = 1,
        INACTIVO = 2
    }
}


