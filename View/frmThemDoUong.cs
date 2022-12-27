using QuanLyBanBia.Service;
using QuanLyBanBia.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanBia
{
    public partial class frmThemDoUong : Form
    {
        public DoUongViewModel DoUong = new DoUongViewModel();
        public frmThemDoUong(DoUongViewModel DU = null)
        {
            InitializeComponent();
            if (DU != null)
            {
                DoUong.Id = DU.Id;
                DoUong.Ten = DU.Ten;
                DoUong.Gia = DU.Gia;
                DoUong.SoLuong = DU.SoLuong;
                cbbDoUong.SelectedValue = DU.Id;
                cbbDoUong.Enabled = false;
                txtGiaBan.Text = DU.Gia.ToString();
                txtSoLuong.Value = DU.SoLuong;
            }
            NapDoUong();
        }
        public void NapDoUong()
        {
            List<DoUongViewModel> ls = DoUongService.GetAll();
            cbbDoUong.DataSource = ls;
            cbbDoUong.ValueMember = "Id";
            cbbDoUong.DisplayMember = "Ten";

        }


        private void frmThemDoUong_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cbbDoUong_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(cbbDoUong.SelectedValue.ToString());
                DoUongViewModel ls = DoUongService.GetById(id);

                txtGiaBan.Text = ls.Gia.ToString();
                DoUong.Id = id;
            }
            catch (Exception)
            {

            }

        }



        private void btnXacNhan_Click(object sender, EventArgs e)
        {



            DoUong.Gia = long.Parse(txtGiaBan.Text);
            DoUong.SoLuong = (int)txtSoLuong.Value;
            DialogResult = DialogResult.OK;//để biết họ nhấn nut ok
        }


    }
}
