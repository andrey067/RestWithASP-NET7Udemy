using Application.Dtos;

namespace Application.Interfaces
{
    public interface ILinkServices
    {
        LinkDto Generete(string endpointName, object? routeValues, string rel, string method);
    }
}
