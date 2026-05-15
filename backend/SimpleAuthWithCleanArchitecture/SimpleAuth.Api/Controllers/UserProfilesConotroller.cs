using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleAuth.Api.Responses;
using SimpleAuth.Application.DTOs.Request.UserProfile;
using SimpleAuth.Application.DTOs.Response.UserProfile;
using SimpleAuth.Application.Facades.UserProfile;
using System.Security.Claims;

namespace SimpleAuth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfilesConotroller : ControllerBase
    {
        private readonly IUserProfileFacade _userProfileFacade;
        public UserProfilesConotroller(IUserProfileFacade userProfileFacade)
        {
            _userProfileFacade = userProfileFacade;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _userProfileFacade.GetAll(ct);
            var res = ApiResponse<IEnumerable<UserProfileResponse>>.Sucesss(result);
            return Ok(res);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(CancellationToken ct)
        {
            var userId = GetUserId();
            var result = await _userProfileFacade.GetProfile(userId, ct);
            var res = ApiResponse<UserProfileResponse>.Sucesss(result);
            return Ok(res);
        }

        [HttpPost("UpsertProfile")]
        public async Task<IActionResult> UpsertProfile(UpsertProfileRequest request, CancellationToken ct)
        {
            var result = await _userProfileFacade.UpsertProfile(request ,ct);
            var res = ApiResponse<UserProfileResponse>.Sucesss(result, "Profile saved");
            return Ok(res);
        }

        [HttpPost("UploadAvatar")]
        public async Task<IActionResult> UploadAvatar(IFormFile file, CancellationToken ct)
        {
            if (file == null || file.Length == 0)
                return BadRequest(ApiResponse<object>.Failure(null!, "File is required"));

            var request = new UploadAvatarRequest
            {
                UserId = GetUserId(),
                FileStream = file.OpenReadStream(),
                FileName = file.FileName,
                ContentType = file.ContentType
            };

            var result = await _userProfileFacade.UploadAvatar(request, ct);
            var res = ApiResponse<UserProfileResponse>.Sucesss(result, "Avatar uploaded");
            return Ok(res);

        }

        [HttpDelete("DeleteProfile")]
        public async Task<IActionResult> DeleteAvatar(CancellationToken ct)
        {
            var userId = GetUserId();
            var result = await _userProfileFacade.DeleteAvatar(userId, ct);
            var res = ApiResponse<object>.Sucesss(null!, "Profile deleted");
            return Ok(res);
        }

        private Guid GetUserId()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.Parse(userIdClaim!);
        }
    }
}
