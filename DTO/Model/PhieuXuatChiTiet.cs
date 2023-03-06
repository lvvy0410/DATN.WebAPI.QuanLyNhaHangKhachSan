using System;
using System.Collections.Generic;

namespace DTO.Model
{
    public partial class PhieuXuatChiTiet
    {
        public long PhieuXuatChiTietId { get; set; }
        public long PhieuXuatId { get; set; }
        public int HangHoaId { get; set; }
        public double SoLuong { get; set; }
        public double DonGia { get; set; }
        public double ThanhTien { get; set; }
        public string DonViTinh { get; set; } = null!;
        public string? GhiChu { get; set; }

        public virtual HangHoa HangHoa { get; set; } = null!;
        public virtual PhieuXuat PhieuXuat { get; set; } = null!;
    }
}
