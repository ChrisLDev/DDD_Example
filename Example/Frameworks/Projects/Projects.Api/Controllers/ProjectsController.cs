using Microsoft.AspNetCore.Mvc;
using Projects.Application.Queries;
using Projects.Shared.DTOs;
using Website.Core.Abstractions.Pipeline;

namespace Projects.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProjectsController(IMediator mediator) : ControllerBase
	{	
		[HttpGet]
		public async Task<ActionResult<ProjectDto[]>> GetProjects()
		{
			try
			{
				return Ok(await mediator.Dispatch(new GetProjectsQuery()));
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}
	}
}
