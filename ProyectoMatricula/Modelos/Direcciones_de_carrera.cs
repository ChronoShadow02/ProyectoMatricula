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
    
    public partial class Direcciones_de_carrera
    {
        public Direcciones_de_carrera()
        {
            this.Carreras_universitarias = new HashSet<Carreras_universitarias>();
        }
    
        public int Id_Direccion_Carrera { get; set; }
        public string Nombre_Direccion_Carrera { get; set; }
        public string Codigo_Direccion_Carrera { get; set; }
        public int Id_Director { get; set; }
        public int Id_Subdirector { get; set; }
    
        public virtual ICollection<Carreras_universitarias> Carreras_universitarias { get; set; }
        public virtual Funcionarios Funcionarios { get; set; }
        public virtual Funcionarios Funcionarios1 { get; set; }
    }
}
