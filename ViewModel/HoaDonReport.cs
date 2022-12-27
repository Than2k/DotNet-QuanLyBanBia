using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanBia.ViewModel
{
    public class HoaDonReport
    {
        public int MaBan { get; set; }
        public DateTime GioVao { get; set; }
        public DateTime? GioRa { get; set; }
        public long TienNuoc  {get;set;}
        public double TienGio { get; set; }
        public int MaHd { get; set; }
        public string TenNhanVien { get; set; }
        public long GioChoi { get; set; }
        
    }
}
