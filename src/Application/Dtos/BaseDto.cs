namespace Application.Dtos
{
    public record BaseDto(long? Id = null, DateTime? CreateAt = null, DateTime? UpdateAt = null)
    {
        public List<LinkDto> Links { get; set; } = new List<LinkDto>();
    }
}
