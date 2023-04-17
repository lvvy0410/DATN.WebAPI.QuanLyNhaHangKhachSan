using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.publicDTO
{
    public class PhieuNhapChiTietDTO:PhieuNhapChiTiet
    {
        public virtual HangHoa? HangHoa { get; set; }
        public virtual Kho? Kho { get; set; } 
        public virtual PhieuNhap? PhieuNhap { get; set; }
    }
}
