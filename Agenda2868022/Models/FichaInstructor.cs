using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Agenda2868022.Models
{
    public class FichaInstructor
    {
        [Key]
        public int FichaInstructorId { get; set; }

        //Relacion con ficha
        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        public int FichaId { get; set; }
        public virtual Ficha Ficha { get; set; }

        //Relacion con instructor

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int InstructorId { get; set; }
        public virtual Instructor Instructor { get; set; }
    }
}