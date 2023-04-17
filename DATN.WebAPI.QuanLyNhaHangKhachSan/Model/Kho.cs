using System;
using System.Collections.Generic;

namespace DATN.WebAPI.QuanLyNhaHangKhachSan.Model
{
    public partial class Kho
    {
        public Kho()
        {
            PhieuNhapChiTiets = new HashSet<PhieuNhapChiTiet>();
        }

        public int KhoId { get; set; }
        public string TenKho { get; set; } = null!;

        public virtual ICollection<PhieuNhapChiTiet> PhieuNhapChiTiets { get; set; }
    }
}
