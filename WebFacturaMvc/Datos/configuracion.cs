//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebFacturaMvc.Datos
{
    using System;
    using System.Collections.Generic;
    
    public partial class configuracion
    {
        public int idConfiguracion { get; set; }
        public string moneda { get; set; }
        public string requerir { get; set; }
        public Nullable<decimal> impuesto { get; set; }
        public string usuario { get; set; }
        public string servidorSmtp { get; set; }
        public int puerto { get; set; }
        public string contrasena { get; set; }
        public string displayName { get; set; }
        public string email { get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }
        public string celular { get; set; }
        public byte[] imagen { get; set; }
        public string puesto { get; set; }
        public string paginaUrl { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual Moneda Moneda1 { get; set; }
    }
}
