using DTO.publicDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MultiTable
{
    public class ListDichVu
    {
        public List<DichVuDTO>? dichVuDTOsBanDau { get; set; }
        public List<DichVuDTO>? dichVuDTOsThem { get; set; }
        public List<DichVuDTO>? dichVuDTOsCapNhat { get; set; }
    }
}
