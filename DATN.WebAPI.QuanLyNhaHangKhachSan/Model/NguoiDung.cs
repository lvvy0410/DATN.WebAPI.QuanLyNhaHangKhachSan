using System;
using System.Collections.Generic;

namespace DATN.WebAPI.QuanLyNhaHangKhachSan.Model
{
    public partial class NguoiDung
    {
        public NguoiDung()
        {
            PhieuDats = new HashSet<PhieuDat>();
            PhieuNhans = new HashSet<PhieuNhan>();
            PhieuNhaps = new HashSet<PhieuNhap>();
            PhieuXuats = new HashSet<PhieuXuat>();
        }

        public int NguoiDungId { get; set; }
        public string TenNguoiDung { get; set; } = null!;
        public string Sdt { get; set; } = null!;
        public string Cccd { get; set; } = null!;
        public string GioiTinh { get; set; } = null!;
        public string DiaChi { get; set; } = null!;
        public string LoaiTaiKhoan { get; set; } = null!;
        public string TaiKhoan { get; set; } = null!;
        public string MatKhau { get; set; } = null!;
        public int TrangThai { get; set; }

        public virtual ICollection<PhieuDat> PhieuDats { get; set; }
        public virtual ICollection<PhieuNhan> PhieuNhans { get; set; }
        public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; }
        public virtual ICollection<PhieuXuat> PhieuXuats { get; set; }
    }
}
