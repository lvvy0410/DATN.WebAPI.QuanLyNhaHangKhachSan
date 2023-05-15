using DAO;
using DTO.Context;
using DTO.DieuKienLoc;
using DTO.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DATN.WebAPI.QuanLyNhaHangKhachSan.Controllers
{
    [Authorize]
    [Route("api/PhieuXuatChiTiet")]
    [ApiController]
    public class PhieuXuatChiTietController : Controller
    {
        private readonly QuanLyNhaHangKhachSanContext dbcontext;
        private readonly PhieuXuatChiTietDAO phieuXuatChiTietDAO;

        public PhieuXuatChiTietController(QuanLyNhaHangKhachSanContext dbcontext, PhieuXuatChiTietDAO phieuXuatChiTietDAO)
        {
            this.phieuXuatChiTietDAO = phieuXuatChiTietDAO;
            this.dbcontext = dbcontext;
        }
        [HttpPost]
        [Route("layPhieuXuatChiTiet")]
        public async Task<ActionResult<ResponseDTO>> LayPhieuXuatChiTiet(DieuKienLocPhieuXuatChiTiet oblay)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await phieuXuatChiTietDAO.LayPhieuXuatChiTiet(oblay);
                if (error.flagBiLoiEx || !error.flagThanhCong)
                {
                    responseDTO.statusCode = HttpStatusCode.OK;
                    responseDTO.errorCode = error.errorCode;
                    responseDTO.message = error.message;
                    return Ok(responseDTO);
                }

                responseDTO.statusCode = HttpStatusCode.OK;
                responseDTO.message = HttpStatusCode.OK.ToString();
                responseDTO.data = error.data;

                return Ok(responseDTO);
            }
            catch (Exception ex)
            {

                responseDTO.statusCode = HttpStatusCode.BadRequest;
                responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnum.BadRequest).ToString();
                responseDTO.message = ex.Message;
                return BadRequest(responseDTO);
            }
        }

    }
}
