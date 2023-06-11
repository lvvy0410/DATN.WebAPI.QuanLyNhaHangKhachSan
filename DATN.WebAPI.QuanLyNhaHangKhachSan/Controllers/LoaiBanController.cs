using DAO;
using DTO.Context;
using DTO.Model;
using DTO.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DATN.WebAPI.QuanLyNhaHangKhachSan.Controllers
{ [Authorize]
        [Route("api/[controller]")]
        [ApiController]
    public class LoaiBanController : ControllerBase
    {
        private readonly QuanLyNhaHangKhachSanContext dbcontext;
        private readonly LoaiBanDAO loaiBanDAO;
        public LoaiBanController(QuanLyNhaHangKhachSanContext dbcontext, LoaiBanDAO loaiBanDAO)
        {
            this.dbcontext = dbcontext;
            this.loaiBanDAO = loaiBanDAO;
        }
        [HttpPost]
        [Route("lay_ds_loaiban")]
        public async Task<ActionResult<ResponseDTO>> LayLoaiBan()
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await loaiBanDAO.LayDanhSachLoaiBan();
                if(error.flagBiLoiEx||!error.flagThanhCong)
                {
                    responseDTO.message= error.message;
                    responseDTO.errorCode= error.errorCode;
                    return Ok(responseDTO);
                }
                error.flagThanhCong = true;
                responseDTO.message= error.message;
                responseDTO.statusCode = HttpStatusCode.OK;
                responseDTO.data= error.data;
                return Ok(responseDTO);
            }
            catch(Exception ex)
            {
                responseDTO.statusCode = HttpStatusCode.BadRequest;
                responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnum.BadRequest).ToString();
                responseDTO.message = ex.Message;
                return BadRequest(responseDTO);
            }
        }

       
    }
}
