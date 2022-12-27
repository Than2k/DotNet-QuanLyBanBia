using QuanLyBanBia.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanBia.View
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Kiểm tra đăng nhập
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string Email = txtEmail.Text;
            string Password = txtPassword.Text;
            var rs = NhanVienService.KtDn(Email, Password);
            //if Đăng nhập thành công mới cho vào sử dụng ứng dụng 
            if( rs != null)
            {
                var f = new FrmMain(rs);
                this.Visible = false;
                f.ShowDialog();
               
            }
            else
            {
                MessageBox.Show("Tên đăng nhập không đúng!");
               
            }
            
            
        }
    }
}
