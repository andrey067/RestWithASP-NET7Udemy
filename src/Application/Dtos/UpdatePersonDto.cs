namespace Application.Dtos
{
    public sealed record UpdatePersonDto(long? Id, string FirstName, string LastName, string Address, string Gender): BaseDto(Id);
}
