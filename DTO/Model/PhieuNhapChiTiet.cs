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
        public string DonViTinh { get; set; } = null!;
        public int? HangHoaId { get; set; }
        public string? GhiChu { get; set; }
        public long? PhieuNhanPhongChiTietId { get; set; }
        public long? PhieuNhanBanChiTietId { get; set; }

        public virtual HangHoa? HangHoa { get; set; }
        public virtual Kho Kho { get; set; } = null!;
        public virtual PhieuNhanBanChiTiet? PhieuNhanBanChiTiet { get; set; }
        public virtual PhieuNhanPhongChiTiet? PhieuNhanPhongChiTiet { get; set; }
        public virtual PhieuNhap PhieuNhap { get; set; } = null!;
    }
}
