using System;
using System.Collections.Generic;

namespace DTO.Model
{
    public partial class Phong
    {
        public Phong()
        {
            DichVus = new HashSet<DichVu>();
            PhieuNhanPhongChiTiets = new HashSet<PhieuNhanPhongChiTiet>();
            TrangThietBis = new HashSet<TrangThietBi>();
        }

        public int PhongId { get; set; }
        public int LoaiPhongId { get; set; }
        public int TrangThaiId { get; set; }
        public int SoPhong { get; set; }
        public int Tang { get; set; }

        public virtual LoaiPhong LoaiPhong { get; set; } = null!;
        public virtual TrangThai TrangThai { get; set; } = null!;
        public virtual ICollection<DichVu> DichVus { get; set; }
        public virtual ICollection<PhieuNhanPhongChiTiet> PhieuNhanPhongChiTiets { get; set; }
        public virtual ICollection<TrangThietBi> TrangThietBis { get; set; }
    }
}
