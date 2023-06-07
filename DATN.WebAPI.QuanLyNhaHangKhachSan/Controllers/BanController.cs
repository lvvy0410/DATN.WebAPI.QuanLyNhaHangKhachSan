using DAO;
using DTO.Context;
using DTO.Model;
using DTO.Public;
using DTO.publicDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DATN.WebAPI.QuanLyNhaHangKhachSan.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BanController : ControllerBase
    {
        private readonly QuanLyNhaHangKhachSanContext dbcontext;
        private readonly BanDAO banDAO;
        public BanController(QuanLyNhaHangKhachSanContext dbcontext, BanDAO banDAO)
        {
            this.dbcontext = dbcontext;
            this.banDAO = banDAO;
        }
        [HttpPost]
        [Route("danhsach_ban")]
        public async Task<ActionResult<ResponseDTO>> DanhSachBan()
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await banDAO.LayDanhSachBan();
                if (error.flagBiLoiEx || !error.flagThanhCong)
                {
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

        [HttpPost]
        [Route("CapNhat-Ban")]
        public async Task<ActionResult<Ban>> CapNhatBan(BanDTO ban)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await banDAO.CapNhatBan(ban);
                if (error.flagBiLoiEx || !error.flagThanhCong)
                {

                    responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTheCapNhat).ToString();
                    responseDTO.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTimThay);
                    return Ok(responseDTO);

                }
                if (error.data == null)
                {

                    responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTimThay).ToString();
                    responseDTO.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTimThay);
                    return Ok(responseDTO);
                }
                responseDTO.statusCode = HttpStatusCode.OK;
                responseDTO.message = ResponseDTO.GetValueError(ErrorCodeEnum.CapNhatThanhCong);
                responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnum.CapNhatThanhCong).ToString();
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
