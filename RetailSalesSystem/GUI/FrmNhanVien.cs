
using System;
using System.Drawing;
using System.Windows.Forms;
using RetailSalesSystem.BUS;
using RetailSalesSystem.DTO;

namespace RetailSalesSystem.GUI
{
    public partial class FrmNhanVien : Form
    {
        private NhanVienBUS bus = new NhanVienBUS();

        private TextBox txtMaNV = new TextBox();
        private TextBox txtTenNV = new TextBox();
        private TextBox txtSoDienThoai = new TextBox();
        private TextBox txtChucVu = new TextBox();
        private TextBox txtTimKiem = new TextBox();

        private DataGridView dgvNhanVien = new DataGridView();

        private Button btnThem = new Button();
        private Button btnSua = new Button();
        private Button btnXoa = new Button();
        private Button btnLamMoi = new Button();
        private Button btnTimKiem = new Button();

        public FrmNhanVien()
        {
            InitializeComponent();
            TaoGiaoDien();
            LoadDanhSach();
        }

        private void TaoGiaoDien()
        {
            this.Text = "QUẢN LÝ NHÂN VIÊN";
            this.Size = new Size(1000, 650);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblTieuDe = new Label();
            lblTieuDe.Text = "QUẢN LÝ NHÂN VIÊN";
            lblTieuDe.AutoSize = true;
            lblTieuDe.Location = new Point(400, 20);
            this.Controls.Add(lblTieuDe);

            TaoLabel("Mã nhân viên:", 50, 90);
            TaoLabel("Tên nhân viên:", 50, 140);
            TaoLabel("Số điện thoại:", 50, 190);
            TaoLabel("Chức vụ:", 50, 240);

            TaoTextBox(txtMaNV, 180, 85, true);
            TaoTextBox(txtTenNV, 180, 135);
            TaoTextBox(txtSoDienThoai, 180, 185);
            TaoTextBox(txtChucVu, 180, 235);

            TaoNut(btnThem, "Thêm", 450, 85);
            TaoNut(btnSua, "Sửa", 560, 85);
            TaoNut(btnXoa, "Xóa", 670, 85);
            TaoNut(btnLamMoi, "Làm mới", 780, 85);

            TaoLabel("Tìm kiếm:", 50, 300);

            txtTimKiem.Location = new Point(180, 295);
            txtTimKiem.Size = new Size(300, 25);
            this.Controls.Add(txtTimKiem);

            TaoNut(btnTimKiem, "Tìm kiếm", 500, 290);

            dgvNhanVien.Location = new Point(30, 350);
            dgvNhanVien.Size = new Size(920, 230);
            dgvNhanVien.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvNhanVien.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvNhanVien.MultiSelect = false;
            dgvNhanVien.ReadOnly = true;
            dgvNhanVien.AllowUserToAddRows = false;
            dgvNhanVien.AutoGenerateColumns = true;
            this.Controls.Add(dgvNhanVien);

            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            btnTimKiem.Click += btnTimKiem_Click;
            dgvNhanVien.CellClick += dgvNhanVien_CellClick;
        }

        private void TaoLabel(string text, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Location = new Point(x, y);
            lbl.Size = new Size(120, 25);
            this.Controls.Add(lbl);
        }

        private void TaoTextBox(TextBox txt, int x, int y,
            bool readOnly = false)
        {
            txt.Location = new Point(x, y);
            txt.Size = new Size(220, 25);
            txt.ReadOnly = readOnly;
            this.Controls.Add(txt);
        }

        private void TaoNut(Button btn, string text, int x, int y)
        {
            btn.Text = text;
            btn.Location = new Point(x, y);
            btn.Size = new Size(100, 35);
            this.Controls.Add(btn);
        }

        private void LoadDanhSach()
        {
            dgvNhanVien.DataSource = bus.GetAll();
            dgvNhanVien.ClearSelection();
            XoaTrang();
        }

        private void XoaTrang()
        {
            txtMaNV.Clear();
            txtTenNV.Clear();
            txtSoDienThoai.Clear();
            txtChucVu.Clear();
            dgvNhanVien.ClearSelection();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                NhanVienDTO nv = new NhanVienDTO();

                nv.TenNV = txtTenNV.Text;
                nv.SoDienThoai = txtSoDienThoai.Text;
                nv.ChucVu = txtChucVu.Text;

                if (bus.Insert(nv))
                {
                    MessageBox.Show("Thêm nhân viên thành công!");
                    LoadDanhSach();
                }
                else
                {
                    MessageBox.Show("Thêm nhân viên thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtMaNV.Text, out int maNV))
                {
                    MessageBox.Show("Vui lòng chọn nhân viên cần sửa!");
                    return;
                }

                NhanVienDTO nv = new NhanVienDTO();

                nv.MaNV = maNV;
                nv.TenNV = txtTenNV.Text;
                nv.SoDienThoai = txtSoDienThoai.Text;
                nv.ChucVu = txtChucVu.Text;

                if (bus.Update(nv))
                {
                    MessageBox.Show("Cập nhật nhân viên thành công!");
                    LoadDanhSach();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtMaNV.Text, out int maNV))
                {
                    MessageBox.Show("Vui lòng chọn nhân viên cần xóa!");
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa nhân viên này?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (bus.Delete(maNV))
                    {
                        MessageBox.Show("Xóa nhân viên thành công!");
                        LoadDanhSach();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            LoadDanhSach();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                dgvNhanVien.DataSource =
                    bus.Search(txtTimKiem.Text.Trim());

                XoaTrang();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }

        private void dgvNhanVien_CellClick(object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];

                txtMaNV.Text =
                    row.Cells["MaNV"].Value?.ToString();

                txtTenNV.Text =
                    row.Cells["TenNV"].Value?.ToString();

                txtSoDienThoai.Text =
                    row.Cells["SoDienThoai"].Value?.ToString();

                txtChucVu.Text =
                    row.Cells["ChucVu"].Value?.ToString();
            }
        }
    }
}
