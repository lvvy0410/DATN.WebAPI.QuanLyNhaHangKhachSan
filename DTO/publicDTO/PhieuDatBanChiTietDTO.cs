using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.publicDTO
{
    public class PhieuDatBanChiTietDTO : PhieuDatBanChiTiet
    {
        public virtual Ban? Ban { get; set; } = null!;
        public virtual PhieuDat? PhieuDat { get; set; } = null!;
    }
}
