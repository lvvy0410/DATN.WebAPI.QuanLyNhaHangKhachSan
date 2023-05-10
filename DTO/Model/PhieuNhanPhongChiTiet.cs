﻿using System;
using System.Collections.Generic;

namespace DTO.Model
{
    public partial class PhieuNhanPhongChiTiet
    {
        public long PhieuNhanPhongChiTietId { get; set; }
        public long PhieuNhanId { get; set; }
        public int PhongId { get; set; }
        public int SoNguoi { get; set; }
        public DateTime ThoiGianNhanPhong { get; set; }
        public DateTime? ThoiGianTraPhong { get; set; }
        public double DonGia { get; set; }
        public string TrangThai { get; set; } = null!;

        public virtual PhieuNhan PhieuNhan { get; set; } = null!;
        public virtual Phong Phong { get; set; } = null!;
    }
}
