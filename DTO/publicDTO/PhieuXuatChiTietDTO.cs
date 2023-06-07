using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.publicDTO
{
    public class PhieuXuatChiTietDTO:PhieuXuatChiTiet
    {
        public virtual HangHoa? HangHoa { get; set; }
    
        public virtual PhieuXuat? PhieuXuat { get; set; }

        public virtual PhieuNhanBanChiTiet? PhieuNhanBanChiTiet { get; set; }
        public virtual PhieuNhanPhongChiTiet? PhieuNhanPhongChiTiet { get; set; }
        public long? PhieuNhanBanChiTietId { get; set; }
    }
}
