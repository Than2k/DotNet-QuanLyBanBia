using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanBia.ViewModel
{
    public class MenuViewModel
    {
        public int maCT { get; set; }
        public string TenBan { get; set; }
        public string TenDoUong { get; set; }
        public long Gia { get; set; }
        public int SoLuong { get; set; }
        public DateTime GioVao{ get; set; }
        public DateTime? GioRa { get; set; }
        public string TenNV { set; get; }
    }
}
