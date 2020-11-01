using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ZipCo.Users.WebApi.Controllers
{
    /// <inheritdoc />
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ApiBaseController<T> : ControllerBase
    {
        private IMediator _mediator;
        private IMapper _mapper;
        private ILogger<T> _logger;


        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();
        protected ILogger<T> Logger => _logger ??= HttpContext.RequestServices.GetService<ILogger<T>>();

    }
}
