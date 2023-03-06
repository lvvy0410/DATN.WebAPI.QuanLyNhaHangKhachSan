using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Public
{
    public class ResponseDTO
    {
        public HttpStatusCode statusCode { get; set; }
        /// <summary>
        /// Tu dinh nghia
        /// </summary>
        public String? errorCode { get; set; }//404
        /// <summary>
        /// Mo ta loi
        /// </summary>
        public String? message { get; set; }//Khong tim thay
        /// <summary>
        /// du lieu tra ve: co the la 1 doi tuong, 1 danh sach
        /// </summary>
        public dynamic? data { get; set; }

        public static string GetValueError(ErrorCodeEnum errorCode)
        {
            switch (errorCode)
            {
                case ErrorCodeEnum.KhongTimThay:
                    return "Không tìm thấy";
                case ErrorCodeEnum.TenSanPhamBatBuocNhap:
                    return "Nhập tên";
                case ErrorCodeEnum.KhongTheThem:
                    return "Không thể thêm";
                case ErrorCodeEnum.KhongTheCapNhat:
                    return "Không thể cập nhật";
                case ErrorCodeEnum.ThemThanhCong:
                    return "Thêm thành công";
                case ErrorCodeEnum.CapNhatThanhCong:
                    return "Cập nhật thành công";
                case ErrorCodeEnum.KhongTheXoa:
                    return "Không thể xóa";
                case ErrorCodeEnum.XoaThanhCong:
                    return "Xóa thành công";
                default: break;
            }
            return errorCode.ToString();
        }
    }
}
