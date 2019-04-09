using AuthenticateBLL.DTO;
using AuthenticateBLL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Api.Controllers
{
    [Authorize]
    [RoutePrefix("Api/Images")]
    public class ImageController : ApiController
    {
        private readonly IImageService _image;

        public ImageController(IImageService image)
        {
            _image = image;
        }

        [HttpPost]
        [Route("UploadImage")]
        public async Task<IHttpActionResult> UploadImage()
        {
            var postedFile = HttpContext.Current.Request.Files["Image"];
            if (postedFile == null)
                return BadRequest();
            var imageName = new string(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray());
            imageName += (DateTime.Now.ToLongDateString()+ DateTime.Now.ToLongTimeString() + Path.GetExtension(postedFile.FileName))
                .Replace(" ", "").Replace(":", "");
            var filePath = HttpContext.Current.Server.MapPath("~/Image") + "/" + imageName;
            postedFile.SaveAs(filePath);
            var profile = (ClaimsIdentity)User.Identity;
            var image = new ImageDTO()
            {
                UserId = profile.FindFirst("Id").Value,
                Caption = HttpContext.Current.Request.Params["Caption"],
                ImageName = imageName
            };
            var result = await _image.UploadImage(image);
            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpGet]
        [Route("GetUserImages")]
        public IEnumerable<ImageDTO> GetUserImages()
        {
            var user = (ClaimsIdentity)User.Identity;
            var result = _image.GetUserImages(user.FindFirst("Id").Value).ToList();
            if (!result.Any())
                return null;
            foreach (var image in result)
            {
                image.ImageName = "http://localhost:51312/Image/" + image.ImageName;
            }
            return result;
        }

        [HttpDelete]
        [Route("DeleteImage")]
        public async Task<IHttpActionResult> DeleteImage()
        {
            var arr = HttpContext.Current.Request.Params["ImageName"].Split('/');
            var imageName = arr[4];
            var imagePath = HttpContext.Current.Server.MapPath("~/Image") + "/" + imageName;
            if(File.Exists(imagePath))
                File.Delete(imagePath);
            var result = await _image.DeleteImage(imageName);
            if (result)
                return Ok();
            return NotFound();
        }

    }
}
