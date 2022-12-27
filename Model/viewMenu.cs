namespace QuanLyBanBia.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("viewMenu")]
    public partial class viewMenu
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int soLuongMua { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long giaBan { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime gioVao { get; set; }

        public DateTime? gioRa { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string hoten { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int maBan { get; set; }

        public bool? trangthaiBan { get; set; }

        [StringLength(50)]
        public string tenBan { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(50)]
        public string tenDoUong { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int maHD { get; set; }

        public bool? trangthaiHD { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int maCT { get; set; }
    }
}
