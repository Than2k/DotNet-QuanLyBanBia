using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanBia.ViewModel
{
    public class HoaDonViewModel
    {
        public int Id { get; set; }
        public int MaNV { get; set; }
        public DateTime GioVao { get; set; }
        public DateTime? GioRa { get; set; }
        public bool? TrangThai { get; set; }
        public int MaBan { get; set; }
    }
}
