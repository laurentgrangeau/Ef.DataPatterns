//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace DataPatterns.EntityFramework.Foo
{
    public partial class RefPays
    {
        public RefPays()
        {
            this.Destination = new HashSet<Destination>();
        }
    
        public int Id { get; set; }
        public string Code { get; set; }
        public string Libelle { get; set; }
        public System.DateTime DateDebut { get; set; }
        public Nullable<System.DateTime> DateFin { get; set; }
    
        public virtual ICollection<Destination> Destination { get; set; }
    }
    
}
