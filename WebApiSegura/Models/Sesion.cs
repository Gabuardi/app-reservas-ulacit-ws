//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApiSegura.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sesion
    {
        public int SES_CODIGO { get; set; }
        public int USU_CODIGO { get; set; }
        public System.DateTime SES_FEC_HORA_INICIO { get; set; }
        public System.DateTime SES_FEC_HORA_FIN { get; set; }
        public string SES_ESTADO { get; set; }
    
        public virtual Usuario Usuario { get; set; }
    }
}
