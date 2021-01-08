using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOAN.Models;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc;
using DOAN.Common;
using System.Web.Security;

namespace DOAN.Controllers
{
    public class HomeController : Controller
    {
        TMDTDbContext db = new TMDTDbContext();
        // GET: Home
        public ActionResult Index()
        {
            var listSP = db.SANPHAMs.Where(x => x.TinhTrang == 1);
            ViewBag.listTH = db.THUONGHIEUx;
            

            return View();
        }

        [ChildActionOnly]
        public ActionResult FeaturedBrandsPartial()
        {
            var listSP = db.SANPHAMs.Where(x => x.TinhTrang == 1);
            ViewBag.listTH = db.THUONGHIEUx;
            return PartialView(listSP);
        }


        [ChildActionOnly]
        public ActionResult HotItemPartial()
        {
            var listSP = db.SANPHAMs.Where(x => x.TinhTrang == 1);
            
            return PartialView(listSP);
        }

     
        public ActionResult MenuPartial()
        {
            var listSP = db.SANPHAMs;
            ViewBag.listSP =listSP ;
            var listLoai = db.LOAISANPHAMs;
            return PartialView(listLoai);
        }

        [HttpGet]
        public ActionResult DangKy()
        {
            ViewBag.ThongBao =0;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(NGUOIDUNG user)
        {
            ViewBag.ThongBao = 0;
            if (this.IsCaptchaValid("Captcha is not valid"))
            {
                user.Password = Encryptor.MD5Hash(user.Password);
                user.Password1 = Encryptor.MD5Hash(user.Password1);
                user.NgayTao = DateTime.Now;
                user.TT_User = true;
                user.IdLoaiUser = 1;
                if (ModelState.IsValid)
                {
                    if (db.NGUOIDUNGs.Where(x => x.Username == user.Mail.Trim()).Count()==0)
                    {
                        user.Username = user.Mail.Trim();
                        db.NGUOIDUNGs.Add(user);
                        db.SaveChanges();
                        ViewBag.ThongBao = 1;
                    }
                    else
                        ViewBag.ThongBao = 2;
                }
                else
                {
                    ViewBag.ThongBao = 4;
                }
            }
            else
                ViewBag.ThongBao = 3;
            return View();
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            ViewBag.ThongBao = 0;
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection f, string strURL)
        {
            ViewBag.ThongBao = 0;
            string username = f["username"].ToString();
            string password = Encryptor.MD5Hash(f["password"].ToString());
            var user = db.NGUOIDUNGs.SingleOrDefault(x => x.Username == username && x.Password == password);
            if(user!=null)
            {
                IEnumerable<PHANQUYEN> lstQuyen = db.PHANQUYENs.Where(x => x.IdLoaiUser == user.IdLoaiUser);
                string Quyen = "";
                foreach (var item in lstQuyen)
                {
                    Quyen += item.TinhNang + ",";
                }
                Quyen = Quyen.Substring(0, Quyen.Length - 1);
                PhanQuyen(username, Quyen);


                Session["TaiKhoan"] = user;
                List<GIOHANG> lstGioHang = Session["GioHang"] as List<GIOHANG>;
                if (lstGioHang != null)
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
                else
                {
                    var listGH = db.GIOHANGs.Where(x => x.IdKH == user.IdUser).ToList();
                    Session["GioHang"] = listGH;
                }
                return Redirect(strURL);
            }
            ViewBag.ThongBao = 1;
            return View();
        }

        public void PhanQuyen(string username, string quyen)
        {
            FormsAuthentication.Initialize();
            var ticket = new FormsAuthenticationTicket(1, username, DateTime.Now,
                                                        DateTime.Now.AddHours(3), //timeout
                                                        false,//remember me
                                                        quyen,
                                                        FormsAuthentication.FormsCookiePath);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            if (ticket.IsPersistent)
                cookie.Expires = ticket.Expiration;
            Response.Cookies.Add(cookie);

        }

        public ActionResult DangXuat(string strURL)
        {
            Session["TaiKhoan"] = null;
            Session["GioHang"] = null;
            FormsAuthentication.SignOut();
            return Redirect(strURL);
        }
    }
}