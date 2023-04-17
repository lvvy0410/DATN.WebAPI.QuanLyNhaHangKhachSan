using System;
using System.Collections.Generic;

namespace DATN.WebAPI.QuanLyNhaHangKhachSan.Model
{
    public partial class DichVu
    {
        public long DichVuId { get; set; }
        public int? PhongId { get; set; }
        public int? BanId { get; set; }
        public int HangHoaId { get; set; }
        public long? PhieuNhanId { get; set; }
        public double SoLuong { get; set; }
        public double DonGia { get; set; }
        public double ThanhTien { get; set; }
        public string GhiChu { get; set; } = null!;
        public string TrangThai { get; set; } = null!;
        public DateTime ThoiGian { get; set; }

        public virtual Ban? Ban { get; set; }
        public virtual HangHoa HangHoa { get; set; } = null!;
        public virtual PhieuNhan? PhieuNhan { get; set; }
        public virtual Phong? Phong { get; set; }
    }
}
