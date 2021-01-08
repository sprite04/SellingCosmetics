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
        public ActionResult Index()
        {
            var list = db.HOADONs.Where(x => x.TinhTrang != 10);

            return View(list);
        }

        public ActionResult ChiTiet(int id)
        {
            var hoadon = db.HOADONs.Find(id);
            if (hoadon == null)
                return HttpNotFound();
            var listCT = db.CHITIETHDs.Where(x => x.IdHD == id);

            ViewBag.HoaDon = hoadon;
            ViewBag.ChiTiet = listCT;
            return View();
        }
    }
}