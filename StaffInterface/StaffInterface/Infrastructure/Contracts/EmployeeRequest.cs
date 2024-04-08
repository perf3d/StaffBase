namespace StaffInterface.Infrastructure.Contracts
{
    public record EmployeeRequest
    (
        string Lastname,
        string Firstname,
        string Patronomic,
        DateTime Birthdate,
        string PassportSeries,
        string PassportNumber,
        Guid OrganizationId
    );
}
