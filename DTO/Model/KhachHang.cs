using System;
using System.Collections.Generic;

namespace DTO.Model
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            PhieuDats = new HashSet<PhieuDat>();
            PhieuNhans = new HashSet<PhieuNhan>();
            PhieuNhaps = new HashSet<PhieuNhap>();
            PhieuXuats = new HashSet<PhieuXuat>();
        }

        public long KhachHangId { get; set; }
        public string? Cccd { get; set; }
        public string? Sdt { get; set; }
        public string? TenKhachHang { get; set; }
        public string? LoaiKhachHang { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? GioiTinh { get; set; }
        public string? QueQuan { get; set; }
        public string? NoiThuongTru { get; set; }

        public virtual ICollection<PhieuDat> PhieuDats { get; set; }
        public virtual ICollection<PhieuNhan> PhieuNhans { get; set; }
        public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; }
        public virtual ICollection<PhieuXuat> PhieuXuats { get; set; }
    }
}
