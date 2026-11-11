
using System;
using System.Data;
using RetailSalesSystem.DAL;
using RetailSalesSystem.DTO;

namespace RetailSalesSystem.BUS
{
    public class SanPhamBUS
    {
        private SanPhamDAL dal = new SanPhamDAL();

        // Lấy danh sách
        public DataTable GetAll()
        {
            return dal.GetAll();
        }

        // Thêm
        public bool Insert(SanPhamDTO sp)
        {
            if (sp == null)
                throw new Exception("Dữ liệu không hợp lệ!");

            if (string.IsNullOrWhiteSpace(sp.MaSP))
                throw new Exception("Mã sản phẩm không được để trống!");

            if (string.IsNullOrWhiteSpace(sp.TenSP))
                throw new Exception("Tên sản phẩm không được để trống!");

            if (sp.GiaBan <= 0)
                throw new Exception("Giá bán phải lớn hơn 0!");

            if (sp.SoLuongTon < 0)
                throw new Exception("Số lượng tồn không được âm!");

            return dal.Insert(sp);
        }

        // Sửa
        public bool Update(SanPhamDTO sp)
        {
            if (sp == null)
                throw new Exception("Dữ liệu không hợp lệ!");

            if (string.IsNullOrWhiteSpace(sp.MaSP))
                throw new Exception("Vui lòng nhập mã sản phẩm!");

            if (string.IsNullOrWhiteSpace(sp.TenSP))
                throw new Exception("Vui lòng nhập tên sản phẩm!");

            if (sp.GiaBan <= 0)
                throw new Exception("Giá bán phải lớn hơn 0!");

            if (sp.SoLuongTon < 0)
                throw new Exception("Số lượng tồn không được âm!");

            return dal.Update(sp);
        }

        // Xóa
        public bool Delete(string maSP)
        {
            if (string.IsNullOrWhiteSpace(maSP))
                throw new Exception("Vui lòng chọn sản phẩm!");

            return dal.Delete(maSP);
        }

        // Tìm kiếm
        public DataTable Search(string keyword)
        {
            return dal.Search(keyword ?? "");
        }
    }
}
