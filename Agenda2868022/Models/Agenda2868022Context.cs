using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Agenda2868022.Models
{
    public class Agenda2868022Context:DbContext
    {
        //Metodo constructor
        public Agenda2868022Context() :base("DefaultConnection")
        { 
            //cuerpo del constructor
        }

        //Atributos
        public DbSet<Ficha> Fichas { get; set; }
        public DbSet<Aprendiz> Aprendizes { get; set; }
        public DbSet<Instructor> Instructors { get; set; }

    }
}