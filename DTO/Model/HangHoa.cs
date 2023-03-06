using System;
using System.Collections.Generic;

namespace DTO.Model
{
    public partial class HangHoa
    {
        public HangHoa()
        {
            DichVus = new HashSet<DichVu>();
            GoiMons = new HashSet<GoiMon>();
            PhieuNhapChiTiets = new HashSet<PhieuNhapChiTiet>();
            PhieuXuatChiTiets = new HashSet<PhieuXuatChiTiet>();
        }

        public int HangHoaId { get; set; }
        public string MaHangHoa { get; set; } = null!;
        public string TenHangHoa { get; set; } = null!;
        public double DonGia { get; set; }
        public string TrangThai { get; set; } = null!;
        public string NhomHangHoa { get; set; } = null!;

        public virtual ICollection<DichVu> DichVus { get; set; }
        public virtual ICollection<GoiMon> GoiMons { get; set; }
        public virtual ICollection<PhieuNhapChiTiet> PhieuNhapChiTiets { get; set; }
        public virtual ICollection<PhieuXuatChiTiet> PhieuXuatChiTiets { get; set; }
    }
}
