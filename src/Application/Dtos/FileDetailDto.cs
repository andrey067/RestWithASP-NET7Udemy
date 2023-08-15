using Application.Enums;

namespace Application.Dtos
{
    public sealed record FileDetailDto(string FileName, string FileType, string FileUrl);
}
