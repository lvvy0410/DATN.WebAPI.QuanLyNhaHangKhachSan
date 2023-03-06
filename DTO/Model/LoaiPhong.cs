using System;
using System.Collections.Generic;

namespace DTO.Model
{
    public partial class LoaiPhong
    {
        public LoaiPhong()
        {
            Phongs = new HashSet<Phong>();
        }

        public int LoaiPhongId { get; set; }
        public string? MaLoaiPhong { get; set; }
        public string TenLoaiPhong { get; set; } = null!;
        public int SoNguoiToiDa { get; set; }
        public double? DonGia { get; set; }

        public virtual ICollection<Phong> Phongs { get; set; }
    }
}
