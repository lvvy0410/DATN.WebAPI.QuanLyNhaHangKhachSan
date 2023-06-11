using DTO.Model;
using DTO.publicDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MultiTable
{
    public class NhanBan
    {
        public PhieuNhanDTO phieuNhanDTO { get; set; }
        public List<PhieuNhanBanChiTietDTO> phieuNhanBanChiTietDTOs { get; set; }
        public KhachHang? khachHang { get; set; }
    }
}
