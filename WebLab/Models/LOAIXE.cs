//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebLab.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LOAIXE
    {
        public LOAIXE()
        {
            this.XEGANMAY = new HashSet<XEGANMAY>();
        }
    
        public int MaLX { get; set; }
        public string TenLoaiXe { get; set; }
    
        public virtual ICollection<XEGANMAY> XEGANMAY { get; set; }
    }
}
