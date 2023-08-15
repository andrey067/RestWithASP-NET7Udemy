using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services
{
    public sealed class LinkServices : ILinkServices
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IHttpContextAccessor _contextAccessor;

        public LinkServices(LinkGenerator linkGenerator, IHttpContextAccessor contextAccessor)
        {
            _linkGenerator = linkGenerator;
            _contextAccessor = contextAccessor;
        }

        public LinkDto Generete(string endpointName, object? routeValues, string rel, string method)
        => new LinkDto(_linkGenerator.GetUriByName(_contextAccessor.HttpContext, endpointName, routeValues), rel, method);

    }
}
