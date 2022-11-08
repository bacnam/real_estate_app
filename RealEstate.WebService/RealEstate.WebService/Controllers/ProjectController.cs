using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Requests;
using RealEstate.Core.Responses;
using RealEstate.WebService.Attributes;
using RealEstate.WebService.Extensions;
using RealEstate.WebService.Services;

namespace RealEstate.WebService.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        IProjectService ProjectService;
        IUserService AccountService;

        public ProjectController(IProjectService projectService, IUserService accountService)
        {
            AccountService = accountService;
            ProjectService = projectService;
        }

        [HttpPost(), AccountAuthorize]
        public async Task<ActionResult> GetNewsAsync([FromBody] SearchProjectRequest request)
        {
            var account = AccountService.GetAccountFromToken(HttpContext.GetToken());
            if (account != null)
            {
                var data = await ProjectService.GetProjectsByAccountAsync(account.Id, request.City, request.District, request.Start);
                return new JsonResult(data);
            }
            return new UnauthorizedResult();
        }

        [HttpPut("save"), AccountAuthorize]
        public async Task<ActionResult> ChangeProjectSaveAsync([FromBody] ChangeSaveStateProjectRequest request)
        {
            var account = AccountService.GetAccountFromToken(HttpContext.GetToken());
            if (account != null)
            {
                var data = await ProjectService.ChangeProjectSaveAsync(account.Id, request.ProjectId);
                ChangeSaveStateProjectResponse response = new ChangeSaveStateProjectResponse();
                if (data)
                {
                    response.Success = true;
                }
                return new JsonResult(response);
            }
            return new UnauthorizedResult();
        }

        [HttpGet("{projectId}"), AccountAuthorize]
        public async Task<ActionResult> GetProjectDetailsAsync([FromRoute] long projectId)
        {
            var account = AccountService.GetAccountFromToken(HttpContext.GetToken());
            if (account != null)
            {
                var data = await ProjectService.GetProjectDetailsAsync(account.Id, projectId);
                return new JsonResult(data);
            }
            return new UnauthorizedResult();
        }

        [HttpGet("saved"), AccountAuthorize]
        public async Task<ActionResult> GetSavedProjectsAsync([FromQuery] int start = 0)
        {
            var account = AccountService.GetAccountFromToken(HttpContext.GetToken());
            if (account != null)
            {
                var data = await ProjectService.GetSavedProjectsAsync(account.Id, start);
                return new JsonResult(data);
            }
            return new UnauthorizedResult();
        }
    }
}
