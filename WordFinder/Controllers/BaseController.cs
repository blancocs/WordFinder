using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WordFinder.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseController:ControllerBase
    {
        private IMediator _mediator;

        //mediator inyection so anything that inherit from this clase, is able to implement it.
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
