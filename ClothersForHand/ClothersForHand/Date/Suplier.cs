//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClothersForHand.Date
{
    using System;
    using System.Collections.Generic;
    
    public partial class Suplier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Suplier()
        {
            this.PossibleSupliersMaterial = new HashSet<PossibleSupliersMaterial>();
        }
    
        public int SuplierID { get; set; }
        public string SuplierName { get; set; }
        public int SuplierTypeID { get; set; }
        public long ITN { get; set; }
        public int QualityRating { get; set; }
        public System.DateTime DateStartWorkWithSuplier { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PossibleSupliersMaterial> PossibleSupliersMaterial { get; set; }
        public virtual SuplierType SuplierType { get; set; }
    }
}
