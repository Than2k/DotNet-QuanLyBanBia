using QuanLyBanBia.Model;
using QuanLyBanBia.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanBia.Service
{
    public class NhanVienService
    {
        public static NhanVienViewModel KtDn(string Email, string Password)
        {
            var db = new AppDBContext();
            var rs = db.NhanViens.Where(e => e.email == Email && e.matkhau == Password).Select(e=> new NhanVienViewModel { 
                Id = e.ID,
                HoTen = e.hoten,
                Email = e.email,
                MaKhau= e.matkhau,
                ChucVu =e.chucvu,
                SDT =e.sdt,
            }).FirstOrDefault() as NhanVienViewModel;
            return rs;
        }

    }
}
