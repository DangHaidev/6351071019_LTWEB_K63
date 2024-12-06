using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLab.Models;
using WebLab.ViewModels;

namespace WebLab.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang

        QLBanXeGanMayEntities _context = new QLBanXeGanMayEntities();
        public ActionResult Index()
        {
            return View();
        }

        //lay gio hang

        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }

        public ActionResult AddToCart(int MaXe, string strURL)
        {
            // lay seeesssion gio hang

            List<GioHang> lstGioHang = LayGioHang();
            var sp = lstGioHang.Find(n => n.MaXe == MaXe);

            if (sp == null)
            {
                sp = new GioHang(MaXe);
                lstGioHang.Add(sp);
                return Redirect(strURL);
            }
            else
            {
                sp.soLuong++;
                return Redirect(strURL);
            }
        }

        private int TongSoLuong()
        {
            int Total = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                Total = lstGiohang.Sum(n => n.soLuong);
            }
            return Total;
        }

        private double TongTien()
        {
            double Total = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                Total = lstGioHang.Sum(p => p.ThanhTien);
            }
            return Total;
        }


        public ActionResult GioHang()
        {

            List<GioHang> lstGioHang = LayGioHang();
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "QuanLyXe");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }

        public ActionResult XoaSp(int id)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.SingleOrDefault(n => n.MaXe == id);

            if (sp != null)
            {
                lstGioHang.RemoveAll(n => n.MaXe == id);
                return RedirectToAction("GioHang");
            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "QuanLyXe");
            }
            return RedirectToAction("GioHang");

        }
        public ActionResult CapNhatGioHang(int MaXe, FormCollection f)
        {
            List<GioHang> lstGioHang = LayGioHang();

            GioHang sp = lstGioHang.SingleOrDefault(n => n.MaXe == MaXe);
            if (sp != null)
            {
                sp.soLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult RemoveCart()
        { 
            List<GioHang> lstGioHang = LayGioHang();
            lstGioHang.Clear();
            return RedirectToAction("Index", "QuanLyXe");
        }
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("SignIn", "Account");
            }

            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "QuanLyXe");
            }

            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }

        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            // Thêm Đơn hàng
            DONDATHANG ddh = new DONDATHANG();
            KHACHHANG kh = (KHACHHANG)Session["Taikhoan"];
            List<GioHang> gh = LayGioHang();
            ddh.MaKH = kh.MaKH;
            ddh.Ngaydat = DateTime.Now;
            var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["Ngaygiao"]);
            ddh.Ngaygiao = DateTime.Parse(ngaygiao);
            ddh.Tinhtranggiaohang = false;
            ddh.Dathanhtoan = false;
            _context.DONDATHANG.Add(ddh);
            _context.SaveChanges();

            // Thêm chi tiết đơn hàng
            foreach (var item in gh)
            {
                CHITIETDONTHANG ctdh = new CHITIETDONTHANG();
                ctdh.MaDonHang = ddh.MaDonHang;
                ctdh.MaXe = item.MaXe;
                ctdh.Soluong = item.soLuong;
                ctdh.Dongia = (decimal)item.Dongia;
                _context.CHITIETDONTHANG.Add(ctdh);
            }

            _context.SaveChanges();
            Session["Giohang"] = null;
            return Redirect("XacNhanDonHang");
        }

        public ActionResult XacNhanDonHang()
        {
            return View();
        }
    }
}