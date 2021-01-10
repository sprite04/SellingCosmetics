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
                    return Content("<script> alert(\"Sản phẩm không còn đủ số lượng\")</script>");
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
            }

            return Redirect(strURL);
        }
        public ActionResult ThemGioHangAjax(int ?MaSP, string strURL)
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
                    return Content("<script> alert(\"Sản phẩm không còn đủ số lượng\")</script>");
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
                return RedirectToAction("GioHangPartial");
            }

            GIOHANG item = new GIOHANG() { IdSP = MaSP, SoLuong = 1, TinhTrang = true, SANPHAM=sp };
            if (sp.SoLuong < item.SoLuong)
            {
                return Content("<script> alert(\"Sản phẩm không còn đủ số lượng\")</script>");
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
            return RedirectToAction("GioHangPartial");
        }

        public ActionResult ThemGioHang(int? MaSP, string strURL)
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
                    return Content("<script> alert(\"Sản phẩm không còn đủ số lượng\")</script>");
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

            GIOHANG item = new GIOHANG() { IdSP = MaSP, SoLuong = 1, TinhTrang = true, SANPHAM = sp };
            if (sp.SoLuong < item.SoLuong)
            {
                return Content("<script> alert(\"Sản phẩm không còn đủ số lượng\")</script>");
            }
            lstGioHang.Add(item);
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

        public ActionResult DatHang(string strURL, string MaKM)
        {
            if (Session["GioHang"] == null)
                return RedirectToAction("Index", "Home");
            NGUOIDUNG user = Session["TaiKhoan"] as NGUOIDUNG;
            if (user == null)
                return RedirectToAction("DangNhap", "Home", new { strURL = strURL });
            int TienVanChuyen = db.DTGIAOHANGs.First().TienVanChuyen ?? 20000;
            int TongTienSP = TinhTongThanhTien();
            int TienGiam = 0;
            HOADON hd = new HOADON();
            hd.NgayDH = DateTime.Now;
            hd.IdKH = user.IdUser;
            hd.TinhTrang = 4;
            hd.SDT = user.SDT;
            hd.DiaChi = user.DiaChi;
            hd.IdDTGH = db.DTGIAOHANGs.SingleOrDefault(x => x.TinhTrang == true).IdDTGH;
            if (hd.KHUYENMAI != null && hd.KHUYENMAI.TinhTrang==true)
            {
                if (hd.KHUYENMAI.LoaiKM == 1)
                {
                    TienGiam = (TongTienSP * (hd.KHUYENMAI.GiaTri ?? 0)) / 100;
                }
            }
            hd.TongTien = TongTienSP + TienVanChuyen - TienGiam;
            db.HOADONs.Add(hd);
            db.SaveChanges();
            List<GIOHANG> lstGH = LayGioHang();
            foreach(var item in lstGH)
            {
                CHITIETHD ct = new CHITIETHD();
                ct.IdHD = hd.IdHD;
                ct.IdSP = item.SANPHAM.IdSP;
                ct.SoLuong = item.SoLuong;
                ct.GiaBan = (int)(item.SANPHAM.GiaGoc * item.SANPHAM.LoiNhuan);
                db.CHITIETHDs.Add(ct);
            }
            db.SaveChanges();
            var dsGH=db.GIOHANGs.Where(x => x.IdKH == user.IdUser);
            db.GIOHANGs.RemoveRange(dsGH);
            db.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XemGioHang");
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
            return listGioHang.Sum(x => (Int32)(x.SoLuong*x.SANPHAM.GiaGoc*x.SANPHAM.LoiNhuan));
        }

        // GET: GioHang
        public ActionResult XemGioHang()
        {
            int TienVanChuyen= db.DTGIAOHANGs.SingleOrDefault(x=>x.TinhTrang==true).TienVanChuyen ??20000;
            int TongTienSP = TinhTongThanhTien();
            int SoLuongSP= TinhTongSoLuong();
            ViewBag.TienVanChuyen = TienVanChuyen;
            ViewBag.TongSoLuong = SoLuongSP;
            ViewBag.TongTien = TongTienSP;
            ViewBag.IsKM = 0;
            ViewBag.KhuyenMai = "";
            ViewBag.GiamGia = 0;
            ViewBag.ThanhToan = TongTienSP + TienVanChuyen;
            List<GIOHANG> listGioHang = LayGioHang();
            return View(listGioHang);
        }

        [HttpPost]
        public ActionResult XemGioHang(FormCollection f)
        {
            string MaKM = f["MaKM"];
            int TienVanChuyen = db.DTGIAOHANGs.First().TienVanChuyen ?? 20000;
            int TongTienSP = TinhTongThanhTien();
            int SoLuongSP = TinhTongSoLuong();
            ViewBag.TienVanChuyen = TienVanChuyen;
            ViewBag.TongSoLuong = SoLuongSP;
            ViewBag.TongTien = TongTienSP;
            ViewBag.IsKM = 0;
            ViewBag.GiamGia = 0;
            List<GIOHANG> listGioHang = LayGioHang();
            KHUYENMAI km= db.KHUYENMAIs.FirstOrDefault(x => x.MaKM == MaKM);
            if(km==null)
            {
                ViewBag.KhuyenMai = "";
                ViewBag.IsKM = 1;
                ViewBag.ThanhToan = TongTienSP + TienVanChuyen;
            }
            else if(km.TinhTrang == false)
            {
                ViewBag.KhuyenMai = "";
                ViewBag.IsKM = 2;
                ViewBag.ThanhToan = TongTienSP + TienVanChuyen;
            }
            else
            {
                int TienGiam = 0;
                ViewBag.KhuyenMai = MaKM;
                ViewBag.IsKM = 3;
                if(km.LoaiKM==1)
                {
                    TienGiam = (TongTienSP * (km.GiaTri??0)) / 100;
                    ViewBag.ThanhToan = TongTienSP + TienVanChuyen-TienGiam;
                    ViewBag.GiamGia = TienGiam;
                }    
                
            }    
            
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