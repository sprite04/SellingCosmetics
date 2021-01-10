using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DOAN.Common;
using DOAN.Models;

namespace DOAN.Controllers
{
    //[Authorize(Roles = "*")]
    public class NguoiDungController : Controller
    {

        TMDTDbContext db = new TMDTDbContext();
        // GET: NguoiDung
        public ActionResult Index()
        {
            var list = db.NGUOIDUNGs.Where(x => x.TT_User == true);
            var listND = db.LOAIUSERs;
            ViewBag.items = new SelectList(listND, "IdLoaiUser", "TenLoai");
            ViewBag.GiaTri = 0;
            ViewBag.DanhSach = list;

            return View(list);
        }

        [HttpPost]
        public ActionResult Index(FormCollection f)
        {
            var kq = f["ddlNguoiDung"];
            var listND = db.LOAIUSERs;

            if (kq != "")
            {
                int giatri = int.Parse(kq);
                var list = db.NGUOIDUNGs.Where(x => x.TT_User == true && x.IdLoaiUser==giatri);
                ViewBag.DanhSach = list;
                ViewBag.items = new SelectList(listND, "IdLoaiUser", "TenLoai",giatri);
                ViewBag.GiaTri = giatri;
                return View(list);
            }
            else
            {
                var list = db.NGUOIDUNGs.Where(x => x.TT_User == true);
                ViewBag.DanhSach = list;
                ViewBag.items = new SelectList(listND, "IdLoaiUser", "TenLoai");
                ViewBag.GiaTri = 0;
                return View(list);
            }
        }

        public ActionResult Create()
        {
            ViewBag.IdLoaiUser = new SelectList(db.LOAIUSERs, "IdLoaiUser", "TenLoai");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public ActionResult Create(NGUOIDUNG nd, HttpPostedFileBase Avatar)
        {
            if(Avatar!=null)
            {
                if (Avatar.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(Avatar.FileName);
                    var path = Path.Combine(Server.MapPath("~/assets/admin/hinhnd"), fileName);
                    nd.Avatar = fileName;
                    if (!System.IO.File.Exists(path))
                    {
                        Avatar.SaveAs(path);
                    }
                }
            }    
            
            nd.Password = Encryptor.MD5Hash(nd.Password);
            nd.Password1 = Encryptor.MD5Hash(nd.Password1);
            nd.NgayTao = DateTime.Now;
            nd.TT_User = true;
            
            if (ModelState.IsValid)
            {
                if (db.NGUOIDUNGs.Where(x => x.Username == nd.Mail.Trim()).Count() == 0)
                {
                    try
                    {
                        nd.Username = nd.Mail.Trim();
                        db.NGUOIDUNGs.Add(nd);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", "Quá trình thực hiện thất bại.");
                        ViewBag.IdLoaiUser = new SelectList(db.LOAIUSERs, "IdLoaiUser", "TenLoai", nd.IdLoaiUser);
                    }
                    
                }
                else
                {
                    ModelState.AddModelError("", "Email bạn nhập đã được sử dụng. Vui lòng sử dụng email khác.");
                    ViewBag.IdLoaiUser = new SelectList(db.LOAIUSERs, "IdLoaiUser", "TenLoai", nd.IdLoaiUser);
                }    
            }
            else
            {
                ModelState.AddModelError("", "Vui lòng kiểm tra lại thông tin đã nhập.");
                ViewBag.IdLoaiUser = new SelectList(db.LOAIUSERs, "IdLoaiUser", "TenLoai", nd.IdLoaiUser);
            }    
            return View(nd);
        }


        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGUOIDUNG nd = db.NGUOIDUNGs.Find(id);
            if (nd==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            try
            {
                nd.TT_User = false;
                nd.Mail = nd.Mail.Trim();
                nd.SDT = nd.SDT.Trim();
                db.Entry(nd).State = EntityState.Modified;
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

            NGUOIDUNG nd = db.NGUOIDUNGs.FirstOrDefault(x => x.IdUser == id);
            if (nd == null)
            {
                return HttpNotFound();
            }
            nd.Mail = nd.Mail.Trim();
            nd.SDT = nd.SDT.Trim();
            ViewBag.IdLoaiUser = new SelectList(db.LOAIUSERs, "IdLoaiUser", "TenLoai", nd.IdLoaiUser);
            return View(nd);
        }

        [HttpPost]
        [Route("Edit")]
        public ActionResult Edit(NGUOIDUNG nd, HttpPostedFileBase Avatar)
        {
            if(Avatar!=null)
            {
                if (Avatar.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(Avatar.FileName);
                    var path = Path.Combine(Server.MapPath("~/assets/admin/hinhnd"), fileName);
                    nd.Avatar = fileName;
                    if (!System.IO.File.Exists(path))
                    {
                        Avatar.SaveAs(path);
                    }
                }
            }

            
            
            if (ModelState.IsValid)
            {
                try
                {
                    nd.Mail = nd.Mail.Trim();
                    nd.SDT = nd.SDT.Trim();
                    db.Entry(nd).State = EntityState.Modified;
                    db.SaveChanges();
                    NGUOIDUNG user = Session["TaiKhoan"] as NGUOIDUNG;
                    if (user != null)
                    {
                        if (user.IdUser==nd.IdUser)
                        {
                            Session["TaiKhoan"] = db.NGUOIDUNGs.FirstOrDefault(x => x.IdUser == nd.IdUser);
                        }
                    }
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Quá trình thực hiện thất bại.");
                    ViewBag.IdLoaiUser = new SelectList(db.LOAIUSERs, "IdLoaiUser", "TenLoai", nd.IdLoaiUser);
                }
            }
            else
            {
                ModelState.AddModelError("", "Vui lòng kiểm tra lại thông tin đã nhập.");
                ViewBag.IdLoaiUser = new SelectList(db.LOAIUSERs, "IdLoaiUser", "TenLoai", nd.IdLoaiUser);
            }
            return View(nd);
        }
    }
}