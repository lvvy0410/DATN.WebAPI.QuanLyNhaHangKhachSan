using System;
using System.Collections.Generic;

namespace DTO.Model
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
        public string? TenNguoiDung { get; set; } 
        public string Sdt { get; set; } 
        public string? Cccd { get; set; }
        public string? GioiTinh { get; set; } 
        public string DiaChi { get; set; } 
        public string? LoaiTaiKhoan { get; set; } 
        public string? TaiKhoan { get; set; } 
        public string MatKhau { get; set; } 
        public int? TrangThai { get; set; }

        public virtual ICollection<PhieuDat> PhieuDats { get; set; }
        public virtual ICollection<PhieuNhan> PhieuNhans { get; set; }
        public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; }
        public virtual ICollection<PhieuXuat> PhieuXuats { get; set; }
    }
}
