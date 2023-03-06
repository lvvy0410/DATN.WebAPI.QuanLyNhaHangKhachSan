using System;
using System.Collections.Generic;

namespace DTO.Model
{
    public partial class LoaiBan
    {
        public LoaiBan()
        {
            Bans = new HashSet<Ban>();
        }

        public int LoaiBanId { get; set; }
        public string TenLoaiBan { get; set; } = null!;
        public int SoNguoiToiDa { get; set; }

        public virtual ICollection<Ban> Bans { get; set; }
    }
}
