//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Server
{
    using System;
    using System.Collections.Generic;
    
    public partial class IllnessHistoryHasMedicine
    {
        public int Id { get; set; }
        public int idIllnessHistory { get; set; }
        public int idMedicine { get; set; }
    
        public virtual IllnessHistory IllnessHistory { get; set; }
        public virtual Medicine Medicine { get; set; }
    }
}
