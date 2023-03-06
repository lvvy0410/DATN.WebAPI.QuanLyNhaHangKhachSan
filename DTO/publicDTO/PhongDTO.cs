using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.publicDTO
{
    public class PhongDTO:Phong
    {
        public virtual LoaiPhong? LoaiPhong { get; set; }
        public virtual TrangThai? TrangThai { get; set; }
    }
}
