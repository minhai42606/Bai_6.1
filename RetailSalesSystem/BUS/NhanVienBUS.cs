
using System;
using System.Data;
using RetailSalesSystem.DAL;
using RetailSalesSystem.DTO;

namespace RetailSalesSystem.BUS
{
    public class NhanVienBUS
    {
        private NhanVienDAL dal = new NhanVienDAL();

        // Lấy danh sách nhân viên
        public DataTable GetAll()
        {
            return dal.GetAll();
        }

        // Thêm nhân viên
        public bool Insert(NhanVienDTO nv)
        {
            if (nv == null)
                throw new Exception("Thông tin nhân viên không hợp lệ!");

            if (string.IsNullOrWhiteSpace(nv.TenNV))
                throw new Exception("Tên nhân viên không được để trống!");

            if (nv.TenNV.Trim().Length > 100)
                throw new Exception("Tên nhân viên không được quá 100 ký tự!");

            if (!string.IsNullOrWhiteSpace(nv.SoDienThoai)
                && nv.SoDienThoai.Trim().Length > 15)
                throw new Exception("Số điện thoại không được quá 15 ký tự!");

            nv.TenNV = nv.TenNV.Trim();
            nv.SoDienThoai = string.IsNullOrWhiteSpace(nv.SoDienThoai)
                ? null : nv.SoDienThoai.Trim();
            nv.ChucVu = string.IsNullOrWhiteSpace(nv.ChucVu)
                ? null : nv.ChucVu.Trim();

            return dal.Insert(nv);
        }

        // Sửa nhân viên
        public bool Update(NhanVienDTO nv)
        {
            if (nv == null)
                throw new Exception("Thông tin nhân viên không hợp lệ!");

            if (nv.MaNV <= 0)
                throw new Exception("Mã nhân viên không hợp lệ!");

            if (string.IsNullOrWhiteSpace(nv.TenNV))
                throw new Exception("Tên nhân viên không được để trống!");

            if (nv.TenNV.Trim().Length > 100)
                throw new Exception("Tên nhân viên không được quá 100 ký tự!");

            if (!string.IsNullOrWhiteSpace(nv.SoDienThoai)
                && nv.SoDienThoai.Trim().Length > 15)
                throw new Exception("Số điện thoại không được quá 15 ký tự!");

            nv.TenNV = nv.TenNV.Trim();
            nv.SoDienThoai = string.IsNullOrWhiteSpace(nv.SoDienThoai)
                ? null : nv.SoDienThoai.Trim();
            nv.ChucVu = string.IsNullOrWhiteSpace(nv.ChucVu)
                ? null : nv.ChucVu.Trim();

            return dal.Update(nv);
        }

        // Xóa nhân viên
        public bool Delete(int maNV)
        {
            if (maNV <= 0)
                throw new Exception("Mã nhân viên không hợp lệ!");

            return dal.Delete(maNV);
        }

        // Tìm kiếm nhân viên
        public DataTable Search(string keyword)
        {
            return dal.Search(keyword ?? "");
        }
    }
}
