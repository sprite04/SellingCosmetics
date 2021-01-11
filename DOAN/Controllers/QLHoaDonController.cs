using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOAN.Models;

namespace DOAN.Controllers
{
    public class QLHoaDonController : Controller
    {
        TMDTDbContext db = new TMDTDbContext();
        // GET: QLHoaDon
        public ActionResult Index(int error=0)
        {
            var list = db.HOADONs.Where(x => x.TinhTrang >=4 && x.TinhTrang<=9).OrderByDescending(y=>y.NgayDH);
            var listTT = db.TINHTRANGs.Where(x => x.IdTT >= 4 && x.IdTT <= 9);
            ViewBag.items = new SelectList(listTT, "IdTT", "TenTT");
            ViewBag.GiaTri = 0;
            ViewBag.DanhSach = list;

            ViewBag.Error = error;

            return View(list);
        }

        [HttpPost]
        public ActionResult Index(FormCollection f)
        {
            var kq = f["ddlTinhTrang"];
            var listTT = db.TINHTRANGs.Where(x => x.IdTT >= 4 && x.IdTT <= 9);

            if (kq != "")
            {
                int giatri = int.Parse(kq);
                var list = db.HOADONs.Where(x => x.TinhTrang ==giatri).OrderBy(y => y.NgayDH);
                ViewBag.DanhSach = list;
                ViewBag.items = new SelectList(listTT, "IdTT", "TenTT",giatri);
                ViewBag.GiaTri = giatri;
                return View(list);
            }
            else
            {
                var list = db.HOADONs.Where(x => x.TinhTrang >= 4 && x.TinhTrang <= 9).OrderByDescending(y => y.NgayDH);
                ViewBag.DanhSach = list;
                ViewBag.items = new SelectList(listTT, "IdTT", "TenTT");
                ViewBag.GiaTri = 0;
                return View(list);
            }
        }

        public ActionResult ChiTiet(int id)
        {
            var hoadon = db.HOADONs.Find(id);
            if (hoadon == null)
                return HttpNotFound();
            var listCT = db.CHITIETHDs.Where(x => x.IdHD == id);

            ViewBag.DoiTacGH = db.DTGIAOHANGs.SingleOrDefault(x=>x.IdDTGH==hoadon.IdDTGH);
            ViewBag.HoaDon = hoadon;
            ViewBag.ChiTiet = listCT;
            return View();
        }

        
    }
}