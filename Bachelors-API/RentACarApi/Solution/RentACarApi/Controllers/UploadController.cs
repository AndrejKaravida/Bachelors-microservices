using System.Security.Claims;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentACarApi.Data;
using RentACarApi.Helpers;

namespace RentACarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UploadController : ControllerBase
    {
        private readonly IRentACarRepository _repo;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private readonly Cloudinary _cloudinary;

        public UploadController(IRentACarRepository repo, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _cloudinaryConfig = cloudinaryConfig;
            _repo = repo;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret);

            _cloudinary = new Cloudinary(acc);
        }

        [HttpPost("{vehicleId}/{companyId}")]
        public async Task<IActionResult> UploadImage(int vehicleId, int companyId, IFormFile file)
        {
            var companyFromRepo = await _repo.GetCompany(companyId);

            if(companyFromRepo == null)
            {
                return BadRequest("Cannot find company with id provided!");
            }

            if (User.FindFirst(ClaimTypes.NameIdentifier).Value != companyFromRepo.Admin &&
                User.FindFirst(ClaimTypes.NameIdentifier).Value != SystemAdminData.SysAdmin1 &&
                User.FindFirst(ClaimTypes.NameIdentifier).Value != SystemAdminData.SysAdmin2)
                return Unauthorized();

            var vehicle = _repo.GetVehicle(vehicleId);

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

            vehicle.Photo = uploadResult.Url.ToString();

            if (await _repo.SaveAll())
            {
                return NoContent();
            }

            return BadRequest("Could not add the photo");
        }

        [HttpPost("newCompany/{companyId}")]
        public async Task<IActionResult> UploadImageForCompany(int companyId, IFormFile file)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value != SystemAdminData.SysAdmin1 &&
                User.FindFirst(ClaimTypes.NameIdentifier).Value != SystemAdminData.SysAdmin2)
                return Unauthorized();

            var company = _repo.GetCompany(companyId);

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

            company.Result.Photo = uploadResult.Url.ToString();

            if (await _repo.SaveAll())
            {
                return NoContent();
            }

            return BadRequest("Could not add the photo");
        }
    }
}