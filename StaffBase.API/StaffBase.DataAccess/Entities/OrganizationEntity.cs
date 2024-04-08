namespace StaffBase.DataAccess.Entities
{
    public class OrganizationEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Inn { get; set; } = string.Empty;
        public string LegalAddress { get; set; } = string.Empty;
        public string ActualAddress { get; set; } = string.Empty;
        public List<EmployeeEntity> Employee { get; set; }
    }
}
