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
    public partial class RessourceInt : Ressource
    {
        public int DestinationId { get; set; }
    
        public virtual Destination Destination { get; set; }
    }
    
}