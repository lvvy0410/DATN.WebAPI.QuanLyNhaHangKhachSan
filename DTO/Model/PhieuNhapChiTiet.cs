using System;
using System.Collections.Generic;

namespace DTO.Model
{
    public partial class PhieuNhapChiTiet
    {
        public long PhieuNhapChiTietId { get; set; }
        public long PhieuNhapId { get; set; }
        public int KhoId { get; set; }
        public string TenMatHang { get; set; } = null!;
        public double SoLuong { get; set; }
        public double Gia { get; set; }
        public double ThanhTien { get; set; }
        public string? DonViTinh { get; set; }
        public int? HangHoaId { get; set; }
        public string? GhiChu { get; set; }

        public virtual HangHoa? HangHoa { get; set; }
        public virtual Kho Kho { get; set; } = null!;
        public virtual PhieuNhap PhieuNhap { get; set; } = null!;
    }
}
