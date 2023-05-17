using DTO.publicDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MultiTable
{
    public class NhanPhong
    {
        public PhieuNhanDTO phieuNhanDTO { get; set; }
        public List<PhieuNhanPhongChiTietDTO> phieuNhanPhongChiTietDTOs { get; set; }
    }
}
