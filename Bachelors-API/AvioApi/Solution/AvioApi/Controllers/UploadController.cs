using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AvioApi.Helpers;
using AvioApi.Data.Repository;
using CloudinaryDotNet;
using Microsoft.Extensions.Options;
using CloudinaryDotNet.Actions;

namespace AvioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UploadController : ControllerBase
    {
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private readonly Cloudinary _cloudinary;
        private readonly IFlightsRepository _aviorepo;

        public UploadController(IFlightsRepository aviorepo, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _cloudinaryConfig = cloudinaryConfig;
            _aviorepo = aviorepo;

            Account acc = new Account(
               _cloudinaryConfig.Value.CloudName,
               _cloudinaryConfig.Value.ApiKey,
               _cloudinaryConfig.Value.ApiSecret);

            _cloudinary = new Cloudinary(acc);
        }

        [HttpPost("newAvioCompany/{companyId}")]
        public async Task<IActionResult> UploadImageForAvioCompany(int companyId, IFormFile file)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value != SystemAdminData.SysAdmin1 &&
                User.FindFirst(ClaimTypes.NameIdentifier).Value != SystemAdminData.SysAdmin2)
                return Unauthorized();

            var company = _aviorepo.GetCompany(companyId);

            if(company == null)
            {
                return BadRequest("Cannot find company with id provided!");
            }

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            company.Photo = uploadResult.Url.ToString();

            if (await _aviorepo.SaveAll())
            {
                return NoContent();
            }

            return BadRequest("Could not add the photo");
        }
    }
}