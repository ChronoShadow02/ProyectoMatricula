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
            this.Curso_x_Cuatrimestre = new HashSet<Curso_x_Cuatrimestre>();
        }
    
        public int Id_Cuatrimeste { get; set; }
        public int Id_Num_Cuatrimestre { get; set; }
        public int Anio_Cuatrimestre { get; set; }
        public int Id_Sede_Universitarias { get; set; }
        public string Inicio_Cuatrimestre { get; set; }
        public string Fin_Cuatrimestre { get; set; }
    
        public virtual Numero_Cuatrimestre Numero_Cuatrimestre { get; set; }
        public virtual Sedes_Universitarias Sedes_Universitarias { get; set; }
        public virtual ICollection<Curso_x_Cuatrimestre> Curso_x_Cuatrimestre { get; set; }
    }
}
