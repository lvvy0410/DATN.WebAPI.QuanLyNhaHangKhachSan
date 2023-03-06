using System;
using System.Collections.Generic;

namespace DTO.Model
{
    public partial class PhieuThu
    {
        public long PhieuThuId { get; set; }
        public string SoChungTu { get; set; } = null!;
        public DateTime NgayLap { get; set; }
        public double SoTienCanThanhToan { get; set; }
        public double? SoTienKhachDua { get; set; }
        public double? SoTienTraLaiKhach { get; set; }
        public string PhuongThucThanhToan { get; set; } = null!;
        public string? SoTk { get; set; }
        public long? PhieuNhanId { get; set; }
        public string? GhiChu { get; set; }

        public virtual PhieuNhan? PhieuNhan { get; set; }
    }
}
