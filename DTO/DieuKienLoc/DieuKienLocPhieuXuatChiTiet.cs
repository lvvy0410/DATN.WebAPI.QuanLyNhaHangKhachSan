using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DieuKienLoc
{
    public class DieuKienLocPhieuXuatChiTiet:PhieuXuatChiTiet
    {
        public virtual HangHoa? HangHoa { get; set; } 
        public virtual PhieuNhanBanChiTiet? PhieuNhanBanChiTiet { get; set; }
        public virtual PhieuNhanPhongChiTiet? PhieuNhanPhongChiTiet { get; set; }
        public virtual PhieuXuat? PhieuXuat { get; set; }

        public string? DonViTinh { get; set; } 
    }
}
