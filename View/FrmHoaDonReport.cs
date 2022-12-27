using Microsoft.Reporting.WinForms;
using QuanLyBanBia.Service;
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

namespace QuanLyBanBia.View
{
    public partial class FrmHoaDonReport : Form
    {
        HoaDonReport HDReport ;
        public FrmHoaDonReport(HoaDonReport hdReport )
        {
            InitializeComponent();
            this.HDReport = hdReport;
        }
        private bool CheckExitsParameter(ReportParameterInfoCollection pairn,string parameter)
        {
            foreach(ReportParameterInfo c in pairn)
            {
                if(c.Name == parameter)
                {
                    return true;
                }
            }
            return false;
        }

        private void FrmHoaDonReport_Load(object sender, EventArgs e)
        {
            
            var listMenu =  MenuService.getHoaDonByBan(HDReport.MaBan);
            List <ChiTietHoaDonRpModel > listReport= new List<ChiTietHoaDonRpModel>();
            ChiTietHoaDonRpModel tam;
            foreach ( var c in listMenu)
            {
                tam = new ChiTietHoaDonRpModel();
                tam.Ten = c.TenDoUong;
                tam.Gia = c.Gia;
                tam.SoLuong = c.SoLuong;
                tam.ThanhTien = (c.SoLuong * c.Gia);
                listReport.Add(tam);
            }
            reportViewer1.LocalReport.ReportPath = @"D:\DotNet\HelloForm\QuanLyBanBia\HoaDonReport.rdlc";
            ReportParameterInfoCollection parainfor = reportViewer1.LocalReport.GetParameters();
            ReportParameterCollection paras = new ReportParameterCollection();
            ReportParameter para;
            if (this.CheckExitsParameter(parainfor, "gio_vao"))
            {
                para = new ReportParameter("gio_vao", HDReport.GioVao.ToString("dd/MM/yyyy HH:mm"));
                paras.Add(para);
            }
            if (this.CheckExitsParameter(parainfor, "gio_ra"))
            {
                para = new ReportParameter("gio_ra", HDReport.GioRa?.ToString("dd/MM/yyyy HH:mm"));
                paras.Add(para);
            }
            if (this.CheckExitsParameter(parainfor, "tien_nuoc"))
            {
                para = new ReportParameter("tien_nuoc", string.Format(new CultureInfo("vi-VN"), "{0:#,##}", HDReport.TienNuoc) + "Đ" );
                paras.Add(para);
            }
            if (this.CheckExitsParameter(parainfor, "tien_gio"))
            {
                para = new ReportParameter("tien_gio", string.Format(new CultureInfo("vi-VN"), "{0:#,##}", HDReport.TienGio) + "Đ");
                paras.Add(para);
            }
            if (this.CheckExitsParameter(parainfor, "tong_tien"))
            {
                para = new ReportParameter("tong_tien", string.Format(new CultureInfo("vi-VN"), "{0:#,##}", HDReport.TienGio+ HDReport.TienNuoc) + "Đ");
                paras.Add(para);
            }
            if (this.CheckExitsParameter(parainfor, "ban"))
            {
                para = new ReportParameter("ban", HDReport.MaBan.ToString());
                paras.Add(para);
            }
            if (this.CheckExitsParameter(parainfor, "ma_hd"))
            {
                para = new ReportParameter("ma_hd", HDReport.MaHd.ToString());
                paras.Add(para);
            }
            if (this.CheckExitsParameter(parainfor, "gio_choi"))
            {
                para = new ReportParameter("gio_choi", $"{ HDReport.GioChoi / 60 } giờ { HDReport.GioChoi % 60}phút ");
                paras.Add(para);
            }
            if (this.CheckExitsParameter(parainfor, "nhan_vien"))
            {
                para = new ReportParameter("nhan_vien", HDReport.TenNhanVien.ToString());
                paras.Add(para);
            }


            //trỏ tới file DonReport.rdlc
            reportViewer1.LocalReport.ReportPath = "HoaDonReport.rdlc";
            //truyền dataset vào dataSource 
            var soure = new ReportDataSource("DataSet1", listReport);
            //clear dữ liệu củ đi
            reportViewer1.LocalReport.DataSources.Clear();
            
            // thêm datasoure vào reportViewer1
            reportViewer1.LocalReport.DataSources.Add(soure);
            //thêm paramater vào reportViewer1
            reportViewer1.LocalReport.SetParameters(paras);
            this.reportViewer1.RefreshReport();
           
        }
        
    }
}
