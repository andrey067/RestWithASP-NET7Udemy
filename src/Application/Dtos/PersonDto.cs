namespace Application.Dtos
{
    public record PersonDto(long? Id,
                            string FirstName,
                            string LastName,
                            string Address,
                            string Gender,
                            bool Status) : BaseDto(Id);
}
