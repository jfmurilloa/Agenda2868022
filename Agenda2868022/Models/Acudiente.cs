﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Agenda2868022.Models
{
    public class Acudiente
    {
        [Key]
        [Display(Name ="Id")]
        public int AcudienteId { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(20, MinimumLength = 5,
            ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        [Index("IndexDocumento", IsUnique = true)]
        public string Documento { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(20, MinimumLength = 3,
            ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(30, MinimumLength = 3,
            ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        public string Apellidos { get; set; }
        

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(10, MinimumLength = 10,
            ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        [Range(3000000000, 3999999999,
            ErrorMessage = "El campo {0} debe estar entre {1} y {2}")]
        [Index("IndexCelular", IsUnique = true)]
        public string Celular { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(50, MinimumLength = 10,
           ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        [DataType(DataType.EmailAddress)]
        [Index("IndexCorreo", IsUnique = true)]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(30, MinimumLength = 5,
            ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        public string Direccion { get; set; }

        //Atributo de solo lectura
        public string FullName { get { return this.Nombres + " " + this.Apellidos; }  }

        //Relacion con Aprendiz
        public virtual ICollection<Aprendiz> Aprendizs { get; set; }

       
    }
}