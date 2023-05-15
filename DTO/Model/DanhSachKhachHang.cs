using System;
using System.Collections.Generic;

namespace DTO.Model
{
    public partial class DanhSachKhachHang
    {
        public long DanhSachKhachHangId { get; set; }
        public long PhieuNhanPhongChiTietId { get; set; }
        public long KhachHangId { get; set; }

        public virtual KhachHang KhachHang { get; set; } = null!;
        public virtual PhieuNhanPhongChiTiet PhieuNhanPhongChiTiet { get; set; } = null!;
    }
}
