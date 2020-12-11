using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOAN.Models;

namespace DOAN.Controllers
{
    public class GioHangController : Controller
    {
        TMDTDbContext db = new TMDTDbContext();
        public List<GIOHANG> LayGioHang()
        {
            List<GIOHANG> lstGioHang = Session["GioHang"] as List<GIOHANG>;
            if(lstGioHang==null)
            {
                NGUOIDUNG user = Session["TaiKhoan"] as NGUOIDUNG;
                if(user!=null)
                {
                    lstGioHang = db.GIOHANGs.Where(x => x.IdKH == user.IdUser).ToList();
                    if(lstGioHang==null)
                    {
                        lstGioHang = new List<GIOHANG>();
                    }    
                }
                else
                    lstGioHang = new List<GIOHANG>();
                Session["GioHang"] = lstGioHang;
            }    
            return lstGioHang;
        }

        public ActionResult GiamGioHang(int? MaSP, string strURL)
        {
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(x => x.IdSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GIOHANG> lstGioHang = LayGioHang();
            NGUOIDUNG user = Session["TaiKhoan"] as NGUOIDUNG;
            GIOHANG spCheck = lstGioHang.SingleOrDefault(x => x.IdSP == MaSP);
            if (spCheck != null)
            {
                if (sp.SoLuong < spCheck.SoLuong - 1)
                {
                    spCheck.TinhTrang = false;
                }
                else
                {
                    spCheck.TinhTrang = true;
                    if (spCheck.SoLuong > 1)
                        spCheck.SoLuong--;
                }


                if (user != null)
                {
                    var ds = db.GIOHANGs.Where(x => x.IdKH == user.IdUser);
                    db.GIOHANGs.RemoveRange(ds);
                    foreach (var i in lstGioHang)
                    {
                        GIOHANG gh = new GIOHANG()
                        {
                            IdKH = user.IdUser,
                            IdSP = i.IdSP,
                            SoLuong = i.SoLuong,
                            TinhTrang = i.TinhTrang
                        };
                        db.GIOHANGs.Add(gh);
                    }
                    db.SaveChanges();
                }
                return Redirect(strURL);
            }


            return Redirect(strURL);
        }
        public ActionResult ThemGioHang(int ?MaSP, string strURL)
        {
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(x => x.IdSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GIOHANG> lstGioHang = LayGioHang();
            NGUOIDUNG user = Session["TaiKhoan"] as NGUOIDUNG;
            GIOHANG spCheck = lstGioHang.SingleOrDefault(x => x.IdSP == MaSP);
            if (spCheck != null)
            {
                if (sp.SoLuong < spCheck.SoLuong)
                {
                    spCheck.TinhTrang = false;
                }
                else
                {
                    spCheck.TinhTrang = true;
                    spCheck.SoLuong++;
                }

                
                if (user != null)
                {
                    var ds = db.GIOHANGs.Where(x => x.IdKH == user.IdUser);
                    db.GIOHANGs.RemoveRange(ds);
                    foreach (var i in lstGioHang)
                    {
                        GIOHANG gh = new GIOHANG()
                        {
                            IdKH = user.IdUser,
                            IdSP = i.IdSP,
                            SoLuong = i.SoLuong,
                            TinhTrang = i.TinhTrang
                        };
                        db.GIOHANGs.Add(gh);
                    }
                    db.SaveChanges();
                }
                return Redirect(strURL);
            }

            GIOHANG item = new GIOHANG() { IdSP = MaSP, SoLuong = 1, TinhTrang = true, SANPHAM=sp };
            if (sp.SoLuong < item.SoLuong)
            {
                return Content("<h3>Không đủ số lượng</h3>");
            }
            lstGioHang.Add(item);
            if(user!=null)
            {
                var ds = db.GIOHANGs.Where(x => x.IdKH == user.IdUser);
                db.GIOHANGs.RemoveRange(ds);
                foreach(var i in lstGioHang)
                {
                    GIOHANG gh = new GIOHANG()
                    {
                        IdKH=user.IdUser,
                        IdSP=i.IdSP,
                        SoLuong=i.SoLuong,
                        TinhTrang=i.TinhTrang
                    };
                    db.GIOHANGs.Add(gh);
                }
                db.SaveChanges();
            }    
            return Redirect(strURL);
        }

        public ActionResult XoaGioHang(int? MaSP, string strURL)
        {
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(x => x.IdSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GIOHANG> lstGioHang = LayGioHang();
            NGUOIDUNG user = Session["TaiKhoan"] as NGUOIDUNG;
            GIOHANG spCheck = lstGioHang.SingleOrDefault(x => x.IdSP == MaSP);
            if (spCheck != null)
            {
                lstGioHang.Remove(spCheck);


                if (user != null)
                {
                    var ds = db.GIOHANGs.Where(x => x.IdKH == user.IdUser);
                    db.GIOHANGs.RemoveRange(ds);
                    foreach (var i in lstGioHang)
                    {
                        GIOHANG gh = new GIOHANG()
                        {
                            IdKH = user.IdUser,
                            IdSP = i.IdSP,
                            SoLuong = i.SoLuong,
                            TinhTrang = i.TinhTrang
                        };
                        db.GIOHANGs.Add(gh);
                    }
                    db.SaveChanges();
                }
                return Redirect(strURL);
            }


            return Redirect(strURL);
        }

        public int TinhTongSoLuong()
        {
            List<GIOHANG> listGioHang = Session["GioHang"] as List<GIOHANG>;
            if (listGioHang == null)
                return 0;
            return listGioHang.Sum(x => x.SoLuong);
        }

        public int TinhTongThanhTien()
        {
            List<GIOHANG> listGioHang = Session["GioHang"] as List<GIOHANG>;
            if (listGioHang == null)
                return 0;
            return listGioHang.Sum(x => (x.SoLuong*x.SANPHAM.GiaGoc));
        }

        // GET: GioHang
        public ActionResult XemGioHang()
        {
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongThanhTien();
            List<GIOHANG> listGioHang = LayGioHang();
            return View(listGioHang);
        }

        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongThanhTien();
            return PartialView();
        }
    }
}