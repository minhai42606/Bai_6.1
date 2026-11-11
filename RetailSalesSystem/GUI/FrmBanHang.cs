
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using RetailSalesSystem.BUS;
using RetailSalesSystem.DTO;

namespace RetailSalesSystem.GUI
{
    public partial class FrmBanHang : Form
    {
        private SanPhamBUS spBUS = new SanPhamBUS();
        private NhanVienBUS nvBUS = new NhanVienBUS();
        private KhachHangBUS khBUS = new KhachHangBUS();
        private HoaDonBUS hdBUS = new HoaDonBUS();

        private ComboBox cboNhanVien = new ComboBox();
        private ComboBox cboKhachHang = new ComboBox();
        private ComboBox cboSanPham = new ComboBox();

        private NumericUpDown numSoLuong = new NumericUpDown();

        private DataGridView dgvChiTiet = new DataGridView();

        private Label lblTongTien = new Label();

        private Button btnThemSP = new Button();
        private Button btnXoaSP = new Button();
        private Button btnThanhToan = new Button();
        private Button btnLamMoi = new Button();

        public FrmBanHang()
        {
            InitializeComponent();
            TaoGiaoDien();
            LoadDuLieu();
        }

        // Tạo giao diện
        private void TaoGiaoDien()
        {
            this.Text = "QUẢN LÝ BÁN HÀNG";
            this.Size = new Size(1050, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblTieuDe = new Label();
            lblTieuDe.Text = "QUẢN LÝ BÁN HÀNG";
            lblTieuDe.Font = new Font("Microsoft Sans Serif", 16);
            lblTieuDe.AutoSize = true;
            lblTieuDe.Location = new Point(400, 20);
            this.Controls.Add(lblTieuDe);

            TaoLabel("Nhân viên:", 40, 85);
            TaoLabel("Khách hàng:", 40, 130);
            TaoLabel("Sản phẩm:", 40, 190);
            TaoLabel("Số lượng:", 40, 235);

            TaoComboBox(cboNhanVien, 160, 80, 300);
            TaoComboBox(cboKhachHang, 160, 125, 300);
            TaoComboBox(cboSanPham, 160, 185, 300);

            numSoLuong.Location = new Point(160, 230);
            numSoLuong.Size = new Size(100, 25);
            numSoLuong.Minimum = 1;
            numSoLuong.Maximum = 100000;
            numSoLuong.Value = 1;
            this.Controls.Add(numSoLuong);

            TaoNut(btnThemSP, "Thêm sản phẩm", 500, 185, 130);
            TaoNut(btnXoaSP, "Xóa sản phẩm", 650, 185, 130);

            // Bảng chi tiết hóa đơn
            dgvChiTiet.Location = new Point(30, 290);
            dgvChiTiet.Size = new Size(970, 250);
            dgvChiTiet.AllowUserToAddRows = false;
            dgvChiTiet.ReadOnly = true;
            dgvChiTiet.MultiSelect = false;
            dgvChiTiet.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvChiTiet.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvChiTiet.Columns.Add("MaSP", "Mã sản phẩm");
            dgvChiTiet.Columns.Add("TenSP", "Tên sản phẩm");
            dgvChiTiet.Columns.Add("SoLuong", "Số lượng");
            dgvChiTiet.Columns.Add("DonGia", "Đơn giá");
            dgvChiTiet.Columns.Add("ThanhTien", "Thành tiền");

            dgvChiTiet.Columns["MaSP"].Visible = false;

            this.Controls.Add(dgvChiTiet);

            // Tổng tiền
            Label lblNhanTongTien = new Label();
            lblNhanTongTien.Text = "Tổng tiền:";
            lblNhanTongTien.AutoSize = true;
            lblNhanTongTien.Location = new Point(650, 565);
            this.Controls.Add(lblNhanTongTien);

            lblTongTien.Text = "0 VNĐ";
            lblTongTien.AutoSize = true;
            lblTongTien.Location = new Point(760, 565);
            this.Controls.Add(lblTongTien);

            // Các nút chức năng
            TaoNut(btnThanhToan, "Thanh toán", 650, 610, 130);
            TaoNut(btnLamMoi, "Làm mới", 800, 610, 130);

            btnThemSP.Click += btnThemSP_Click;
            btnXoaSP.Click += btnXoaSP_Click;
            btnThanhToan.Click += btnThanhToan_Click;
            btnLamMoi.Click += btnLamMoi_Click;
        }

        private void TaoLabel(string text, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Location = new Point(x, y);
            lbl.Size = new Size(110, 25);
            this.Controls.Add(lbl);
        }

        private void TaoComboBox(
            ComboBox cbo, int x, int y, int width)
        {
            cbo.Location = new Point(x, y);
            cbo.Size = new Size(width, 25);
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Controls.Add(cbo);
        }

        private void TaoNut(
            Button btn, string text, int x, int y, int width)
        {
            btn.Text = text;
            btn.Location = new Point(x, y);
            btn.Size = new Size(width, 35);
            this.Controls.Add(btn);
        }

        // Tải dữ liệu từ database
        private void LoadDuLieu()
        {
            try
            {
                // Tải nhân viên
                DataTable dtNhanVien = nvBUS.GetAll();

                cboNhanVien.DataSource = dtNhanVien;
                cboNhanVien.DisplayMember = "TenNV";
                cboNhanVien.ValueMember = "MaNV";

                // Tải khách hàng
                DataTable dtKhachHang = new DataTable();
                dtKhachHang.Columns.Add("MaKH", typeof(int));
                dtKhachHang.Columns.Add("TenKH", typeof(string));

                // Khách lẻ không có mã khách hàng
                dtKhachHang.Rows.Add(DBNull.Value, "Khách lẻ");

                foreach (KhachHangDTO kh in khBUS.GetAll())
                {
                    dtKhachHang.Rows.Add(kh.MaKH, kh.TenKH);
                }

                cboKhachHang.DataSource = dtKhachHang;
                cboKhachHang.DisplayMember = "TenKH";
                cboKhachHang.ValueMember = "MaKH";

                // Tải sản phẩm
                cboSanPham.DataSource = spBUS.GetAll();
                cboSanPham.DisplayMember = "TenSP";
                cboSanPham.ValueMember = "MaSP";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải dữ liệu!\n" + ex.Message,
                    "Lỗi");
            }
        }

