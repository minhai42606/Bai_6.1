
using System;
using System.Drawing;
using System.Windows.Forms;
using RetailSalesSystem.BUS;
using RetailSalesSystem.DTO;

namespace RetailSalesSystem.GUI
{
    public partial class FrmKhachHang : Form
    {
        private KhachHangBUS bus = new KhachHangBUS();

        private TextBox txtMaKH;
        private TextBox txtTenKH;
        private TextBox txtDienThoai;
        private TextBox txtDiemTichLuy;
        private TextBox txtTimKiem;

        private DataGridView dgvKhachHang;

        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Button btnTimKiem;

        public FrmKhachHang()
        {
            InitializeComponent();

            TaoGiaoDien();
            LoadDanhSach();
        }

        // TẠO GIAO DIỆN
        private void TaoGiaoDien()
        {
            this.Text = "QUẢN LÝ KHÁCH HÀNG";
            this.Size = new Size(1000, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;

            Label lblTieuDe = new Label()
            {
                Text = "QUẢN LÝ KHÁCH HÀNG",
                Font = new Font("Arial", 20, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 15),
                Size = new Size(980, 45)
            };

            this.Controls.Add(lblTieuDe);

            // Mã khách hàng
            this.Controls.Add(TaoLabel("Mã khách hàng:", 30, 90));

            txtMaKH = TaoTextBox(160, 90);
            txtMaKH.ReadOnly = true;
            this.Controls.Add(txtMaKH);

            // Tên khách hàng
            this.Controls.Add(TaoLabel("Tên khách hàng:", 30, 135));

            txtTenKH = TaoTextBox(160, 135);
            this.Controls.Add(txtTenKH);

            // Điện thoại
            this.Controls.Add(TaoLabel("Điện thoại:", 510, 90));

            txtDienThoai = TaoTextBox(640, 90);
            this.Controls.Add(txtDienThoai);

            // Điểm tích lũy
            this.Controls.Add(TaoLabel("Điểm tích lũy:", 510, 135));

            txtDiemTichLuy = TaoTextBox(640, 135);
            txtDiemTichLuy.ReadOnly = true;
            this.Controls.Add(txtDiemTichLuy);

            // Các nút chức năng
            btnThem = TaoButton("Thêm", 30, 200);
            btnSua = TaoButton("Sửa", 150, 200);
            btnXoa = TaoButton("Xóa", 270, 200);
            btnLamMoi = TaoButton("Làm mới", 390, 200);

            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLamMoi.Click += btnLamMoi_Click;

            this.Controls.AddRange(new Control[]
            {
                btnThem, btnSua, btnXoa, btnLamMoi
            });

            // Tìm kiếm
            this.Controls.Add(TaoLabel("Tìm kiếm:", 30, 260));

            txtTimKiem = TaoTextBox(160, 260);
            txtTimKiem.Width = 350;
            this.Controls.Add(txtTimKiem);

            btnTimKiem = TaoButton("Tìm kiếm", 530, 255);
            btnTimKiem.Click += btnTimKiem_Click;
            this.Controls.Add(btnTimKiem);

            // Bảng danh sách
            dgvKhachHang = new DataGridView()
            {
                Location = new Point(30, 320),
                Size = new Size(920, 260),
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };

            dgvKhachHang.CellClick += dgvKhachHang_CellClick;

            this.Controls.Add(dgvKhachHang);
        }

        // TẠO LABEL
        private Label TaoLabel(string text, int x, int y)
        {
            return new Label()
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(125, 25),
                Font = new Font("Arial", 10)
            };
        }

        // TẠO TEXTBOX
        private TextBox TaoTextBox(int x, int y)
        {
            return new TextBox()
            {
                Location = new Point(x, y),
                Size = new Size(280, 27),
                Font = new Font("Arial", 10)
            };
        }

        // TẠO BUTTON
        private Button TaoButton(string text, int x, int y)
        {
            return new Button()
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(105, 35),
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
        }

        // TẢI DANH SÁCH
        private void LoadDanhSach()
        {
            try
            {
                dgvKhachHang.DataSource = null;
                dgvKhachHang.DataSource = bus.GetAll();

                dgvKhachHang.ClearSelection();
                XoaTrang();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải danh sách khách hàng: " + ex.Message);
            }
        }

        // XÓA TRẮNG
        private void XoaTrang()
        {
            txtMaKH.Clear();
            txtTenKH.Clear();
            txtDienThoai.Clear();
            txtDiemTichLuy.Clear();

            dgvKhachHang.ClearSelection();
        }

        // THÊM KHÁCH HÀNG
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTenKH.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên khách hàng!");
                    txtTenKH.Focus();
                    return;
                }

                KhachHangDTO kh = new KhachHangDTO();

                kh.TenKH = txtTenKH.Text.Trim();
                kh.DienThoai = txtDienThoai.Text.Trim();
                kh.DiemTichLuy = 0;

                bool ketQua = bus.Insert(kh);

                if (ketQua)
                {
                    MessageBox.Show("Thêm khách hàng thành công!");
                    LoadDanhSach();
                }
                else
                {
                    MessageBox.Show("Không thể thêm khách hàng!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm khách hàng: " + ex.Message);
            }
        }

        // SỬA KHÁCH HÀNG
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaKH.Text))
                {
                    MessageBox.Show("Vui lòng chọn khách hàng cần sửa!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTenKH.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên khách hàng!");
                    return;
                }

                KhachHangDTO kh = new KhachHangDTO();

                kh.MaKH = int.Parse(txtMaKH.Text);
                kh.TenKH = txtTenKH.Text.Trim();
                kh.DienThoai = txtDienThoai.Text.Trim();
                kh.DiemTichLuy = int.Parse(
                    string.IsNullOrWhiteSpace(txtDiemTichLuy.Text)
                    ? "0" : txtDiemTichLuy.Text);

                bool ketQua = bus.Update(kh);

                if (ketQua)
                {
                    MessageBox.Show("Cập nhật khách hàng thành công!");
                    LoadDanhSach();
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật khách hàng!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa khách hàng: " + ex.Message);
            }
        }

        // XÓA KHÁCH HÀNG
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaKH.Text))
                {
                    MessageBox.Show("Vui lòng chọn khách hàng cần xóa!");
                    return;
                }

                int maKH = int.Parse(txtMaKH.Text);

                DialogResult xacNhan = MessageBox.Show(
                    "Bạn có chắc muốn xóa khách hàng mã " + maKH + "?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (xacNhan == DialogResult.Yes)
                {
                    bool ketQua = bus.Delete(maKH);

                    if (ketQua)
                    {
                        MessageBox.Show("Xóa khách hàng thành công!");
                        LoadDanhSach();
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa khách hàng!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa khách hàng: " + ex.Message);
            }
        }

        // TÌM KIẾM
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                dgvKhachHang.DataSource = null;
                dgvKhachHang.DataSource =
                    bus.Search(txtTimKiem.Text.Trim());

                dgvKhachHang.ClearSelection();
                XoaTrang();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        // LÀM MỚI
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            LoadDanhSach();
        }

        // CHỌN KHÁCH HÀNG TRÊN BẢNG
        private void dgvKhachHang_CellClick(
            object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];

            txtMaKH.Text =
                row.Cells["MaKH"].Value?.ToString() ?? "";

            txtTenKH.Text =
                row.Cells["TenKH"].Value?.ToString() ?? "";

            txtDienThoai.Text =
                row.Cells["DienThoai"].Value?.ToString() ?? "";

            txtDiemTichLuy.Text =
                row.Cells["DiemTichLuy"].Value?.ToString() ?? "";
        }
    }
}
