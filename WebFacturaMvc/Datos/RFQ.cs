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
    
    public partial class RFQ
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RFQ()
        {
            this.RFQItem = new HashSet<RFQItem>();
        }
    
        public int idRFQ { get; set; }
        public string idVendedor { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public string estatus { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RFQItem> RFQItem { get; set; }
    }
}
