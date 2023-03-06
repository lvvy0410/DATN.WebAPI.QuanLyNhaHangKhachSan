using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Public
{
    public class ErrorMessageDTO
    {
        public bool flagThanhCong { get; set; }
        public bool flagBiLoiEx { get; set; }
        public String? errorCode { get; set; }
        /// <summary>
        /// Mo ta loi
        /// </summary>
        public String? message { get; set; }
        /// <summary>
        /// du lieu tra ve: co the la 1 doi tuong, 1 danh sach
        /// </summary>
        public dynamic? data { get; set; }
    }
}
