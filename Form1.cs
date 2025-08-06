using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLKTX_ConfigTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Cấu hình kết nối - QLKTX";
            LoadDefaultConnectionString();

            lblGoiY.Text = "📌 Gợi ý:\n- <TEN_SERVER> = tên SQL Server bạn đang dùng\n  (ví dụ: . hoặc localhost hoặc TENSERVER\\SQLEXPRESS)";
        }

        private void LoadDefaultConnectionString()
        {
            txtConnStr.Text =
                "metadata=res://*/TestModel.csdl|res://*/TestModel.ssdl|res://*/TestModel.msl;" +
                "provider=System.Data.SqlClient;" +
                "provider connection string=\"data source=<TEN_SERVER>;" +
                "initial catalog=KTX_Database;" +
                "integrated security=True;" +
                "TrustServerCertificate=True;" +
                "MultipleActiveResultSets=True;" +
                "App=EntityFramework\"";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string connStr = txtConnStr.Text.Trim();
            string folder = txtFolder.Text.Trim();

            if (string.IsNullOrEmpty(connStr))
            {
                MessageBox.Show("Vui lòng nhập chuỗi kết nối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(folder) || !Directory.Exists(folder))
            {
                MessageBox.Show("Vui lòng chọn thư mục hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string filePath = Path.Combine(folder, "config.txt");
                File.WriteAllText(filePath, connStr);
                MessageBox.Show($"Đã lưu cấu hình thành công!\n{filePath}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu cấu hình:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Chọn thư mục để lưu file cấu hình";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtFolder.Text = fbd.SelectedPath;
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
