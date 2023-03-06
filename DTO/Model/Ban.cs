using System;
using System.Collections.Generic;

namespace DTO.Model
{
    public partial class Ban
    {
        public Ban()
        {
            DichVus = new HashSet<DichVu>();
            GoiMons = new HashSet<GoiMon>();
            PhieuDatBanChiTiets = new HashSet<PhieuDatBanChiTiet>();
            PhieuNhanBanChiTiets = new HashSet<PhieuNhanBanChiTiet>();
        }

        public int BanId { get; set; }
        public int LoaiBanId { get; set; }
        public int TrangThaiId { get; set; }
        public string TenBan { get; set; } = null!;

        public virtual LoaiBan LoaiBan { get; set; } = null!;
        public virtual TrangThai TrangThai { get; set; } = null!;
        public virtual ICollection<DichVu> DichVus { get; set; }
        public virtual ICollection<GoiMon> GoiMons { get; set; }
        public virtual ICollection<PhieuDatBanChiTiet> PhieuDatBanChiTiets { get; set; }
        public virtual ICollection<PhieuNhanBanChiTiet> PhieuNhanBanChiTiets { get; set; }
    }
}
