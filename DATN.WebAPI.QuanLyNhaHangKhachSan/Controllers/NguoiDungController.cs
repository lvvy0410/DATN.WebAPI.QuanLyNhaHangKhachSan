
using DAO;
using DTO.Context;
using DTO.Model;

using DTO.Public;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DATN.WebAPI.QuanLyNhaHangKhachSan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguoiDungController : ControllerBase
    {
        private readonly QuanLyNhaHangKhachSanContext dbcontext;
        private readonly NguoiDungDAO nguoiDungDAO;


        public NguoiDungController(QuanLyNhaHangKhachSanContext dbcontext, NguoiDungDAO nguoiDungDAO)
        {
            this.dbcontext = dbcontext;
            this.nguoiDungDAO = nguoiDungDAO;

        }

        [HttpGet]
        [Route("timNguoiDung")]
        public async Task<ActionResult<ResponseDTO>> TimNguoiDung(int nguoiDung)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await nguoiDungDAO.TimNguoiDung(nguoiDung);
                if (error.flagBiLoiEx || !error.flagThanhCong)//(error.flagThanhCong == false))
                {
                    responseDTO.statusCode = HttpStatusCode.OK;
                    responseDTO.errorCode = error.errorCode;
                    responseDTO.message = error.message;
                   
                }
                if (nguoiDung == null)
                {
                    error.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTimThay).ToString();
                    error.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTimThay);
                    error.flagThanhCong = false;
                     return Ok(responseDTO);
                }
                if (error.data == null)
                {
                    responseDTO.statusCode = HttpStatusCode.OK;
                    //responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnumDTO.NotFound).ToString();
                    //responseDTO.message = "NguoiDungng tim thay san pham";
                    responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTimThay).ToString();
                    responseDTO.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTimThay);
                    return Ok(responseDTO);
                }

                responseDTO.statusCode = HttpStatusCode.OK;
                responseDTO.message = HttpStatusCode.OK.ToString();
                responseDTO.errorCode = HttpStatusCode.OK.ToString();
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
        [Route("danhsach-NguoiDung")]
        public async Task<ActionResult<ResponseDTO>> LayDanhSachNguoiDung()
        {

            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await nguoiDungDAO.LayDanhSachNguoiDung();
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

        [HttpPost]
        [Route("Them-NguoiDung")]
        public async Task<ActionResult<NguoiDung>> ThemNguoiDung(NguoiDung nguoiDung)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await nguoiDungDAO.ThemNguoiDung(nguoiDung);
                if (error.flagBiLoiEx)
                {

                    responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTheThem).ToString();
                    responseDTO.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTheThem);
                    return Ok(responseDTO);
                }

                responseDTO.statusCode = HttpStatusCode.OK;
                responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnum.ThemThanhCong).ToString();
                responseDTO.message = ResponseDTO.GetValueError(ErrorCodeEnum.ThemThanhCong);
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
        [Route("CapNhat-NguoiDung")]
        public async Task<ActionResult<NguoiDung>> CapNhatNguoiDung(NguoiDung nguoiDung)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await nguoiDungDAO.CapNhatNguoiDung(nguoiDung);
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
        [HttpDelete]
        [Route("Xoa-NguoiDung")]
        public async Task<ActionResult<NguoiDung>> Xoa(int nguoiDung)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await nguoiDungDAO.Xoa(nguoiDung);
                if (error.flagBiLoiEx)
                {
                    responseDTO.statusCode = HttpStatusCode.OK;
                    responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTheXoa).ToString();
                    responseDTO.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTheXoa);
                    return Ok(responseDTO);
                }
                if (nguoiDung == null)
                {
                    error.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTimThay).ToString();
                    error.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTimThay);
                    return Ok(responseDTO);
                }
               
                responseDTO.statusCode = HttpStatusCode.OK;
                responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnum.XoaThanhCong).ToString();
                responseDTO.message = ResponseDTO.GetValueError(ErrorCodeEnum.XoaThanhCong);
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
