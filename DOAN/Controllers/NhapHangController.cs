using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}