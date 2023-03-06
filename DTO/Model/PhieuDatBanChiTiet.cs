using System;
using System.Collections.Generic;

namespace DTO.Model
{
    public partial class PhieuDatBanChiTiet
    {
        public long PhieuDatBanChiTietId { get; set; }
        public long PhieuDatId { get; set; }
        public int BanId { get; set; }
        public int SoNguoi { get; set; }

        public virtual Ban Ban { get; set; } = null!;
        public virtual PhieuDat PhieuDat { get; set; } = null!;
    }
}
