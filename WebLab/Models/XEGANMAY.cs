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
    
    public partial class XEGANMAY
    {
        public XEGANMAY()
        {
            this.CHITIETDONTHANG = new HashSet<CHITIETDONTHANG>();
            this.SANXUATXE = new HashSet<SANXUATXE>();
        }
    
        public int MaXe { get; set; }
        public string TenXe { get; set; }
        public Nullable<decimal> Giaban { get; set; }
        public string Mota { get; set; }
        public string Anhbia { get; set; }
        public Nullable<System.DateTime> Ngaycapnhat { get; set; }
        public Nullable<int> Soluongton { get; set; }
        public Nullable<int> MaLX { get; set; }
        public Nullable<int> MaNPP { get; set; }
    
        public virtual ICollection<CHITIETDONTHANG> CHITIETDONTHANG { get; set; }
        public virtual LOAIXE LOAIXE { get; set; }
        public virtual NHAPHANPHOI NHAPHANPHOI { get; set; }
        public virtual ICollection<SANXUATXE> SANXUATXE { get; set; }
    }
}
