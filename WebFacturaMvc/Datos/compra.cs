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
    
    public partial class compra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public compra()
        {
            this.compraDetalle = new HashSet<compraDetalle>();
        }
    
        public int idCompra { get; set; }
        public decimal total { get; set; }
        public System.DateTime fechaCompra { get; set; }
        public Nullable<int> idSucursal { get; set; }
        public Nullable<int> idProveedor { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<compraDetalle> compraDetalle { get; set; }
        public virtual sucursal sucursal { get; set; }
        public virtual proveedor proveedor { get; set; }
    }
}