using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Model;

namespace DTO.publicDTO
{
    public class PhieuNhanPhongChiTietDTO : PhieuNhanPhongChiTiet
    {
        public virtual PhieuNhan? PhieuNhan { get; set; }
        public virtual Phong? Phong { get; set; }
    }
}
