using DTO.Model;
using DTO.publicDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MultiTable
{
    public class DatBan
    {
        public PhieuDatDTO phieuDatDTO { get; set; }
        public List<PhieuDatBanChiTietDTO> phieuDatBanChiTiets { get; set; }
        public KhachHang? khachHang { get; set; }
    }
}
