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
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    
    public partial class PacientHasIllnesHistory
    {
        public int Id { get; set; }
        public int idPacient { get; set; }
        public int idIllenssHistory { get; set; }
        public System.DateTime VisitDate { get; set; }
        public virtual IllnessHistory IllnessHistory { get; set; }
        [JsonIgnore]
        public virtual Pacient Pacient { get; set; }
    }
}
