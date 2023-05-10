using System;
using System.Collections.Generic;

namespace DTO.Model
{
    public partial class PhieuNhap
    {
        public PhieuNhap()
        {
            PhieuNhapChiTiets = new HashSet<PhieuNhapChiTiet>();
        }

        public long PhieuNhapId { get; set; }
        public string SoChungtu { get; set; } = null!;
        public DateTime NgayLap { get; set; }
        public int NguoiDungId { get; set; }
        public long? KhachHangId { get; set; }
        public string? GhiChu { get; set; }

        public virtual KhachHang? KhachHang { get; set; }
        public virtual NguoiDung NguoiDung { get; set; } = null!;
        public virtual ICollection<PhieuNhapChiTiet> PhieuNhapChiTiets { get; set; }
    }
}
