using StaffBase.Core.Utils;

namespace StaffBase.Core.Models
{
    public class Employee
    {
        public const int MAX_NAMES_SIZE = 100;
        public const int PASPORT_SERIES_SIZE = 4;
        public const int PASPORT_NUMBER_SIZE = 6;
        private Employee(
            Guid id,
            string lastname,
            string firstname,
            string patronomic,
            DateTime birthdate,
            string passportSeries,
            string passportNumber,
            Guid organizationId)
        {
            Id = id;
            Lastname = lastname;
            Firstname = firstname;
            Patronomic = patronomic;
            Birthdate = birthdate;
            PassportSeries = passportSeries;
            PassportNumber = passportNumber;
            OrganizationId = organizationId;
        }

        public Guid Id { get; }
        public string Lastname { get; }
        public string Firstname { get; }
        public string Patronomic { get; }
        public DateTime Birthdate { get; }
        public string PassportSeries { get; }
        public string PassportNumber { get; }
        public Guid OrganizationId { get; }

        public static (Employee? employee, string error) Create(
            Guid? id,
            string lastname,
            string firstname,
            string patronomic,
            DateTime? birthdate,
            string passportSeries,
            string passportNumber,
            Guid? organizationId)
        {
            string error = string.Empty;
            Employee? employee = null;

            if (id is  null)
            {
                error = "wrong id.";
                return (employee, error);
            }
            if(birthdate is null)
            {
                error = "wrong birthdate.";
                return (employee, error);
            }
            if (organizationId is null)
            {
                error = "wrong organization id.";
                return (employee, error);
            }
            bool check;
            (check, error) = Check.TextFields(lastname, MAX_NAMES_SIZE);
            if (!check)
            {
                return (employee, error);
            }
            (check, error) = Check.TextFields(firstname, MAX_NAMES_SIZE);
            if (!check)
            {
                return (employee, error);
            }
            (check, error) = Check.TextFields(patronomic, MAX_NAMES_SIZE);
            if (!check)
            {
                return (employee, error);
            }
            (check, error) = Check.PassportFields(passportNumber, PASPORT_NUMBER_SIZE);
            if (!check)
            {
                return (employee, error);
            }
            (check, error) = Check.PassportFields(passportSeries, PASPORT_SERIES_SIZE);
            if (!check)
            {
                return (employee, error);
            }
            employee = new(
                (Guid)id,
                lastname,
                firstname,
                patronomic,
                (DateTime)birthdate,
                passportSeries,
                passportNumber,
                (Guid)organizationId);
            return (employee, error);
        }
    }
}
