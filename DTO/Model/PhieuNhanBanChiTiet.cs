using System;
using System.Collections.Generic;

namespace DTO.Model
{
    public partial class PhieuNhanBanChiTiet
    {
        public PhieuNhanBanChiTiet()
        {
            PhieuNhapChiTiets = new HashSet<PhieuNhapChiTiet>();
            PhieuXuatChiTiets = new HashSet<PhieuXuatChiTiet>();
        }

        public long PhieuNhanBanChiTietId { get; set; }
        public long PhieuNhanId { get; set; }
        public int BanId { get; set; }
        public int SoNguoi { get; set; }
        public DateTime ThoiGianNhanBan { get; set; }
        public DateTime? ThoiGianTraBan { get; set; }
        public int TrangThai { get; set; }

        public virtual Ban Ban { get; set; } = null!;
        public virtual PhieuNhan PhieuNhan { get; set; } = null!;
        public virtual ICollection<PhieuNhapChiTiet> PhieuNhapChiTiets { get; set; }
        public virtual ICollection<PhieuXuatChiTiet> PhieuXuatChiTiets { get; set; }
    }
}
