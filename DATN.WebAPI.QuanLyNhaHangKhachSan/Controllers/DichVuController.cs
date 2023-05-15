using DAO;
using DTO.Context;
using DTO.Model;
using DTO.MultiTable;
using DTO.Public;
using DTO.publicDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DATN.WebAPI.QuanLyNhaHangKhachSan.Controllers
{
    [Authorize]
    [Route("api/hanghoa")]
    [ApiController]
    public class DichVuController : ControllerBase
    {
        private readonly QuanLyNhaHangKhachSanContext dbcontext;
        private readonly DichVuDAO dichVuDAO;


        public DichVuController(QuanLyNhaHangKhachSanContext dbcontext, DichVuDAO dichVuDAO)
        {
            this.dbcontext = dbcontext;
            this.dichVuDAO = dichVuDAO;

        }

        [HttpGet]
        [Route("timdichvu")]
        public async Task<ActionResult<ResponseDTO>> Tim(int dichvu)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await dichVuDAO.TimDV(dichvu);
                if (error.flagBiLoiEx || !error.flagThanhCong)//(error.flagThanhCong == false))
                {
                    responseDTO.statusCode = HttpStatusCode.OK;
                    responseDTO.errorCode = error.errorCode;
                    responseDTO.message = error.message;

                }
                if (dichvu == null)
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
                    //responseDTO.message = "HangHoang tim thay san pham";
                    responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTimThay).ToString();
                    responseDTO.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTimThay);
                    return Ok(responseDTO);
                }

                responseDTO.statusCode = HttpStatusCode.OK;
                responseDTO.message = HttpStatusCode.OK.ToString();
                responseDTO.data = error.data;
                return　Ok(responseDTO);
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
        [Route("danhsach-dichvu")]
        public async Task<ActionResult<ResponseDTO>> LayDanhSachDichVu(DichVuDTO dichVu)
        {

            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await dichVuDAO.LayDanhSachDichVu(dichVu);
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
        [Route("them-dichvu")]
        public async Task<ActionResult<DichVu>> ThemDichVu(ListDichVu listDichVu)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await dichVuDAO.ThemDV(listDichVu);
                if (error.flagBiLoiEx)
                {

                    responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTheThem).ToString();
                    responseDTO.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTheXoa);
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
        [Route("capnhat-dichvu")]
        public async Task<ActionResult<DichVu>> CapNhatDichVu(ListDichVu listDichVus)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await dichVuDAO.CapNhatDV(listDichVus);
                if (error.flagBiLoiEx || !error.flagThanhCong)
                {

                    responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTheCapNhat).ToString();
                    responseDTO.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTheCapNhat);
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
        [Route("xoa-dichvu")]
        public async Task<ActionResult<DichVu>> Xoa(int dichvu)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await dichVuDAO.Xoa(dichvu);
                if (error.flagBiLoiEx)
                {
                    responseDTO.statusCode = HttpStatusCode.OK;
                    responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTheXoa).ToString();
                    responseDTO.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTheXoa);
                    return Ok(responseDTO);
                }
                if (dichvu == null)
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
