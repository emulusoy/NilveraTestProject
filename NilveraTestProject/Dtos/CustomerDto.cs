using NilveraTestProject.Entities;

namespace NilveraTestProject.Dtos
{
    public record CustomerDto(
        int Id,
        string Name,
        string Surname,
        string Email,
        string Phone,
        string Address,
        string JsonData,
        CustomerExtra? Extra
    );
}
