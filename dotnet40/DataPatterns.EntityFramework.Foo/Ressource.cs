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
    public partial class Ressource
    {
        public Ressource()
        {
            this.DemandeImplementation = new HashSet<DemandeImplementation>();
        }
    
        public int Id { get; set; }
        public int RefPlanNumerotationId { get; set; }
        public System.DateTime DateCreation { get; set; }
        public System.DateTime DateMaj { get; set; }
        public string Numero { get; set; }
        public int RefEtatRessourceId { get; set; }
        public int RefModaliteId { get; set; }
        public System.DateTime DateEtat { get; set; }
    
        public virtual RefPlanNumerotation RefPlanNumerotation { get; set; }
        public virtual RefEtatRessource RefEtatRessource { get; set; }
        public virtual ICollection<DemandeImplementation> DemandeImplementation { get; set; }
    }
    
}