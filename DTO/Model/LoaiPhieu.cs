using System;
using System.Collections.Generic;

namespace DTO.Model
{
    public partial class LoaiPhieu
    {
        public LoaiPhieu()
        {
            PhieuDats = new HashSet<PhieuDat>();
            PhieuNhans = new HashSet<PhieuNhan>();
        }

        public int LoaiPhieuId { get; set; }
        public string TenLoaiPhieu { get; set; } = null!;
        public string MaLoaiPhieu { get; set; } = null!;

        public virtual ICollection<PhieuDat> PhieuDats { get; set; }
        public virtual ICollection<PhieuNhan> PhieuNhans { get; set; }
    }
}
