using DAO;
using DTO.Context;
using DTO.Public;
using DTO.publicDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DATN.WebAPI.QuanLyNhaHangKhachSan.Controllers
{
    [Authorize]
    [Route("api/PhieuDatBanChiTiet")]
    [ApiController]
    public class PhieuDatBanChiTietController : Controller
    {
        private readonly QuanLyNhaHangKhachSanContext dbcontext;
        private readonly PhieuDatBanChiTietDAO phieuDatBanChiTietDAO;

        public PhieuDatBanChiTietController(QuanLyNhaHangKhachSanContext dbcontext, PhieuDatBanChiTietDAO phieuDatBanChiTietDAO)
        {
            this.dbcontext = dbcontext;
            this.phieuDatBanChiTietDAO = phieuDatBanChiTietDAO;
        }

        [HttpPost]
        [Route("layPhieuDatBanChiTiet")]
        public async Task<ActionResult<ResponseDTO>> LayPhieuDatBanChiTiet(PhieuDatDTO phieuDat)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await phieuDatBanChiTietDAO.LayPhieuDatBanChiTiet(phieuDat);
                if (error.flagBiLoiEx || !error.flagThanhCong)
                {
                    responseDTO.statusCode = HttpStatusCode.OK;
                    responseDTO.errorCode = error.errorCode;
                    responseDTO.message = error.message;
                    return Ok(responseDTO);
                }
                if (error.data == null)
                {
                    responseDTO.statusCode = HttpStatusCode.OK;
                    responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTimThay).ToString();
                    responseDTO.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTimThay);
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
