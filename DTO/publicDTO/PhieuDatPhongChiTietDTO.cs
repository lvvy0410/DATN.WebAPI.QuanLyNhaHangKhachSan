using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Model;

namespace DTO.publicDTO
{
    public class PhieuDatPhongChiTietDTO : PhieuDatPhongChiTiet
    {
        public virtual PhieuDat? PhieuDat { get; set; }

        public virtual LoaiPhong? LoaiPhong { get; set; }
        //sửa loại phòng
    }
}
