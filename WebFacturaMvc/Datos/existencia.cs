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
    
    public partial class existencia
    {
        public string idProducto { get; set; }
        public int idSucursal { get; set; }
        public int cantidad { get; set; }
        public string seccion { get; set; }
    
        public virtual producto producto { get; set; }
        public virtual sucursal sucursal { get; set; }
    }
}
