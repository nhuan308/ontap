using cau1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace cau1.Controllers
{
    public class DefaultController : ApiController
    {

        private Model1 db = new Model1();
        [HttpGet]
        [Route("api/sv/ds")]
        public List<SinhVien> ds()
        {
            return db.SinhViens.ToList();
        }
        [HttpGet]
        [Route("api/sv/timnv")]
        public SinhVien timsv(int MaSV)
        {
            return db.SinhViens.SingleOrDefault(nv => nv.MaSV == MaSV);
        }
        [HttpPost]
        [Route("api/sv/them")]
        public bool them([FromBody] SinhVien sv)
        {
            try
            {
                db.SinhViens.Add(sv);
                db.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }

        }
        [HttpPut]
        [Route("api/sv/sua")]
        public bool sua([FromBody] SinhVien sv)
        {
            try
            {
                SinhVien n = db.SinhViens.SingleOrDefault(x => x.MaSV == sv.MaSV);
                n.MaSV = sv.MaSV;
                n.TenSV = sv.TenSV;
                n.HeSoLuong = sv.HeSoLuong;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [HttpDelete]
        [Route("api/sv/xoa")]
        public bool xoa(int MaSV)
        {
            SinhVien n = db.SinhViens.SingleOrDefault(s => s.MaSV == MaSV);
            if (n != null)
            {
                db.SinhViens.Remove(n);
                db.SaveChanges();
                return true;

            }
            return false;
        }

    }
}
