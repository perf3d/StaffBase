namespace StaffBase.DataAccess.Entities
{
    public class EmployeeEntity
    {
        public Guid Id { get; set; }
        public string Lastname { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
        public string Patronomic { get; set; } = string.Empty;
        public DateTime Birthdate { get; set; }
        public string PassportSeries { get; set; } = string.Empty;
        public string PassportNumber { get; set; } = string.Empty;
        public Guid OrganizationId { get; set; }
        public OrganizationEntity Organization { get; set; }
    }
}
