﻿using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.publicDTO
{
    public class TrangThietBiDTO:TrangThietBi
    {
        public virtual Phong? Phong { get; set; }
        public virtual TrangThai? TrangThai { get; set; }
    }
}