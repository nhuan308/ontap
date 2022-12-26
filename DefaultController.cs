using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CheckTest.Models;

namespace CheckTest.Controllers
{
    public class DefaultController : ApiController
    {
        private NhanVien db = new NhanVien();
        [HttpGet]
        [Route ("api/nv/ds")]
        public List<NV> ds()
        {
            return db.NVs.ToList();
        }
        [HttpGet]
        [Route("api/nv/timnv")]
        public NV timnv(string MaNV)
        {
            return db.NVs.SingleOrDefault(nv => nv.MaNV == MaNV);
        }
        [HttpPost]
        [Route ("api/nv/them")]
        public bool them([FromBody] NV nv)
        {
            try
            {
                db.NVs.Add(nv);
                db.SaveChanges();
                return true;

            }
            catch {
                return false;
            }
            
        }
        [HttpPut]
        [Route ("api/nv/sua")]
        public bool sua([FromBody] NV nv)
        {
            try
            {
                NV n = db.NVs.SingleOrDefault(x => x.MaNV == nv.MaNV);
                n.MaNV = nv.MaNV;
                n.TenNV = nv.TenNV;
                n.HSLuong = nv.HSLuong;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [HttpDelete]
        [Route("api/nv/xoa")]
        public bool xoa(string ma)
        {
            NV n = db.NVs.SingleOrDefault(s => s.MaNV == ma);
            if (n != null)
            {
                db.NVs.Remove(n);
                db.SaveChanges();
                return true;

            }
            return false;
        }

    }
}
