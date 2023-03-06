using System;
using System.Collections.Generic;

namespace DTO.Model
{
    public partial class TrangThai
    {
        public TrangThai()
        {
            Bans = new HashSet<Ban>();
            Phongs = new HashSet<Phong>();
            TrangThietBis = new HashSet<TrangThietBi>();
        }

        public int TrangThaiId { get; set; }
        public string TenTrangThai { get; set; } = null!;
        public string MaTrangThai { get; set; } = null!;

        public virtual ICollection<Ban> Bans { get; set; }
        public virtual ICollection<Phong> Phongs { get; set; }
        public virtual ICollection<TrangThietBi> TrangThietBis { get; set; }
    }
}
