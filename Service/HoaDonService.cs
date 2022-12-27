using QuanLyBanBia.Model;
using QuanLyBanBia.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanBia.Service
{
    public class HoaDonService
    {
        public static HoaDonViewModel GetHDByBan(int MaBan)
        {
            var db = new AppDBContext();
            var rs = db.HoaDons.Where(e => e.maBan == MaBan && e.trangthai == false).Select(e => new HoaDonViewModel
            {
                Id = e.ID,
                MaBan =e.maBan,
                MaNV = e.maNv,
                GioVao = e.gioVao,
                GioRa = e.gioRa,
                TrangThai = e.trangthai,

            }).FirstOrDefault();
            return rs;
        }
        public static int Add(HoaDon hd)
        {
            var db = new AppDBContext();
            db.HoaDons.Add(hd);
            return db.SaveChanges();
            
        }
        public static int UpdateGioRA(int MaHD)
        {
            var db = new AppDBContext();
            var rs = db.HoaDons.Where(e => e.ID == MaHD).FirstOrDefault();
            rs.gioRa = DateTime.Now;
            return db.SaveChanges();
        }
        /// <summary>
        /// set trạn thái hóa đơn thanh toán
        /// </summary>
        /// <param name="MaHD"></param>
        /// <returns></returns>
        public static int SetStatus(int MaHD, bool tt )
        {
            var db = new AppDBContext();
            var rs = db.HoaDons.Where(e => e.ID == MaHD).FirstOrDefault();
            rs.trangthai = tt;
            return db.SaveChanges();
        }
        /// <summary>
        /// Tính giờ chơi của khách
        /// </summary>
        /// <param name="IdBan"></param>
        /// <returns></returns>
        public static double TinhTienGio(int IdBan)
        {
            var db = new AppDBContext();
            var Ban = db.Bans.Where(e => e.maBan == IdBan).FirstOrDefault();
            return Ban.gia * Math.Round((double)GetGioChoi(IdBan) / 60, 2);
        }
        /// <summary>
        /// Lấy số giờ chơi của khách
        /// </summary>
        /// <param name="IdBan"></param>
        /// <returns></returns>
        public static long GetGioChoi(int IdBan)
        {
            var db = new AppDBContext();
            var HoaDon = db.HoaDons.Where(e => e.maBan == IdBan && e.trangthai == false).FirstOrDefault();          
            DateTime GioRa = HoaDon.gioRa ?? DateTime.Now;
            TimeSpan temp = GioRa - HoaDon.gioVao;
            long i = (long)temp.TotalMinutes;
            return i;
        }
    }
}
