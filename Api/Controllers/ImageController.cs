using AuthenticateBLL.DTO;
using AuthenticateBLL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;   
using System.Web;
using System.Web.Http;
using ApplicationDAL.Exceptions;

namespace Api.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Controller for interaction with images through the image service.
    /// </summary>
    [Authorize]
    [RoutePrefix("Images")]
    public class ImageController : BaseApiController
    {
        private readonly IImageService _image;

        public ImageController(IImageService image)
        {
            _image = image;
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> UploadImage()
        {
            var profile = (ClaimsIdentity)User.Identity;
            if (!Secure(profile.FindFirst("Id").Value))
                return Unauthorized();
            var postedFile = HttpContext.Current.Request.Files["Image"];
            if (postedFile == null)
                return BadRequest();
            var imageName = new string(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray());
            imageName += (DateTime.Now.ToLongDateString()+ DateTime.Now.ToLongTimeString() + Path.GetExtension(postedFile.FileName))
                .Replace(" ", "").Replace(":", "");
            var filePath = HttpContext.Current.Server.MapPath("~/Image") + "/" + imageName;
            postedFile.SaveAs(filePath);
            var image = new ImageDTO()
            {
                UserId = profile.FindFirst("Id").Value,
                Caption = HttpContext.Current.Request.Params["Caption"],
                ImageName = imageName
            };
            var result = await _image.UploadImage(image);
            if (result)
                return StatusCode(HttpStatusCode.Created);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Search/{caption}")]
        public IHttpActionResult SearchImages(string caption)
        {
            var result =_image.SearchImages(caption).ToList();
            foreach (var image in result)
            {
                image.ImageName = "http://localhost:51312/Image/" + image.ImageName;
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteImage(string id)
        {
            if (!Secure(id))
                return Unauthorized();
            var arr = HttpContext.Current.Request.Params["ImageName"].Split('/');
            var imageName = arr[4];
            var imagePath = HttpContext.Current.Server.MapPath("~/Image") + "/" + imageName;
            if(File.Exists(imagePath))
                File.Delete(imagePath);
            var result = await _image.DeleteImage(imageName);
            if (result)
                return StatusCode(HttpStatusCode.NoContent);
            return NotFound();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{page:int:min(1)}")]
        public IHttpActionResult GetAllImages(int page)
        {
            IEnumerable<ImageDTO> result;
            try
            {
                result = _image.GetAllImages(page).ToList();
            }
            catch (WrongPageException e)
            {
                return BadRequest(e.ToString());
            }
            foreach (var image in result)
            {
                image.ImageName = "http://localhost:51312/Image/" + image.ImageName;
            }
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetPages")]
        public IHttpActionResult GetPages()
        {
            return Ok(_image.GetPages());
        }
    }
}
