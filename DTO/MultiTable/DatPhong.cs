using DTO.Model;
using DTO.publicDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MultiTable
{
    public class DatPhong
    {
        public PhieuDatDTO phieuDatDTO { get; set; }
        public List<PhieuDatPhongChiTietDTO> phieuDatPhongChiTiets { get; set; }
        public KhachHang? khachHang { get; set; }
    }
}
