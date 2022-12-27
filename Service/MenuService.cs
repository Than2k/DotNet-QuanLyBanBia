using QuanLyBanBia.Model;
using QuanLyBanBia.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanBia.Service
{
    class MenuService
    {
        public static List<MenuViewModel> getHoaDonByBan(int id)
        {
            var db = new AppDBContext();
            var rs = db.viewMenus.Where(e => e.maBan == id && e.trangthaiHD == false ).Select(e => new MenuViewModel
            {
                maCT = e.maCT,
                TenBan = e.tenBan,
                SoLuong = e.soLuongMua,
                Gia = e.giaBan,
                GioRa = e.gioRa,
                GioVao = e.gioVao,
                TenDoUong = e.tenDoUong,
                TenNV = e.hoten,
               
            }).ToList();
            return rs;
        }
    }
}
