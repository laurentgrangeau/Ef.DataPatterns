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
    public partial class DemandeImplementation
    {
        public int Id { get; set; }
        public int RessourceId { get; set; }
        public int StId { get; set; }
        public int RefEtatDemandeId { get; set; }
        public int RefNatureDemandeId { get; set; }
        public string RessourceTmp { get; set; }
        public System.DateTime DateCreation { get; set; }
    
        public virtual Ressource Ressource { get; set; }
    }
    
}
