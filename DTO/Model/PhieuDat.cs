using System;
using System.Collections.Generic;

namespace DTO.Model
{
    public partial class PhieuDat
    {
        public PhieuDat()
        {
            PhieuDatBanChiTiets = new HashSet<PhieuDatBanChiTiet>();
            PhieuDatPhongChiTiets = new HashSet<PhieuDatPhongChiTiet>();
        }

        public long PhieuDatId { get; set; }
        public string SoChungtu { get; set; } = null!;
        public DateTime NgayLap { get; set; }
        public int NguoiDungId { get; set; }
        public int LoaiPhieuId { get; set; }
        public DateTime ThoiGianNhanDuKien { get; set; }
        public DateTime? ThoiGianTraDuKien { get; set; }
        public string? GhiChu { get; set; }
        public long KhachHangId { get; set; }
        public string? TrangThai { get; set; }

        public virtual KhachHang KhachHang { get; set; } = null!;
        public virtual LoaiPhieu LoaiPhieu { get; set; } = null!;
        public virtual NguoiDung NguoiDung { get; set; } = null!;
        public virtual ICollection<PhieuDatBanChiTiet> PhieuDatBanChiTiets { get; set; }
        public virtual ICollection<PhieuDatPhongChiTiet> PhieuDatPhongChiTiets { get; set; }
    }
}
