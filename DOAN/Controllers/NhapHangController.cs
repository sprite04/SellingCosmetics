using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DOAN.Models;


namespace DOAN.Controllers
{
    public class NhapHangController : Controller
    {
        TMDTDbContext db = new TMDTDbContext();
        // GET: NhapHang
        public ActionResult Index()
        {
            var model = db.NHAPHANGs.OrderByDescending(x => x.NgayNhap);
            return View(model);
        }

       
        public ActionResult Delete(DateTime dt, int ? idSP)
        {
            if (idSP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHAPHANG nh = db.NHAPHANGs.FirstOrDefault(x => x.IdSP == idSP && x.NgayNhap == dt);
            if (nh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            try
            {
                db.NHAPHANGs.Remove(nh);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return Content("<script> alert(\"Quá trình thực hiện thất bại\")</script>");
            }
        }

        public ActionResult PhieuNhap()
        {
            ViewBag.SanPham = db.SANPHAMs;
            return View();
        }

        [HttpPost]
        public ActionResult PhieuNhap(IEnumerable<NHAPHANG> Model, FormCollection f)
        {
            try
            {
                DateTime dt = DateTime.Parse(f["NgayNhap"].ToString());
                foreach (var item in Model)
                {
                    NHAPHANG nh = new NHAPHANG();
                    nh.IdSP = item.IdSP;
                    nh.NgayNhap = dt;
                    nh.SoLuong = item.SoLuong;
                    nh.GiaNhap = item.GiaNhap;
                    if (db.NHAPHANGs.Where(x => x.NgayNhap == nh.NgayNhap && x.IdSP == nh.IdSP).Count() > 0)
                    {
                        db.Entry(nh).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        db.NHAPHANGs.Add(nh);
                        db.SaveChanges();
                    }

                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Please check the information you entered.");
                ViewBag.SanPham = db.SANPHAMs;
                return View();
            }
            
        }
    }
}