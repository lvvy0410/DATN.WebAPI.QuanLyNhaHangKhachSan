using System;
using System.Collections.Generic;

namespace DATN.WebAPI.QuanLyNhaHangKhachSan.Model
{
    public partial class PhieuNhan
    {
        public PhieuNhan()
        {
            DichVus = new HashSet<DichVu>();
            GoiMons = new HashSet<GoiMon>();
            PhieuNhanBanChiTiets = new HashSet<PhieuNhanBanChiTiet>();
            PhieuNhanPhongChiTiets = new HashSet<PhieuNhanPhongChiTiet>();
            PhieuThus = new HashSet<PhieuThu>();
            PhieuXuats = new HashSet<PhieuXuat>();
        }

        public long PhieuNhanId { get; set; }
        public string SoChungTu { get; set; } = null!;
        public DateTime NgayLap { get; set; }
        public int NguoiDungId { get; set; }
        public int LoaiPhieuId { get; set; }
        public DateTime? NgayTra { get; set; }
        public long KhachHangId { get; set; }
        public string? GhiChu { get; set; }
        public string? TrangThai { get; set; }

        public virtual KhachHang KhachHang { get; set; } = null!;
        public virtual LoaiPhieu LoaiPhieu { get; set; } = null!;
        public virtual NguoiDung NguoiDung { get; set; } = null!;
        public virtual ICollection<DichVu> DichVus { get; set; }
        public virtual ICollection<GoiMon> GoiMons { get; set; }
        public virtual ICollection<PhieuNhanBanChiTiet> PhieuNhanBanChiTiets { get; set; }
        public virtual ICollection<PhieuNhanPhongChiTiet> PhieuNhanPhongChiTiets { get; set; }
        public virtual ICollection<PhieuThu> PhieuThus { get; set; }
        public virtual ICollection<PhieuXuat> PhieuXuats { get; set; }
    }
}
