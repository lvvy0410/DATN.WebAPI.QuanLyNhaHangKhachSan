using System;
using System.Collections.Generic;

namespace DTO.Model
{
    public partial class PhieuXuat
    {
        public PhieuXuat()
        {
            PhieuXuatChiTiets = new HashSet<PhieuXuatChiTiet>();
        }

        public long PhieuXuatId { get; set; }
        public long KhachHangId { get; set; }
        public string SoChungTu { get; set; } = null!;
        public long? PhieuNhanId { get; set; }
        public DateTime NgayLap { get; set; }
        public int NguoiDungId { get; set; }
        public double TongThanhTien { get; set; }
        public double? PhuThu { get; set; }
        public double? ChietKhau { get; set; }
        public int Trangthai { get; set; }
        public string? GhiChu { get; set; }

        public virtual KhachHang KhachHang { get; set; } = null!;
        public virtual NguoiDung NguoiDung { get; set; } = null!;
        public virtual PhieuNhan? PhieuNhan { get; set; }
        public virtual ICollection<PhieuXuatChiTiet> PhieuXuatChiTiets { get; set; }
    }
}