        // Thêm sản phẩm vào hóa đơn
        private void btnThemSP_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboSanPham.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm!");
                    return;
                }

                DataRowView sp =
                    cboSanPham.SelectedItem as DataRowView;

                if (sp == null)
                    return;

                string maSP = sp["MaSP"].ToString();
                string tenSP = sp["TenSP"].ToString();

                decimal donGia = Convert.ToDecimal(sp["GiaBan"]);
                int tonKho = Convert.ToInt32(sp["SoLuongTon"]);
                int soLuong = Convert.ToInt32(numSoLuong.Value);

                if (soLuong > tonKho)
                {
                    MessageBox.Show("Số lượng tồn kho không đủ!");
                    return;
                }

                // Kiểm tra sản phẩm đã có trong hóa đơn chưa
                foreach (DataGridViewRow row in dgvChiTiet.Rows)
                {
                    if (row.Cells["MaSP"].Value.ToString() == maSP)
                    {
                        int soLuongCu =
                            Convert.ToInt32(row.Cells["SoLuong"].Value);

                        if (soLuongCu + soLuong > tonKho)
                        {
                            MessageBox.Show(
                                "Số lượng vượt quá tồn kho!");
                            return;
                        }

                        int soLuongMoi = soLuongCu + soLuong;

                        row.Cells["SoLuong"].Value = soLuongMoi;
                        row.Cells["ThanhTien"].Value =
                            soLuongMoi * donGia;

                        TinhTongTien();
                        return;
                    }
                }

                decimal thanhTien = soLuong * donGia;

                dgvChiTiet.Rows.Add(
                    maSP,
                    tenSP,
                    soLuong,
                    donGia,
                    thanhTien
                );

                TinhTongTien();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }

        // Xóa sản phẩm khỏi hóa đơn
        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!");
                return;
            }

            dgvChiTiet.Rows.RemoveAt(
                dgvChiTiet.SelectedRows[0].Index);

            TinhTongTien();
        }

        // Tính tổng tiền
        private void TinhTongTien()
        {
            decimal tongTien = 0;

            foreach (DataGridViewRow row in dgvChiTiet.Rows)
            {
                tongTien += Convert.ToDecimal(
                    row.Cells["ThanhTien"].Value);
            }

            lblTongTien.Text =
                tongTien.ToString("N0") + " VNĐ";
        }

        // Thanh toán hóa đơn
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboNhanVien.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên!");
                    return;
                }

                if (dgvChiTiet.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "Vui lòng thêm sản phẩm vào hóa đơn!");
                    return;
                }

                List<ChiTietHoaDonDTO> danhSachChiTiet =
                    new List<ChiTietHoaDonDTO>();

                foreach (DataGridViewRow row in dgvChiTiet.Rows)
                {
                    ChiTietHoaDonDTO ct = new ChiTietHoaDonDTO();

                    ct.MaSP = row.Cells["MaSP"].Value.ToString();
                    ct.SoLuong = Convert.ToInt32(
                        row.Cells["SoLuong"].Value);
                    ct.DonGia = Convert.ToDecimal(
                        row.Cells["DonGia"].Value);

                    danhSachChiTiet.Add(ct);
                }

                HoaDonDTO hd = new HoaDonDTO();

                hd.NgayLap = DateTime.Now;
                hd.MaNV = Convert.ToInt32(
                    cboNhanVien.SelectedValue);

                if (cboKhachHang.SelectedValue != null &&
                    cboKhachHang.SelectedValue != DBNull.Value)
                {
                    hd.MaKH = Convert.ToInt32(
                        cboKhachHang.SelectedValue);
                }
                else
                {
                    hd.MaKH = null;
                }

                DialogResult xacNhan = MessageBox.Show(
                    "Bạn có chắc chắn muốn thanh toán hóa đơn?",
                    "Xác nhận thanh toán",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (xacNhan != DialogResult.Yes)
                    return;

                int maHD = hdBUS.ThanhToan(
                    hd, danhSachChiTiet);

                MessageBox.Show(
                    "Thanh toán thành công!\nMã hóa đơn: " + maHD +
                    "\nTổng tiền: " + hd.TongTien.ToString("N0") + " VNĐ");

                dgvChiTiet.Rows.Clear();
                TinhTongTien();

                // Tải lại sản phẩm để cập nhật tồn kho
                cboSanPham.DataSource = spBUS.GetAll();
                cboSanPham.DisplayMember = "TenSP";
                cboSanPham.ValueMember = "MaSP";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Thanh toán thất bại!\n" + ex.Message,
                    "Lỗi thanh toán");
            }
        }

        // Làm mới hóa đơn đang lập
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            dgvChiTiet.Rows.Clear();
            numSoLuong.Value = 1;
            TinhTongTien();
        }
    }
}
