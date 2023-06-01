using DTO.publicDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MultiTable
{
    public class XuatPhong
    {
        public PhieuXuatDTO  phieuXuatDTO {get;set; }
        public List<PhieuXuatChiTietDTO> phieuXuatChiTiets { get; set; } 

}
}
