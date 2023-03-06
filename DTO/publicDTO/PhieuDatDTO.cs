using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Model;

namespace DTO.publicDTO
{
    public class PhieuDatDTO : PhieuDat
    {
        public virtual KhachHang? KhachHang { get; set; }
        public virtual LoaiPhieu? LoaiPhieu { get; set; }
        public virtual NguoiDung? NguoiDung { get; set; }
    }
}
