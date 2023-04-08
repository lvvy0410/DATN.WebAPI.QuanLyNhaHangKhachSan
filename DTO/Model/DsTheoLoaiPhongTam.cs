using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Model
{
    public class DsTheoLoaiPhongTam

    {
       
        public int PhongId { get; set; }
        
        public int TrangThaiId { get; set; }
        public int SoPhong { get; set; }
        public int Tang { get; set; }   
        public int LoaiPhongId { get; set; }
        public string? MaLoaiPhong { get; set; }
        public string TenLoaiPhong { get; set; } = null!;
        public int SoNguoiToiDa { get; set; }
        public double DonGia { get; set; }

    }
}
