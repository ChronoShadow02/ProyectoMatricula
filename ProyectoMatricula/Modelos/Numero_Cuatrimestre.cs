//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoMatricula.Modelos
{
    using System;
    using System.Collections.Generic;
    
    public partial class Numero_Cuatrimestre
    {
        public Numero_Cuatrimestre()
        {
            this.Cuatrimestre = new HashSet<Cuatrimestre>();
            this.Curso_x_Cuatrimestre = new HashSet<Curso_x_Cuatrimestre>();
            this.Curso_x_Sede = new HashSet<Curso_x_Sede>();
        }
    
        public int Id_Num_Cuatrimestre { get; set; }
        public int Numero_Cuatrimestre1 { get; set; }
    
        public virtual ICollection<Cuatrimestre> Cuatrimestre { get; set; }
        public virtual ICollection<Curso_x_Cuatrimestre> Curso_x_Cuatrimestre { get; set; }
        public virtual ICollection<Curso_x_Sede> Curso_x_Sede { get; set; }
    }
}