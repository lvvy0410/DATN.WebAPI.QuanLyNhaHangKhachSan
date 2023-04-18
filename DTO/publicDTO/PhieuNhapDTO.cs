using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.publicDTO
{
    public class PhieuNhapDTO:PhieuNhap
    {
        public virtual KhachHang? KhachHang { get; set; }
        public virtual NguoiDung? NguoiDung { get; set; }
    }
}
