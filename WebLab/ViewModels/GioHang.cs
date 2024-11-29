using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLab.Models;

namespace WebLab.ViewModels
{
    public class GioHang
    {
        QLBanXeGanMayEntities db = new QLBanXeGanMayEntities();
        public int MaXe { get; set; }

        public string TenXe { get; set; }
        public string Anhbia { get; set; }
        public double Dongia { get; set; }
        public int soLuong {  get; set; }
        public double ThanhTien
        {
            get { return soLuong * Dongia; }
        }

        public GioHang(int _MaXe)
        {
            MaXe = _MaXe;
            XEGANMAY xe = db.XEGANMAY.FirstOrDefault(n => n.MaXe == _MaXe);
            TenXe = xe.TenXe;
            Anhbia = xe.Anhbia;
            Dongia = double.Parse(xe.Giaban.ToString());
            soLuong = 1;
        }
    }
}