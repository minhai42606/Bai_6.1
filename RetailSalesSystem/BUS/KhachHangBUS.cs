
using System;
using System.Collections.Generic;
using RetailSalesSystem.DAL;
using RetailSalesSystem.DTO;

namespace RetailSalesSystem.BUS
{
    public class KhachHangBUS
    {
        private KhachHangDAL dal = new KhachHangDAL();

        // 1. Lấy danh sách khách hàng
        public List<KhachHangDTO> GetAll()
        {
            return dal.GetAll();
        }

        // 2. Thêm khách hàng
        public bool Insert(KhachHangDTO kh)
        {
            if (kh == null)
                throw new Exception("Thông tin khách hàng không hợp lệ!");

            if (string.IsNullOrWhiteSpace(kh.TenKH))
                throw new Exception("Tên khách hàng không được để trống!");

            if (kh.TenKH.Trim().Length > 100)
                throw new Exception("Tên khách hàng không được quá 100 ký tự!");

            if (!string.IsNullOrWhiteSpace(kh.DienThoai)
                && kh.DienThoai.Trim().Length > 15)
                throw new Exception("Số điện thoại không được quá 15 ký tự!");

            kh.TenKH = kh.TenKH.Trim();
            kh.DienThoai = string.IsNullOrWhiteSpace(kh.DienThoai)
                ? null : kh.DienThoai.Trim();

            return dal.Insert(kh);
        }

        // 3. Sửa khách hàng
        public bool Update(KhachHangDTO kh)
        {
            if (kh == null)
                throw new Exception("Thông tin khách hàng không hợp lệ!");

            if (kh.MaKH <= 0)
                throw new Exception("Mã khách hàng không hợp lệ!");

            if (string.IsNullOrWhiteSpace(kh.TenKH))
                throw new Exception("Tên khách hàng không được để trống!");

            if (kh.TenKH.Trim().Length > 100)
                throw new Exception("Tên khách hàng không được quá 100 ký tự!");

            if (!string.IsNullOrWhiteSpace(kh.DienThoai)
                && kh.DienThoai.Trim().Length > 15)
                throw new Exception("Số điện thoại không được quá 15 ký tự!");

            kh.TenKH = kh.TenKH.Trim();
            kh.DienThoai = string.IsNullOrWhiteSpace(kh.DienThoai)
                ? null : kh.DienThoai.Trim();

            return dal.Update(kh);
        }

        // 4. Xóa khách hàng
        public bool Delete(int maKH)
        {
            if (maKH <= 0)
                throw new Exception("Mã khách hàng không hợp lệ!");

            return dal.Delete(maKH);
        }

        // 5. Tìm kiếm khách hàng
        public List<KhachHangDTO> Search(string tuKhoa)
        {
            if (string.IsNullOrWhiteSpace(tuKhoa))
                return dal.GetAll();

            return dal.Search(tuKhoa.Trim());
        }
    }
}
