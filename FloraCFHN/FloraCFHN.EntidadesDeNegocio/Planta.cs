using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FloraCFHN.EntidadesDeNegocio
{
    public class Planta
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "El campo de nombre es requerido")]
        [MaxLength(100, ErrorMessage = "El largo máximo es de 100 caracteres")]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "El campo de descripción es requerido")]
        //[MaxLength(500, ErrorMessage = "El lago máximo es de 500 carateres")]
        public string Descripcion { get; set; }


        [Required(ErrorMessage = "El campo de imagen es requeridO")]
        public string ImagenUrl { get; set; }

        [Required(ErrorMessage = "El id de la categoria es requerida")]
        [ForeignKey("Categoria")]
        [Display(Name = "Id de la categoria")]
        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; }


        [NotMapped]
        public int top_aux { get; set; }
    }
}
