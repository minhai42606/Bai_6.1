
using System;
using System.Drawing;
using System.Windows.Forms;
using RetailSalesSystem.BUS;
using RetailSalesSystem.DTO;

namespace RetailSalesSystem.GUI
{
    public partial class FrmSanPham : Form
    {
        private SanPhamBUS bus = new SanPhamBUS();

        private TextBox txtMaSP;
        private TextBox txtTenSP;
        private TextBox txtGiaBan;
        private TextBox txtSoLuongTon;
        private TextBox txtTimKiem;

        private DataGridView dgvSanPham;

        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Button btnTimKiem;

        public FrmSanPham()
        {
            InitializeComponent();

            TaoGiaoDien();
            LoadDanhSach();
        }

        // TẠO GIAO DIỆN
        private void TaoGiaoDien()
        {
            this.Text = "QUẢN LÝ SẢN PHẨM";
            this.Size = new Size(1000, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;

            Label lblTieuDe = new Label()
            {
                Text = "QUẢN LÝ SẢN PHẨM",
                Font = new Font("Arial", 20, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 15),
                Size = new Size(980, 45)
            };

            this.Controls.Add(lblTieuDe);

            // Mã sản phẩm
            this.Controls.Add(TaoLabel("Mã sản phẩm:", 30, 90));
            txtMaSP = TaoTextBox(160, 90);
            this.Controls.Add(txtMaSP);

            // Tên sản phẩm
            this.Controls.Add(TaoLabel("Tên sản phẩm:", 30, 135));
            txtTenSP = TaoTextBox(160, 135);
            this.Controls.Add(txtTenSP);

            // Giá bán
            this.Controls.Add(TaoLabel("Giá bán:", 510, 90));
            txtGiaBan = TaoTextBox(640, 90);
            this.Controls.Add(txtGiaBan);

            // Số lượng tồn
            this.Controls.Add(TaoLabel("Số lượng tồn:", 510, 135));
            txtSoLuongTon = TaoTextBox(640, 135);
            this.Controls.Add(txtSoLuongTon);

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
            dgvSanPham = new DataGridView()
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

            dgvSanPham.CellClick += dgvSanPham_CellClick;

            this.Controls.Add(dgvSanPham);
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

        // HIỂN THỊ DANH SÁCH
        private void LoadDanhSach()
        {
            try
            {
                dgvSanPham.DataSource = bus.GetAll();
                dgvSanPham.ClearSelection();
                XoaTrang();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải danh sách: " + ex.Message);
            }
        }

        // XÓA TRẮNG CÁC Ô
        private void XoaTrang()
        {
            txtMaSP.Clear();
            txtTenSP.Clear();
            txtGiaBan.Clear();
            txtSoLuongTon.Clear();

            dgvSanPham.ClearSelection();
        }

        // KIỂM TRA DỮ LIỆU
        private bool KiemTraDuLieu()
        {
            if (string.IsNullOrWhiteSpace(txtMaSP.Text) ||
                string.IsNullOrWhiteSpace(txtTenSP.Text) ||
                string.IsNullOrWhiteSpace(txtGiaBan.Text) ||
                string.IsNullOrWhiteSpace(txtSoLuongTon.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return false;
            }

            if (!decimal.TryParse(txtGiaBan.Text.Trim(), out decimal gia))
            {
                MessageBox.Show("Giá bán phải là số hợp lệ!");
                return false;
            }

            if (gia <= 0)
            {
                MessageBox.Show("Giá bán phải lớn hơn 0!");
                return false;
            }

            if (!int.TryParse(txtSoLuongTon.Text.Trim(), out int soLuong))
            {
                MessageBox.Show("Số lượng tồn phải là số nguyên!");
                return false;
            }

            if (soLuong < 0)
            {
                MessageBox.Show("Số lượng tồn không được âm!");
                return false;
            }

            return true;
        }

        // LẤY DỮ LIỆU TỪ TEXTBOX
        private SanPhamDTO LayThongTinSanPham()
        {
            SanPhamDTO sp = new SanPhamDTO();

            sp.MaSP = txtMaSP.Text.Trim();
            sp.TenSP = txtTenSP.Text.Trim();
            sp.GiaBan = decimal.Parse(txtGiaBan.Text.Trim());
            sp.SoLuongTon = int.Parse(txtSoLuongTon.Text.Trim());

            return sp;
        }

        // THÊM SẢN PHẨM
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!KiemTraDuLieu())
                    return;

                SanPhamDTO sp = LayThongTinSanPham();

                bool ketQua = bus.Insert(sp);

                if (ketQua)
                {
                    MessageBox.Show("Thêm sản phẩm thành công!");
                    LoadDanhSach();
                }
                else
                {
                    MessageBox.Show("Không thể thêm sản phẩm!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm sản phẩm: " + ex.Message);
            }
        }

        // SỬA SẢN PHẨM
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (!KiemTraDuLieu())
                    return;

                SanPhamDTO sp = LayThongTinSanPham();

                bool ketQua = bus.Update(sp);

                if (ketQua)
                {
                    MessageBox.Show("Cập nhật sản phẩm thành công!");
                    LoadDanhSach();
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật sản phẩm!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa sản phẩm: " + ex.Message);
            }
        }

        // XÓA SẢN PHẨM
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaSP.Text))
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!");
                    return;
                }

                DialogResult xacNhan = MessageBox.Show(
                    "Bạn có chắc muốn xóa sản phẩm " + txtMaSP.Text + "?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (xacNhan == DialogResult.Yes)
                {
                    bool ketQua = bus.Delete(txtMaSP.Text.Trim());

                    if (ketQua)
                    {
                        MessageBox.Show("Xóa sản phẩm thành công!");
                        LoadDanhSach();
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa sản phẩm!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa sản phẩm: " + ex.Message);
            }
        }

        // TÌM KIẾM
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                dgvSanPham.DataSource =
                    bus.Search(txtTimKiem.Text.Trim());

                dgvSanPham.ClearSelection();
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

        // CHỌN DÒNG TRÊN BẢNG
        private void dgvSanPham_CellClick(
            object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvSanPham.Rows[e.RowIndex];

            txtMaSP.Text =
                row.Cells["MaSP"].Value?.ToString() ?? "";

            txtTenSP.Text =
                row.Cells["TenSP"].Value?.ToString() ?? "";

            txtGiaBan.Text =
                row.Cells["GiaBan"].Value?.ToString() ?? "";

            txtSoLuongTon.Text =
                row.Cells["SoLuongTon"].Value?.ToString() ?? "";
        }
    }
}
