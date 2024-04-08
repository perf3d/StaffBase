namespace StaffBase.API.Contracts
{
    public record OrganizationResponse
    (
        Guid Id,
        string Name,
        string Inn,
        string LegalAddress,
        string ActualAddress
    );
}
