using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Public
{
    public enum ErrorCodeEnum
    {
        BadRequest = 400,
        NotFound = 404,
        InternalServerError = 500,
        NoError = 0,
        TenSanPhamBatBuocNhap = 1,
        KhongTimThay = 2,
        KhongTheThem = 3,
        KhongTheCapNhat = 4,
        ThemThanhCong = 5,
        CapNhatThanhCong = 6,
        KhongTheXoa = 7,
        XoaThanhCong = 8,
    }

    
}
