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
    
    public partial class CursosXEstudiante
    {
        public int Id_CursosXEstudiantes { get; set; }
        public int Id_Estudiante { get; set; }
        public int Id_Curso { get; set; }
        public double Nota { get; set; }
        public string Estado_Nota { get; set; }
        public string Finaliza_Curso { get; set; }
    
        public virtual Cursos Cursos { get; set; }
        public virtual Estudiantes Estudiantes { get; set; }
    }
}
