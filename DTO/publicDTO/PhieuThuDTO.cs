using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.publicDTO
{
    public class PhieuThuDTO:PhieuThu
    {
        public virtual PhieuNhan? PhieuNhan { get; set; }
   
    }
}
