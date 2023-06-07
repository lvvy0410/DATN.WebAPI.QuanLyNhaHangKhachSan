using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DieuKienLoc
{
    public class DieuKienLocPhieuDat
    {
        public long? PhieuDatId { get; set; }
        public string? SoChungTu { get;set; }
        public int? loaiPhieu { get; set; }
        public long? KhachHangId { get; set; }
        public string? TrangThai { get; set; }
    }
}
