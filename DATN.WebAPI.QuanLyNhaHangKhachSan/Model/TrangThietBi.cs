using System;
using System.Collections.Generic;

namespace DATN.WebAPI.QuanLyNhaHangKhachSan.Model
{
    public partial class TrangThietBi
    {
        public int TrangThietBiId { get; set; }
        public string TenTrangThietBi { get; set; } = null!;
        public int PhongId { get; set; }
        public int TrangThaiId { get; set; }
        public int SoLuong { get; set; }

        public virtual Phong Phong { get; set; } = null!;
        public virtual TrangThai TrangThai { get; set; } = null!;
    }
}
