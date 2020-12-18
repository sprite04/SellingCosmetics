﻿using System;
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
    public class QuanLyLoaiSPController : Controller
    {
        TMDTDbContext db = new TMDTDbContext();
        // GET: QuanLyLoaiSP
        public ActionResult Index()
        {
            var model = db.LOAISANPHAMs.Where(x => x.TinhTrang == true);
            List<DANHMUC> danhmuc = new List<DANHMUC>();
            danhmuc.Add(new DANHMUC(1, "Skincare"));
            danhmuc.Add(new DANHMUC(2, "Makeup"));
            danhmuc.Add(new DANHMUC(3, "Hair Care"));
            danhmuc.Add(new DANHMUC(4, "Bath & Body"));
            danhmuc.Add(new DANHMUC(5, "Accessories"));
            danhmuc.Add(new DANHMUC(6, "Fragrance"));
            ViewBag.DanhMuc = danhmuc;
            return View(model);
        }

        public ActionResult Create()
        {
            List<DANHMUC> danhmuc = new List<DANHMUC>();
            danhmuc.Add(new DANHMUC(1, "Skincare"));
            danhmuc.Add(new DANHMUC(2, "Makeup"));
            danhmuc.Add(new DANHMUC(3, "Hair Care"));
            danhmuc.Add(new DANHMUC(4, "Bath & Body"));
            danhmuc.Add(new DANHMUC(5, "Accessories"));
            danhmuc.Add(new DANHMUC(6, "Fragrance"));
            ViewBag.DanhMuc = new SelectList(danhmuc, "IdDM", "TenDM");
            return View();
        }


        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public ActionResult Create(LOAISANPHAM loaiSP)
        {
            loaiSP.TinhTrang = true;

            if (ModelState.IsValid)
            {
                try
                {
                    db.LOAISANPHAMs.Add(loaiSP);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Product type is not created");
                }
            }
            else
            {
                ModelState.AddModelError("", "Please check the information you entered.");
            }
            List<DANHMUC> danhmuc = new List<DANHMUC>();
            danhmuc.Add(new DANHMUC(1, "Skincare"));
            danhmuc.Add(new DANHMUC(2, "Makeup"));
            danhmuc.Add(new DANHMUC(3, "Hair Care"));
            danhmuc.Add(new DANHMUC(4, "Bath & Body"));
            danhmuc.Add(new DANHMUC(5, "Accessories"));
            danhmuc.Add(new DANHMUC(6, "Fragrance"));
            ViewBag.DanhMuc = new SelectList(danhmuc, "IdDM", "TenDM",loaiSP.DanhMuc);
            return View(loaiSP);
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAISANPHAM loaiSP = db.LOAISANPHAMs.Find(id);
            if (loaiSP == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            try
            {
                loaiSP.TinhTrang = false;
                db.Entry(loaiSP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return Content("<script> alert(\"The implementation failed.\")</script>");
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAISANPHAM loaiSP = db.LOAISANPHAMs.FirstOrDefault(x => x.IdLoaiSP == id);
            if (loaiSP == null)
            {
                return HttpNotFound();
            }
            List<DANHMUC> danhmuc = new List<DANHMUC>();
            danhmuc.Add(new DANHMUC(1, "Skincare"));
            danhmuc.Add(new DANHMUC(2, "Makeup"));
            danhmuc.Add(new DANHMUC(3, "Hair Care"));
            danhmuc.Add(new DANHMUC(4, "Bath & Body"));
            danhmuc.Add(new DANHMUC(5, "Accessories"));
            danhmuc.Add(new DANHMUC(6, "Fragrance"));
            ViewBag.DanhMuc = new SelectList(danhmuc, "IdDM", "TenDM", loaiSP.DanhMuc);
            return View(loaiSP);
        }

        [HttpPost]
        [Route("Edit")]
        [ValidateInput(false)]
        public ActionResult Edit(LOAISANPHAM loaiSP)
        {
            loaiSP.TinhTrang = true;
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(loaiSP).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Product type is not created");
                }
            }
            else
            {
                ModelState.AddModelError("", "Please check the information you entered.");
            }
            List<DANHMUC> danhmuc = new List<DANHMUC>();
            danhmuc.Add(new DANHMUC(1, "Skincare"));
            danhmuc.Add(new DANHMUC(2, "Makeup"));
            danhmuc.Add(new DANHMUC(3, "Hair Care"));
            danhmuc.Add(new DANHMUC(4, "Bath & Body"));
            danhmuc.Add(new DANHMUC(5, "Accessories"));
            danhmuc.Add(new DANHMUC(6, "Fragrance"));
            ViewBag.DanhMuc = new SelectList(danhmuc, "IdDM", "TenDM", loaiSP.DanhMuc);
            return View(loaiSP);
        }
    }
}