using QuanLyBanBia.Model;
using QuanLyBanBia.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanBia.Service
{
    class DoUongService
    {
        
        public static List<DoUongViewModel> GetAll()
        {
            var db = new AppDBContext();
            var rs = db.DoUongs.Select(e => new DoUongViewModel
            {
                Id = e.ID,
                Ten = e.ten,
                Gia = e.gia,
                SoLuong = e.soLuong,
            }).ToList();
            return rs;
        }
        public static DoUongViewModel GetById(int id)
        {
            var db = new AppDBContext();
            
            var rs = db.DoUongs.Where(e => e.ID == id).Select(e => new DoUongViewModel
            {
                
                Id = e.ID,
                Ten = e.ten,
                Gia = e.gia,
                SoLuong = e.soLuong,
            }).FirstOrDefault();
            return rs;
        }
        public static DoUongViewModel GetByTenDoUong(string TenDoUong)
        {
            var db = new AppDBContext();

            var rs = db.DoUongs.Where(e => e.ten == TenDoUong).Select(e => new DoUongViewModel
            {

                Id = e.ID,
                Ten = e.ten,
                Gia = e.gia,
                SoLuong = e.soLuong,
            }).FirstOrDefault();
            return rs;
        }

    }
}
