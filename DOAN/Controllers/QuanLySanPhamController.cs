using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DOAN.Models;

namespace DOAN.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        TMDTDbContext db = new TMDTDbContext();
        // GET: QuanLySanPham
        public ActionResult Index()
        {
            var model = db.SANPHAMs.Where(x => (x.TinhTrang == 1||x.TinhTrang==2));
            ViewBag.ThuongHieu = db.THUONGHIEUx.Where(x => x.TinhTrang == true);
            return View(model);
        }

        public ActionResult Create(int ?idTH)
        {
            if(idTH==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THUONGHIEU th = db.THUONGHIEUx.FirstOrDefault(x => x.IdTH == idTH);
            if(th==null)
            {
                Response.StatusCode = 404;
                return null;
            }    
            
            ViewBag.MaKM = new SelectList(db.KHUYENMAIs, "IdMa", "MaKM");
            ViewBag.IdTH = new SelectList(db.THUONGHIEUx, "IdTH", "TenTH",th.IdTH);
            ViewBag.IdLoaiSP = new SelectList(db.LOAISANPHAMs, "IdLoaiSP", "TenLoai");
            return View();
        }


        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public ActionResult Create(SANPHAM sp, HttpPostedFileBase [] AnhSP)
        {
            for(int i=0; i<AnhSP.Length; i++)
            {
                if (AnhSP[i]!=null&& i==0 &&AnhSP[i].ContentLength > 0)
                {
                    string tenth = db.THUONGHIEUx.FirstOrDefault(x => x.IdTH == sp.IdTH).TenTH.ToLower();
                    var fileName = Path.GetFileName(AnhSP[i].FileName);
                    var path = Path.Combine(Server.MapPath("~/assets/client/hinhsp/"+tenth), fileName);
                    sp.AnhSP = fileName;
                    if (!System.IO.File.Exists(path))
                    {
                        AnhSP[i].SaveAs(path);
                    }
                }
            }    
            
            sp.NgayTao = DateTime.Now;
            sp.SoLanMua = 0;
            sp.SoLuong = 0;
            if (sp.SoLuong > 0)
                sp.TinhTrang = 1;
            else
                sp.TinhTrang = 2;

            if (ModelState.IsValid)
            {
                try
                {
                    db.SANPHAMs.Add(sp);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Product creation failed");
                    ViewBag.MaKM = new SelectList(db.KHUYENMAIs, "IdMa", "MaKM",sp.MaKM);
                    ViewBag.IdTH = new SelectList(db.THUONGHIEUx, "IdTH", "TenTH",sp.IdTH);
                    ViewBag.IdLoaiSP = new SelectList(db.LOAISANPHAMs, "IdLoaiSP", "TenLoai",sp.IdLoaiSP);
                }
            }
            else
            {
                ModelState.AddModelError("", "Please check the information you entered.");
                ViewBag.MaKM = new SelectList(db.KHUYENMAIs, "IdMa", "MaKM", sp.MaKM);
                ViewBag.IdTH = new SelectList(db.THUONGHIEUx, "IdTH", "TenTH", sp.IdTH);
                ViewBag.IdLoaiSP = new SelectList(db.LOAISANPHAMs, "IdLoaiSP", "TenLoai", sp.IdLoaiSP);
            }
            return View(sp);
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sp = db.SANPHAMs.Find(id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            try
            {
                sp.TinhTrang = 10;
                db.Entry(sp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return Content("<script> alert(\"Quá trình thực hiện thất bại\")</script>");
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sp = db.SANPHAMs.FirstOrDefault(x => x.IdSP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKM = new SelectList(db.KHUYENMAIs, "IdMa", "MaKM", sp.MaKM);
            ViewBag.IdTH = new SelectList(db.THUONGHIEUx, "IdTH", "TenTH", sp.IdTH);
            ViewBag.IdLoaiSP = new SelectList(db.LOAISANPHAMs, "IdLoaiSP", "TenLoai", sp.IdLoaiSP);
            return View(sp);
        }

        [HttpPost]
        [Route("Edit")]
        [ValidateInput(false)]
        public ActionResult Edit(SANPHAM sp, HttpPostedFileBase[] AnhSP)
        {
            for (int i = 0; i < AnhSP.Length; i++)
            {
                if (AnhSP[i] != null && i == 0 && AnhSP[i].ContentLength > 0)
                {
                    string tenth = db.THUONGHIEUx.FirstOrDefault(x => x.IdTH == sp.IdTH).TenTH.ToLower();
                    var fileName = Path.GetFileName(AnhSP[i].FileName);
                    var path = Path.Combine(Server.MapPath("~/assets/client/hinhsp/" + tenth), fileName);
                    sp.AnhSP = fileName;
                    if (!System.IO.File.Exists(path))
                    {
                        AnhSP[i].SaveAs(path);
                    }
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(sp).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Product creation failed");
                    ViewBag.MaKM = new SelectList(db.KHUYENMAIs, "IdMa", "MaKM", sp.MaKM);
                    ViewBag.IdTH = new SelectList(db.THUONGHIEUx, "IdTH", "TenTH", sp.IdTH);
                    ViewBag.IdLoaiSP = new SelectList(db.LOAISANPHAMs, "IdLoaiSP", "TenLoai", sp.IdLoaiSP);
                }
            }
            else
            {
                ModelState.AddModelError("", "Please check the information you entered.");
                ViewBag.MaKM = new SelectList(db.KHUYENMAIs, "IdMa", "MaKM", sp.MaKM);
                ViewBag.IdTH = new SelectList(db.THUONGHIEUx, "IdTH", "TenTH", sp.IdTH);
                ViewBag.IdLoaiSP = new SelectList(db.LOAISANPHAMs, "IdLoaiSP", "TenLoai", sp.IdLoaiSP);
            }
            return View(sp);
        }
    }
}