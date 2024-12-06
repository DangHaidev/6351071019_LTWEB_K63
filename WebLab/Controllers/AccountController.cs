using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebLab.Models;
using WebLab.ViewModels;

namespace WebLab.Controllers
{
    public class AccountController : Controller
    {
        QLBanXeGanMayEntities db = new QLBanXeGanMayEntities();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newUser = new KHACHHANG
                    {
                        HoTen = model.HoTen,
                        Taikhoan = model.Taikhoan,
                        Matkhau = (model.Matkhau), 
                        Email = model.Email,
                        DiachiKH = model.DiachiKH,
                        DienthoaiKH = model.DienthoaiKH,
                        Ngaysinh = model.NgaySinh,
                        // Các trường khác nếu cần
                    };
                    db.KHACHHANG.Add(newUser);
                    db.SaveChanges();

                    return RedirectToAction("SignIn");
                } catch(Exception e) {
                    ModelState.AddModelError("", "Đã xảy ra lỗi khi lưu thông tin người dùng. Vui lòng thử lại.");
                }
            }

            // Nếu dữ liệu không hợp lệ, trả lại view với các lỗi
            return View(model);
        }
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
            public ActionResult SignIn(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var customer = db.KHACHHANG.FirstOrDefault(p => p.Taikhoan == model.Taikhoan && p.Matkhau == model.Matkhau);

                if (customer != null)
                {
                    ViewBag.ThongBao = "Chúc mừng đăng nhập thành công!";
                    Session["TaiKhoan"] = customer;

                    return RedirectToAction("Index", "QuanLyXe");
                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";                
                }



            }
            return View();
        }
    }
}