using DAO;
using DTO.Context;
using DTO.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DATN.WebAPI.QuanLyNhaHangKhachSan.Controllers
{
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class LoaiBanController : Controller
    {
        private readonly QuanLyNhaHangKhachSanContext dbcontext;
        private readonly LoaiBanDAO loaiBanDAO;

        public LoaiBanController(QuanLyNhaHangKhachSanContext dbcontext, LoaiBanDAO loaiBanDAO)
        {
            this.dbcontext = dbcontext;
            this.loaiBanDAO = loaiBanDAO;
        }

        [HttpPost]
        [Route("danhsach-LoaiBan")]
        public async Task<ActionResult<ResponseDTO>> LayDanhSachLoaiBan()
        {

            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await loaiBanDAO.LayDanhSachLoaiBan();
                if (error.flagBiLoiEx || !error.flagThanhCong)//(error.flagThanhCong == false))
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
    }
}
