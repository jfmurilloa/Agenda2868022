using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;

namespace Agenda2868022.Models
{
    public class Ficha
    {
        [Key]
        public int FichaId { get; set; }

        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        [StringLength(50,MinimumLength =6,
            ErrorMessage ="El campo {0} debe tener entre {2} y {1} caracteres")]
        public string Programa { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(8, MinimumLength = 4,
            ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        [Index("IndexCodigo", IsUnique =true)]
        
        public string Codigo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime FechaInicio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaFin { get; set; }

        //relacion con aprendiz
        public virtual ICollection<Aprendiz> Aprendizs { get; set; }
    }
}