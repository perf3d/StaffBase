namespace StaffInterface.Infrastructure.Contracts
{
    public record OrganizationRequest
    (
        string name,
        string inn,
        string legalAddress,
        string actualAddress
    );
}
