namespace StaffInterface.Infrastructure.Contracts
{
    public record EmployeeResponse
    (
        Guid Id,
        string Lastname,
        string Firstname,
        string Patronomic,
        DateTime Birthdate,
        string PassportSeries,
        string PassportNumber,
        Guid OrganizationId
    );
}
