using Microsoft.Data.SqlClient;
using RetailSalesSystem.DAL;

namespace RetailSalesSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnKetNoi_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();

                    MessageBox.Show(
                        "Kết nối SQL Server thành công!",
                        "Thông báo"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi kết nối: " + ex.Message
                );
            }
        }
    }
}
