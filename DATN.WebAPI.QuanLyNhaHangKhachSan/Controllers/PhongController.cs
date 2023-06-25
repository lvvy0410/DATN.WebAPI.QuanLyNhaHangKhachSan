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
    [Route("api/[controller]")]
    [ApiController]
    public class PhongController : ControllerBase
    {
        private readonly QuanLyNhaHangKhachSanContext dbcontext;
        private readonly PhongDAO phongDAO;


        public PhongController(QuanLyNhaHangKhachSanContext dbcontext, PhongDAO phongDAO)
        {
            this.dbcontext = dbcontext;
            this.phongDAO = phongDAO;

        }
        [HttpGet]
        [Route("timPhong")]
        public async Task<ActionResult<ResponseDTO>> TimPhong(int phong)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await phongDAO.TimPhong(phong);
                if (error.flagBiLoiEx || !error.flagThanhCong)//(error.flagThanhCong == false))
                {
                    responseDTO.statusCode = HttpStatusCode.OK;
                    responseDTO.errorCode = error.errorCode;
                    responseDTO.message = error.message;

                }
                if (phong == null)
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
                    //responseDTO.message = "Phongng tim thay san pham";
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
        [Route("danhsach-Phong")]
        public async Task<ActionResult<ResponseDTO>> LayDanhSachPhong()
        {

            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await phongDAO.LayDanhSachPhong();
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
        [Route("Them-Phong")]
        public async Task<ActionResult<Phong>> ThemPhong(PhongDTO Phong)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await phongDAO.ThemPhong(Phong);
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
        [Route("CapNhat-Phong")]
        public async Task<ActionResult<Phong>> CapNhatPhong(PhongDTO Phong)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await phongDAO.CapNhatPhong(Phong);
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
        [Route("Xoa-Phong")]
        public async Task<ActionResult<Phong>> Xoa(int phong)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await phongDAO.Xoa(phong);
                if (error.flagBiLoiEx)
                {
                    responseDTO.statusCode = HttpStatusCode.OK;
                    responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTheXoa).ToString();
                    responseDTO.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTheXoa);
                    return Ok(responseDTO);
                }


                if (phong == null)
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
        
        [HttpPost]
        [Route("danhsach-phongthuong1g")]
        public async Task<ActionResult<ResponseDTO>> LayDanhSachPhongThuong1G(int id)
        {

            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await phongDAO.LayDanhSachPhongThuong1G(id);
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
        //    [HttpPost]
        //    [Route("danhsach-phongthuong2g")]
        //    public async Task<ActionResult<ResponseDTO>> LayDanhSachPhongThuong2G()
        //    {

        //        ResponseDTO responseDTO = new ResponseDTO();
        //        try
        //        {
        //            ErrorMessageDTO error = await phongDAO.LayDanhSachPhongThuong2G();
        //            if (error.flagBiLoiEx || !error.flagThanhCong)//(error.flagThanhCong == false))
        //            {

        //                responseDTO.errorCode = error.errorCode;
        //                responseDTO.message = error.message;
        //                return Ok(responseDTO);
        //            }

        //            responseDTO.statusCode = HttpStatusCode.OK;
        //            responseDTO.message = HttpStatusCode.OK.ToString();
        //            responseDTO.data = error.data;

        //            return Ok(responseDTO);
        //        }
        //        catch (Exception ex)
        //        {

        //            responseDTO.statusCode = HttpStatusCode.BadRequest;
        //            responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnum.BadRequest).ToString();
        //            responseDTO.message = ex.Message;
        //            return BadRequest(responseDTO);
        //        }
        //    }
        //    [HttpPost]
        //    [Route("danhsach-phongvip1g")]
        //    public async Task<ActionResult<ResponseDTO>> LayDanhSachPhongVip1G()
        //    {

        //        ResponseDTO responseDTO = new ResponseDTO();
        //        try
        //        {
        //            ErrorMessageDTO error = await phongDAO.LayDanhSachPhongVip1G();
        //            if (error.flagBiLoiEx || !error.flagThanhCong)//(error.flagThanhCong == false))
        //            {

        //                responseDTO.errorCode = error.errorCode;
        //                responseDTO.message = error.message;
        //                return Ok(responseDTO);
        //            }

        //            responseDTO.statusCode = HttpStatusCode.OK;
        //            responseDTO.message = HttpStatusCode.OK.ToString();
        //            responseDTO.data = error.data;

        //            return Ok(responseDTO);
        //        }
        //        catch (Exception ex)
        //        {

        //            responseDTO.statusCode = HttpStatusCode.BadRequest;
        //            responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnum.BadRequest).ToString();
        //            responseDTO.message = ex.Message;
        //            return BadRequest(responseDTO);
        //        }
        //    }
        //    [HttpPost]
        //    [Route("danhsach-phongvip2g")]
        //    public async Task<ActionResult<ResponseDTO>> LayDanhSachPhongVip2G()
        //    {

        //        ResponseDTO responseDTO = new ResponseDTO();
        //        try
        //        {
        //            ErrorMessageDTO error = await phongDAO.LayDanhSachPhongVip2G();
        //            if (error.flagBiLoiEx || !error.flagThanhCong)//(error.flagThanhCong == false))
        //            {

        //                responseDTO.errorCode = error.errorCode;
        //                responseDTO.message = error.message;
        //                return Ok(responseDTO);
        //            }

        //            responseDTO.statusCode = HttpStatusCode.OK;
        //            responseDTO.message = HttpStatusCode.OK.ToString();
        //            responseDTO.data = error.data;

        //            return Ok(responseDTO);
        //        }
        //        catch (Exception ex)
        //        {

        //            responseDTO.statusCode = HttpStatusCode.BadRequest;
        //            responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnum.BadRequest).ToString();
        //            responseDTO.message = ex.Message;
        //            return BadRequest(responseDTO);
        //        }
        //    }

        //}


        [HttpPost]
        [Route("Doi-Phong")]
        public async Task<ActionResult<Phong>> DoiPhong(DoiPhong doiPhong)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await phongDAO.DoiPhong(doiPhong);
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
    }
}
