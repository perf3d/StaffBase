namespace StaffBase.API.Contracts
{
    public record EmployeeResponse
    (
        Guid id,
        string lastname,
        string firstname,
        string patronomic,
        DateTime birthdate,
        string passportSeries,
        string passportNumber,
        Guid organizationId
    );
}
