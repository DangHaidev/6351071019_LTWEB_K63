using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebLab.Models;
using WebLab.ViewModels;

namespace WebLab.Controllers
{
    public class AdminController : Controller
    {
        QLBanXeGanMayEntities db = new QLBanXeGanMayEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
         public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginAdminVM model)
        {
            if (ModelState.IsValid)
            {
                var admin = db.Admin.FirstOrDefault(p => p.UserAdmin == model.UserAdmin && p.PassAdmin == model.PassAdmin);


                if (admin != null)
                {
                    ViewBag.ThongBao = "Chúc mừng đăng nhập thành công!";
                    Session["TaiKhoanAD"] = admin;

                    return RedirectToAction("xeganmay", "Admin");
                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }

        public ActionResult Xeganmay(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            return View(db.XEGANMAY.ToList().OrderBy(m => m.MaXe).ToPagedList(pageNumber,pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.LoaiXe = new SelectList( db.LOAIXE.ToList(),"MaLX","TenLoaiXe");
            ViewBag.NhaPP = new SelectList(db.NHAPHANPHOI.ToList(),"MaNPP", "TenNPP");

            return View();

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(XEGANMAY model, HttpPostedFileBase fileUpload)
        {

            ViewBag.LoaiXe = new SelectList(db.LOAIXE.ToList(), "MaLX", "TenLoaiXe");
            ViewBag.NhaPP = new SelectList(db.NHAPHANPHOI.ToList(), "MaNPP", "TenNPP");

            if (fileUpload == null)
            {
                ViewBag.ThongBao = "Vui lòng chọn ảnh bìa";
                return View();
            }

            // Thêm vào CSDL
            if (ModelState.IsValid)
            {
                // Lưu tên file
                var fileName = Path.GetFileName(fileUpload.FileName);
                // Lưu đường dẫn của file
                var path = Path.Combine(Server.MapPath("~/Content/HinhSanPham/"), fileName);

                // Kiểm tra hình ảnh tồn tại chưa
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                }
                else
                {
                    // Lưu hình ảnh vào đường dẫn
                    fileUpload.SaveAs(path);

                    // Gán đường dẫn ảnh vào thuộc tính Anhbia
                    model.Anhbia = fileName;

                    try
                    {
                        // Thêm vào CSDL
                        db.XEGANMAY.Add(model);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (DbEntityValidationException ex)
                    {
                        // Hiển thị lỗi xác thực
                        foreach (var validationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                ModelState.AddModelError(validationError.PropertyName, validationError.ErrorMessage);
                            }
                        }
                    }
                }
            }


            return View();
        }

        public ActionResult Details(int id)
        {
            var xe = db.XEGANMAY.SingleOrDefault(n => n.MaXe == id);
            ViewBag.MaXe = xe.MaXe;
            if (xe == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(xe);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var xe = db.XEGANMAY.SingleOrDefault(n => n.MaXe == id);
            ViewBag.Masach = xe.MaXe;
            if (xe == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(xe);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Xacnhanxoa(int id)
        {
            var xe = db.XEGANMAY.SingleOrDefault(m => m.MaXe == id);
            ViewBag.Masach = xe.MaXe;
            if (xe == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.XEGANMAY.Remove(xe);
            db.SaveChanges();
            return RedirectToAction("xeganmay");
        }



        [HttpGet]
        public ActionResult Edit(int id = 9)
        {
            ViewBag.LoaiXe = new SelectList(db.LOAIXE.ToList(), "MaLX", "TenLoaiXe");
            ViewBag.NhaPP = new SelectList(db.NHAPHANPHOI.ToList(), "MaNPP", "TenNPP");
            var xe = db.XEGANMAY.SingleOrDefault(n => n.MaXe == id);
            ViewBag.Masach = xe.MaXe;
            if (xe == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(xe);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(XEGANMAY xe, HttpPostedFileBase fileupload)
        {
            ViewBag.LoaiXe = new SelectList(db.LOAIXE.ToList(), "MaLX", "TenLoaiXe");
            ViewBag.NhaPP = new SelectList(db.NHAPHANPHOI.ToList(), "MaNPP", "TenNPP");
            
            if (fileupload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View(xe);
            }
            else
            {

                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/HinhSanPham/"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                        return View(xe);
                    }
                    else
                    {
                        fileupload.SaveAs(path);
                    }
                    xe.Anhbia = fileName;
                    db.XEGANMAY.AddOrUpdate(xe);
                    //UpdateModel(sach);

                    db.SaveChanges();

                }
                return RedirectToAction("xeganmay");
            }
        }

        public ActionResult ThongKeXe()
        {
            var bookStatistics = db.XEGANMAY
           .GroupBy(s => s.LOAIXE.TenLoaiXe)
           .ToDictionary(
               g => g.Key?.ToString() ?? "Unknown", // Convert nullable int to string, handle null as "Unknown"
               g => g.Count()
           );

            return View(bookStatistics); // Pass the dictionary as a model to the view
        }
    }
}