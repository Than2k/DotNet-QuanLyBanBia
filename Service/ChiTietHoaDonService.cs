using QuanLyBanBia.Model;
using QuanLyBanBia.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanBia.Service
{
    public class ChiTietHoaDonService
    {
        public static void Add(ChiTietHoaDon cthd)
        {
            var db = new AppDBContext();
            int Count = db.ChiTietHoaDons.Where(e=> e.maDoUong == cthd.maDoUong && e.maHD == cthd.maHD).Count();               
            if(Count == 0)
            {
               
                db.ChiTietHoaDons.Add(cthd);
                db.SaveChanges();
            }
            else
            {
                var result = db.ChiTietHoaDons.Where(e => e.maDoUong == cthd.maDoUong).FirstOrDefault();
                result.soLuongMua += cthd.soLuongMua;                              
                db.SaveChanges();
            }
            

        }
        public static int Update(ChiTietHoaDon CtHd)
        {
            var db = new AppDBContext();
            var rs = db.ChiTietHoaDons.Where(e => e.ID == CtHd.ID).FirstOrDefault();
            rs.soLuongMua = CtHd.soLuongMua;
            return db.SaveChanges();
        }
        /// <summary>
        /// lấy chi tieests hóa đơn theo mã hóa đơn
        /// </summary>
        /// <param name="MaHD"></param>
        /// <returns></returns>
        public static List<ChiTietHoaDonViewModel> GetByHD(int MaHD)
        {
            var db = new AppDBContext();
            var rs = db.ChiTietHoaDons.Where(e => e.maHD == MaHD).Select(e => new ChiTietHoaDonViewModel
            {
                Id = e.ID,
                MaHD=e.maHD,
                SoLuong =e.soLuongMua,
                Gia = e.gia,
                MaDoUong = e.maDoUong,
            }).ToList();
            return rs;
        }
        /// <summary>
        /// Tính tiền nước trong hóa đơn
        /// </summary>
        /// <param name="MaHD"></param>
        /// <returns></returns>
        public static long TinhTienNuoc(int MaHD)
        {
            long sum = 0;
            foreach(var item in GetByHD(MaHD))
            {
                sum += item.ThanhTien;
            }
            return sum;
        }
        public static int Delete(string TenDoUong)
        {
            var db = new AppDBContext();
            var DoUong = db.DoUongs.Where(e => e.ten == TenDoUong).FirstOrDefault();
            if(DoUong != null)
            {
                var rs = db.ChiTietHoaDons.Where(e => e.maDoUong == DoUong.ID).FirstOrDefault();
                db.ChiTietHoaDons.Remove(rs);
                return db.SaveChanges();
            }
            return 0;
        }

       
    }
}
