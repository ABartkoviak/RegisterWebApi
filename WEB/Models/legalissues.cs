//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WEB.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class legalissues
    {
        public int idLegalIssues { get; set; }
        public string LegalIssueDescription { get; set; }
        public int Experts_idExperts { get; set; }
    
        public virtual experts experts { get; set; }
    }
}
