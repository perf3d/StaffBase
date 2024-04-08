namespace StaffBase.API.Contracts
{
    public record EmployeeRequest
    (
        string lastname,
        string firstname,
        string patronomic,
        DateTime birthdate,
        string passportSeries,
        string passportNumber,
        Guid organizationId
    );
}
