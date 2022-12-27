using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanBia.ViewModel
{
    public class ChiTietHoaDonViewModel
    {
        public int Id { get; set; }
        public int MaHD { get; set; }
        public int MaDoUong { get; set; }
        public int SoLuong { get; set; }
        public long Gia { get; set; }
        public long ThanhTien
        {
            get
            {
                return Gia * SoLuong;
            }
        }
    }
}
