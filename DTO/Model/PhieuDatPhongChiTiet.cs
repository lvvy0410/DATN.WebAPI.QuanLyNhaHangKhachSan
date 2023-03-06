using System;
using System.Collections.Generic;

namespace DTO.Model
{
    public partial class PhieuDatPhongChiTiet
    {
        public long PhieuDatPhongChiTietId { get; set; }
        public long PhieuDatId { get; set; }
        public int PhongId { get; set; }
        public int SoNguoi { get; set; }

        public virtual PhieuDat PhieuDat { get; set; } = null!;
        public virtual Phong Phong { get; set; } = null!;
    }
}
