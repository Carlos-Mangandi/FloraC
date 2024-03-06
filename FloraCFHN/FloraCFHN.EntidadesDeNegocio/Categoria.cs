using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FloraCFHN.EntidadesDeNegocio
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(50, ErrorMessage = "El largo máximo es de 50 caracteres")]
        public string Nombre { get; set; }


        [NotMapped]
        public int top_aux { get; set; } //propíedad auxiliar para seleccionar cantidad de datos a consultar 

    }
}
