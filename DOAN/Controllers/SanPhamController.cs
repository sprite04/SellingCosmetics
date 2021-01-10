using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOAN.Models;
using PagedList;



namespace DOAN.Controllers
{
    public class SanPhamController : Controller
    {
        TMDTDbContext db = new TMDTDbContext();
        // GET: SanPham
        public ActionResult Index(int ?id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(x => x.IdSP == id);
            if(sp==null)
            {
                return HttpNotFound();
            }    
            return View(sp);
        }

      
        public ActionResult SanPhamTheoThuongHieu(int? page, int ?idTH, int idLoai=0)
        {
            ViewBag.IdTH = idTH;
            ViewBag.IdLoai = idLoai;
            if (idTH == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            IEnumerable<SANPHAM> listSP;
            if (idLoai == 0)
            {
                listSP = db.SANPHAMs.Where(x => x.IdTH == idTH);
            }
            else
            {
                listSP = db.SANPHAMs.Where(x => x.IdLoaiSP == idLoai && x.IdTH == idTH);
            }
            ViewBag.Loai = null;
            if(idLoai!=0)
            {
                ViewBag.Loai= db.LOAISANPHAMs.SingleOrDefault(x => x.IdLoaiSP == idLoai);
            }    
            ViewBag.TH = db.THUONGHIEUx.SingleOrDefault(x => x.IdTH == idTH);


            //So san pham tren 1 trang
            int PageSize = 8;
            //So trang hien tai
            int PageNumber = (page ?? 1);
            ViewBag.IdTH = idTH;
            ViewBag.IdLoai = idLoai;
            return View(listSP.OrderBy(x=>x.IdSP).ToPagedList(PageNumber,PageSize));
        }

      
        public ActionResult SanPhamTheoLoai(int? page, int? idLoai,int idTH=0)
        {
            ViewBag.IdTH = idTH;
            ViewBag.IdLoai = idLoai;
            if (idLoai == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            IEnumerable<SANPHAM> listSP;
            if (idTH==0)
            {
                listSP = db.SANPHAMs.Where(x => x.IdLoaiSP == idLoai);
            }
            else
            {
                listSP = db.SANPHAMs.Where(x => x.IdLoaiSP == idLoai&& x.IdTH==idTH);
            }
            ViewBag.TH = null;
            if(idTH!=0)
            {
                ViewBag.TH = db.THUONGHIEUx.SingleOrDefault(x => x.IdTH == idTH);
            }    
            
            ViewBag.Loai = db.LOAISANPHAMs.SingleOrDefault(x => x.IdLoaiSP == idLoai);
            //So san pham tren 1 trang
            int PageSize = 8;
            //So trang hien tai
            int PageNumber = (page ?? 1);
            ViewBag.IdTH = idTH;
            ViewBag.IdLoai = idLoai;
            return View(listSP.OrderBy(x => x.IdSP).ToPagedList(PageNumber, PageSize));
        }

        [ChildActionOnly]
        public ActionResult BrandsPartial(LOAISANPHAM loai)
        {
            ViewBag.Loai = loai;
            var listTH = db.THUONGHIEUx;
            return PartialView(listTH);
        }

        [ChildActionOnly]
        public ActionResult TypePartial(THUONGHIEU thuonghieu)
        {
            ViewBag.TH = thuonghieu;
            var listLoai = db.LOAISANPHAMs;
            return PartialView(listLoai);
        }

        [ChildActionOnly]
        public ActionResult DanhMucSP(IEnumerable<SANPHAM> listSP)
        {
            return PartialView(listSP);
        }

        
    }
}