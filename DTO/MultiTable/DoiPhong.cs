using DTO.publicDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MultiTable
{
    public class DoiPhong
    {
        public PhongDTO viTriBanDau { get; set; }
        public PhongDTO viTriDoi { get; set; }
        public string lyDoDoi { get; set; }
        public string? ghiChu { get; set; }
    }
}
