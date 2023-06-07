using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.publicDTO
{
    public class BanDTO:Ban
    {
        public virtual LoaiBan? LoaiBan { get; set; } 
        public virtual TrangThai? TrangThai { get; set; }
        public string? TenBan { get; set; }
    }
}
