using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.publicDTO
{
    public class DichVuDTO:DichVu
    {
        public virtual Ban? Ban { get; set; }
        public virtual HangHoa? HangHoa { get; set; } 
        public virtual PhieuNhan? PhieuNhan { get; set; }
        public virtual Phong? Phong { get; set; }
    }
}
