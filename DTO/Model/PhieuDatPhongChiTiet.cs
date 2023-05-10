using System;
using System.Collections.Generic;

namespace DTO.Model
{
    public partial class PhieuDatPhongChiTiet
    {
        public long PhieuDatPhongChiTietId { get; set; }
        public long PhieuDatId { get; set; }
        public int LoaiPhongId { get; set; }
        public int SoLuong { get; set; }
        public DateTime? ThoiGianNhanDuKien { get; set; }
        public DateTime? ThoiGianTraDuKien { get; set; }
        public string? TrangThai { get; set; }

        public virtual LoaiPhong LoaiPhong { get; set; } = null!;
        public virtual PhieuDat PhieuDat { get; set; } = null!;
    }
}
