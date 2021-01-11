using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOAN.Models;
using DOAN.Common;
using System.Net;
using System.Data.Entity;

namespace DOAN.Controllers
{
    //[Authorize(Roles = "*")]
    public class QLKhuyenMaiController : Controller
    {
        // GET: QLKhuyenMai
        TMDTDbContext db = new TMDTDbContext();
        public ActionResult Index()
        {
            var list = db.KHUYENMAIs.Where(x => x.TinhTrang == true);
            return View(list);
        }

        public ActionResult Create()
        {
            List<LoaiKM> listLoai = new List<LoaiKM>();
            LoaiKM LOAI1 = new LoaiKM();
            LOAI1.IdLoai = 1;
            LOAI1.TenLoai = "Giảm phần trăm";
            listLoai.Add(LOAI1);
            LoaiKM LOAI2 = new LoaiKM();
            LOAI2.IdLoai = 2;
            LOAI2.TenLoai = "Giảm trực tiếp";
            listLoai.Add(LOAI2);

            ViewBag.LoaiKM = new SelectList(listLoai, "IdLoai", "TenLoai");
            return View();
        }


        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(KHUYENMAI khuyenmai)
        {
            khuyenmai.TinhTrang = true;
            if (ModelState.IsValid)
            {
                try
                {
                    db.KHUYENMAIs.Add(khuyenmai);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Quá trình thực hiện thất bại.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Vui lòng kiểm tra lại thông tin đã nhập.");
            }
            List<LoaiKM> listLoai = new List<LoaiKM>();
            LoaiKM LOAI1 = new LoaiKM();
            LOAI1.IdLoai = 1;
            LOAI1.TenLoai = "Giảm phần trăm";
            listLoai.Add(LOAI1);
            LoaiKM LOAI2 = new LoaiKM();
            LOAI2.IdLoai = 2;
            LOAI2.TenLoai = "Giảm trực tiếp";
            listLoai.Add(LOAI2);

            ViewBag.LoaiKM = new SelectList(listLoai, "IdLoai", "TenLoai",khuyenmai.LoaiKM);
            return View(khuyenmai);
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHUYENMAI khuyenmai = db.KHUYENMAIs.Find(id);
            if (khuyenmai == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            try
            {
                khuyenmai.TinhTrang = false;
                db.Entry(khuyenmai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return Content("<script> alert(\"Quá trình thực hiện thất bại.\")</script>");
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHUYENMAI khuyenmai = db.KHUYENMAIs.SingleOrDefault(x => x.IdMa == id);
            if (khuyenmai == null)
            {
                return HttpNotFound();
            }

            List<LoaiKM> listLoai = new List<LoaiKM>();
            LoaiKM LOAI1 = new LoaiKM();
            LOAI1.IdLoai = 1;
            LOAI1.TenLoai = "Giảm phần trăm";
            listLoai.Add(LOAI1);
            LoaiKM LOAI2 = new LoaiKM();
            LOAI2.IdLoai = 2;
            LOAI2.TenLoai = "Giảm trực tiếp";
            listLoai.Add(LOAI2);

            ViewBag.LoaiKM = new SelectList(listLoai, "IdLoai", "TenLoai",khuyenmai.LoaiKM);
            return View(khuyenmai);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(KHUYENMAI khuyenmai)
        {
            khuyenmai.TinhTrang = true;
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(khuyenmai).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Quá trình thực hiện thất bại.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Vui lòng kiểm tra lại thông tin đã nhập.");
            }

            List<LoaiKM> listLoai = new List<LoaiKM>();
            LoaiKM LOAI1 = new LoaiKM();
            LOAI1.IdLoai = 1;
            LOAI1.TenLoai = "Giảm phần trăm";
            listLoai.Add(LOAI1);
            LoaiKM LOAI2 = new LoaiKM();
            LOAI2.IdLoai = 2;
            LOAI2.TenLoai = "Giảm trực tiếp";
            listLoai.Add(LOAI2);

            ViewBag.LoaiKM = new SelectList(listLoai, "IdLoai", "TenLoai",khuyenmai.LoaiKM);
            return View(khuyenmai);
        }

    }
}