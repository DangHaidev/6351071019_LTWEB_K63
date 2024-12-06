using Microsoft.Ajax.Utilities;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLab.Models;

namespace WebLab.Controllers
{
    public class QuanLyXeController : Controller
    {
        QLBanXeGanMayEntities db = new QLBanXeGanMayEntities();
        // GET: QuanLyXe
        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNum = (page ?? 1);

            var data = db.XEGANMAY.ToList()
                .OrderByDescending(p => p.Ngaycapnhat)                
                .ToPagedList(pageNum, pageSize);
                ;
            return View(data);
        }

        public ActionResult LoaiXe()
        {
            var loaixe = from cd in db.LOAIXE select cd;
            return View(loaixe);
        }
        public ActionResult NhaPhanPhoi()
        {
            var nhapp = from cd in db.NHAPHANPHOI select cd;
            return View(nhapp);
        }
        
        public ActionResult SPTheoLoaiXe(int id)
        {
            var nhapp = from cd in db.XEGANMAY where cd.MaLX==id select cd;
            return View(nhapp);
        } 
        public ActionResult SPTheoNhaPP(int id)
        {
            var nhapp = from cd in db.XEGANMAY where cd.MaNPP==id select cd;
            return View(nhapp);
        }

        public ActionResult Details(int id)
        {
            var data = db.XEGANMAY.SingleOrDefault(p => p.MaXe == id);
            return View(data);
        }
    }
}