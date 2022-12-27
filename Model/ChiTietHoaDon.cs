namespace QuanLyBanBia.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietHoaDon")]
    public partial class ChiTietHoaDon
    {
        public int ID { get; set; }

        public int maHD { get; set; }

        public int soLuongMua { get; set; }

        public int maDoUong { get; set; }

        public long gia { get; set; }

        public virtual DoUong DoUong { get; set; }

        public virtual HoaDon HoaDon { get; set; }
    }
}
