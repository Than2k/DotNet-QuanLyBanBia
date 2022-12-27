using QuanLyBanBia.Service;
using QuanLyBanBia.View;
using QuanLyBanBia.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanBia
{
    
    public partial class FrmMain : Form
    {
        NhanVienViewModel NhanVien { get; set; }
        public BanViewModel Ban { get; set; }
        public long TienNuoc { get; set; }
        public double TienGio { get; set; }
        public FrmMain( NhanVienViewModel nv)
        {
           
            InitializeComponent();
            LoadBan();
            SetStatus(false);
            btnBatDau.Enabled = false;
            this.NhanVien = nv;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        public void AnChucNangBan()
        {
            btnBatDau.Enabled = false;
            btnKetThuc.Enabled = false;
            btnThemDoUong.Enabled = false;
            btnSuaDoUong.Enabled = false;
        }
        public void ShowChucNangBan()
        {
            btnBatDau.Enabled = true;
            btnKetThuc.Enabled = true;
            btnThemDoUong.Enabled = true;
            btnSuaDoUong.Enabled = true;
        }
        public void LoadBan()
        {
            flowLayoutPanel1.Controls.Clear();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            List<BanViewModel> list =BanService.GetBan();
            foreach(var ban in list)
            {
                Button bt = new Button();

                bt.Width = 90;
                bt.Height = 80;
                bt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                bt.Image = global::QuanLyBanBia.Properties.Resources.icons8_table_30;
                bt.ImageAlign= System.Drawing.ContentAlignment.TopCenter;
                bt.BackColor = (ban.TrangThai == false)? System.Drawing.Color.Aqua: System.Drawing.Color.Gray;
                bt.Text = ban.TenBan + "\n" + ((ban.TrangThai== false)?"bàn trống":"có người");
                bt.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
                flowLayoutPanel1.Controls.Add(bt);
                bt.Tag = ban;
                bt.Click += new EventHandler(OnClick);
            }
        }
        public void NapMenu(int idBan)
        {
            
            listViewMenu.Items.Clear();
            TienNuoc = 0;
            List<MenuViewModel> listMenu = MenuService.getHoaDonByBan(idBan);
            
            labBan.Text = $"Bàn số {idBan}";



            long TongTienNuoc = 0;
            foreach (var i in listMenu)
            {
                
                TongTienNuoc += i.Gia * i.SoLuong;
                ListViewItem item = new ListViewItem(i.TenDoUong);
                item.Tag = i.maCT;
                item.SubItems.Add(i.Gia.ToString());
                item.SubItems.Add(i.SoLuong.ToString());
                item.SubItems.Add((i.Gia * i.SoLuong).ToString());
                item.SubItems.Add(i.maCT.ToString());

                listViewMenu.Items.Add(item);
                

            }

            TienNuoc = TongTienNuoc;
            //kiểm tra hoa đơn đã keert thúc chưa 
            var HoaDon = HoaDonService.GetHDByBan(Ban.MaBan);
            if (HoaDon != null && HoaDon.GioRa != null)
            {

                SetStatus(false);
                btnBatDau.Enabled = false;
                btnInHD.Enabled = true;
                btnThanToan.Enabled = true;
                ShowThongTin();
            }
            else
            {
                btnThanToan.Enabled = false;
                btnInHD.Enabled = false;
                AnThongTin();
            }
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        public void SetStatus(bool tt)
        {
            if(tt == true)
            {
                btnThemDoUong.Enabled = true;
                btnSuaDoUong.Enabled = true;
                btnKetThuc.Enabled = true;
                btnBatDau.Enabled = false;
               
            }
            else
            {
                btnInHD.Enabled = false;
                btnThanToan.Enabled = false;
                btnThemDoUong.Enabled = false;
                btnSuaDoUong.Enabled = false;
                btnKetThuc.Enabled = false;
                btnBatDau.Enabled = true;
            }
        }
        public void ShowThongTin()
        {
            AnChucNangBan();
            HoaDonViewModel HD = HoaDonService.GetHDByBan(Ban.MaBan);
            int MaHD = HD.Id;
            //lấy gio chơi
            long GioChoi = HoaDonService.GetGioChoi(Ban.MaBan);
            // set giờ ra
            btnInHD.Enabled = true;
            btnThanToan.Enabled = true;
            TienGio = HoaDonService.TinhTienGio(Ban.MaBan);
            labThoiGian.Text = $"Thòi gian vào: {HD.GioVao} Thời gian ra:{HD.GioRa}( {GioChoi / 60} giờ {GioChoi % 60} phút )";
            labTienGio.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##}", TienGio) +"Đ";
            labTienNuoc.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##}", TienNuoc)+ "Đ";
            labTongTien.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##}", TienGio+TienNuoc)+"Đ";
        }
        public void AnThongTin()
        {
            var HD = HoaDonService.GetHDByBan(Ban.MaBan);
            labThoiGian.Text = (HD != null)?$"Thời gian vào: {HD.GioVao}":"Thời gian: Chưa xác định";
            labTienGio.Text = "Chưa xác định";
            string TienNuocFomat = (TienNuoc>0)? string.Format(new CultureInfo("vi-VN"), "{0:#,##}", TienNuoc):"0";
            labTienNuoc.Text = (Ban.TrangThai == true)?$"{TienNuocFomat}Đ":"Chưa xác định";
            labTongTien.Text = "Chưa xác định";
        }
        public void OnClick(object sender, EventArgs e)
        {
            var ban = ((Button)sender).Tag as BanViewModel;
            this.Ban = ban;
            SetStatus(ban.TrangThai);
            NapMenu(ban.MaBan);
        }
        #region sự kiện
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void flowLayoutPanel1_Click(object sender, EventArgs e)
        {
           
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        #endregion
        private void button2_Click(object sender, EventArgs e)
        {

        }
        #region Them chi tiết hóa đơn
        /// <summary>
        /// Thêm chi tiết hóa đơn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnThemCTHD_Click(object sender, EventArgs e)
        {
            
            var f = new frmThemDoUong();
            var rs= f.ShowDialog();
            if (rs == DialogResult.OK)
            {
                
                var chd = new Model.ChiTietHoaDon
                {
                    maHD = HoaDonService.GetHDByBan(Ban.MaBan).Id,
                    maDoUong = f.DoUong.Id,
                    soLuongMua = f.DoUong.SoLuong,
                    gia = f.DoUong.Gia,

                };
                ChiTietHoaDonService.Add(chd);
                AnThongTin();
                NapMenu(Ban.MaBan);                                             
            }
        }
        #endregion
        private void listViewMenu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Tạo hóa đơn khi bàn trống
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBatDau_Click(object sender, EventArgs e)
        {
            var hd = new Model.HoaDon
            {
                maNv = NhanVien.Id,
                maBan = Ban.MaBan,
                trangthai =false,
                gioVao = DateTime.Now,
            };
            BanService.SetStatusBan(Ban.MaBan, true);
            HoaDonService.Add(hd);
            LoadBan();
            NapMenu(Ban.MaBan);
            SetStatus(true);
        }
        /// <summary>
        /// khách muons thanh toán 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnKetThuc_Click(object sender, EventArgs e)
        {
            HoaDonViewModel HD = HoaDonService.GetHDByBan(Ban.MaBan);
            int MaHD = HD.Id;
            // set giờ ra
            HoaDonService.UpdateGioRA(MaHD);
            ShowThongTin();

        }
        
        private void btnSuaDoUong_Click(object sender, EventArgs e)
        {
            // lấy đồ uống theo select
            try
            {
                var DU = DoUongService.GetByTenDoUong(listViewMenu.SelectedItems[0].SubItems[0].Text);
                if (DU != null)
                {
                    DU.SoLuong = int.Parse(listViewMenu.SelectedItems[0].SubItems[2].Text);
                    frmThemDoUong f = new frmThemDoUong(DU);
                    var rs = f.ShowDialog();
                    if (rs == DialogResult.OK)
                    {

                        var chd = new Model.ChiTietHoaDon
                        {
                            ID = int.Parse(listViewMenu.SelectedItems[0].SubItems[4].Text),
                            maHD = HoaDonService.GetHDByBan(Ban.MaBan).Id,
                            maDoUong = f.DoUong.Id,
                            soLuongMua = f.DoUong.SoLuong,
                            gia = f.DoUong.Gia,

                        };
                        ChiTietHoaDonService.Update(chd);
                        AnThongTin();
                        NapMenu(Ban.MaBan);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Bạn chưa chọn Đồ uống để sửa!");
            }
            
        }
        /// <summary>
        /// Thanh toán lưu hóa đơn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnThanToan_Click(object sender, EventArgs e)
        {
            HoaDonViewModel HD = HoaDonService.GetHDByBan(Ban.MaBan);
            BanService.SetStatusBan(Ban.MaBan, false);
            HoaDonService.SetStatus(HD.Id, true);
            LoadBan();
            btnBatDau.Enabled = true;
            NapMenu(Ban.MaBan);
        }

        private void listViewMenu_DoubleClick(object sender, EventArgs e)
        {
            
            var rs = MessageBox.Show("Bạn có chắc muốn xóa không ?", "Chú ý", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if(rs == DialogResult.OK)
            {
                ChiTietHoaDonService.Delete(listViewMenu.SelectedItems[0].Text);
                NapMenu(Ban.MaBan);
            }
           
        }

        private void labTienGio_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnInHD_Click(object sender, EventArgs e)
        {
            HoaDonReport HdReport = new HoaDonReport();
            HdReport.TienGio = TienGio;
            HdReport.TienNuoc = TienNuoc;
            HdReport.MaBan = Ban.MaBan;
            var hd = HoaDonService.GetHDByBan(Ban.MaBan);
            HdReport.GioVao = hd.GioVao;
            HdReport.GioRa = hd.GioRa;
            HdReport.TenNhanVien = NhanVien.HoTen;
            HdReport.MaHd = hd.Id;
            HdReport.GioChoi = HoaDonService.GetGioChoi(Ban.MaBan);
            var f = new FrmHoaDonReport(HdReport);
            f.ShowDialog();
        }
    }
}
