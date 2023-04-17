using System;
using System.Collections.Generic;

namespace DATN.WebAPI.QuanLyNhaHangKhachSan.Model
{
    public partial class LichSuDoi
    {
        public long LichSuDoiId { get; set; }
        public string ViTriBanDau { get; set; } = null!;
        public string ViTriDoi { get; set; } = null!;
        public string LyDoDoi { get; set; } = null!;
        public string? GhiChu { get; set; }
        public long PhieuNhanId { get; set; }

        public virtual PhieuNhan PhieuNhan { get; set; } = null!;
    }
}
