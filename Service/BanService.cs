using QuanLyBanBia.Model;
using QuanLyBanBia.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanBia.Service
{
    public class BanService
    {
        public static List<BanViewModel> GetBan()
        {
            var db = new AppDBContext();
            var rs = db.Bans.Select(e => new BanViewModel
            {
                MaBan = e.maBan,
                TenBan = e.tenBan,
                TrangThai = e.trangthai,
            }).ToList();
            return rs;
        }
        public static void SetStatusBan(int IdBan ,bool tt)
        {
            var db = new AppDBContext();
            var rs = db.Bans.Where(e => e.maBan == IdBan).FirstOrDefault();
            rs.trangthai = tt;
            db.SaveChanges();
        }
    }
}
