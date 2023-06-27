using DTO.publicDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MultiTable
{
    public class DoiBan
    {
        public BanDTO viTriBanDau { get; set; }
        public BanDTO viTriDoi { get; set; }
        public string lyDoDoi { get; set; }
        public string? ghiChu { get; set; }
    }
}
