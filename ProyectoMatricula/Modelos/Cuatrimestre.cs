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
    
    public partial class Cuatrimestre
    {
        public Cuatrimestre()
        {
            this.Cuatrimestre_x_Sede = new HashSet<Cuatrimestre_x_Sede>();
            this.Curso_x_Cuatrimestre = new HashSet<Curso_x_Cuatrimestre>();
        }
    
        public int Num_Cuatrimestre { get; set; }
        public int Año_Cuatrimestre { get; set; }
        public int Id_Cuatrimestre { get; set; }
        public string inicia_Cuatrimestre { get; set; }
        public string fin_Cuatrimestre { get; set; }
    
        public virtual ICollection<Cuatrimestre_x_Sede> Cuatrimestre_x_Sede { get; set; }
        public virtual ICollection<Curso_x_Cuatrimestre> Curso_x_Cuatrimestre { get; set; }
    }
}
